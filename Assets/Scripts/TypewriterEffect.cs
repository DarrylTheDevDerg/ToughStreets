using UnityEngine;
using TMPro;
using System.Collections;

public class TypewriterEffect : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;   // Reference to the TextMeshProUGUI component
    public float typingSpeed = 0.05f;     // Time between each character being revealed
    public string[] dialogueStrings;      // Array of strings to display
    public bool interacting, inactiveAfterUse;
    private int currentStringIndex = 0;   // Tracks which string we're on

    private bool isTyping = false;        // Are we currently typing out a string?
    private bool skipTyping = false;      // Should we skip the typing animation?
    private bool textFullyDisplayed = false; // Is the current text fully displayed?
    private NPCAnimationManager npc;
    private TransparencyEffect tE;

    void Start()
    {
        npc = FindObjectOfType<NPCAnimationManager>();
        tE = FindObjectOfType<TransparencyEffect>();

        // Start with the first string
        if (npc != null)
        {
            dialogueStrings = npc.firstMessage;
        }
        else if (npc == null)
        {
            StartTyping();
        }

    }

    void Update()
    {
        if (npc != null)
        {
            if (npc.alreadyInteracted && !interacting)
            {
                dialogueStrings = npc.secondMessage;
            }
        }

        // Check for the "E" key press
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isTyping)
            {
                // If typing, skip the current typewriter effect
                skipTyping = true;
            }
            else if (textFullyDisplayed)
            {
                // If the text is fully displayed, advance to the next string when the player presses "E"
                NextString();
            }
        }
    }

    IEnumerator TypeText(string textToType)
    {
        isTyping = true;
        textFullyDisplayed = false;  // Reset flag for fully displayed text
        textMeshPro.text = "";       // Clear any existing text
        skipTyping = false;          // Reset skip flag

        for (int i = 0; i < textToType.Length; i++)
        {
            if (skipTyping)
            {
                // If skipping, show the full text instantly
                textMeshPro.text = textToType;
                break;
            }

            // Append the next character to the text
            textMeshPro.text += textToType[i];
            yield return new WaitForSeconds(typingSpeed);
        }

        // Text is now fully displayed
        isTyping = false;
        textFullyDisplayed = true;
    }

    void NextString()
    {
        currentStringIndex++;

        if (currentStringIndex < dialogueStrings.Length)
        {
            // Start typing the next string in the list
            StartCoroutine(TypeText(dialogueStrings[currentStringIndex]));
        }
        else
        {
            // Optional: If all strings have been displayed, trigger an event or action here
            if (inactiveAfterUse)
            {
                textMeshPro.gameObject.SetActive(false);
                interacting = false;
            }

            if (npc != null)
            {
                npc.pM.enabled = true;
                npc.pA.enabled = true;
                npc.an.SetBool(npc.interactTrigger, false);
            }
        }
    }

    public void ResetStringIndex()
    {
        currentStringIndex = 0;
    }

    public void StartTyping()
    {
        textMeshPro.gameObject.SetActive(true);
        interacting = true;

        ResetStringIndex();
        StartCoroutine(TypeText(dialogueStrings[currentStringIndex]));

        if (npc != null && tE != null)
        {
            npc.pM.enabled = false;
            npc.pA.enabled = false;

            tE.SetInvul();
        }
    }
}
