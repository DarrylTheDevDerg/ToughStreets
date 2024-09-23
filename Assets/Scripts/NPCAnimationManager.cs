using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class NPCAnimationManager : MonoBehaviour
{
    [Range(0, 3)]
    public int npcType;
    public string interactTrigger;

    public string[] firstMessage;
    public string[] secondMessage;

    public bool giveItem;
    public GameObject[] items;

    public bool alreadyInteracted;
    private Animator an;
    public TypewriterEffect te;
    public PlayerMovement pM;

    // Start is called before the first frame update
    void Start()
    {
        an = GetComponent<Animator>();
        te = FindObjectOfType<TypewriterEffect>();
        pM = FindObjectOfType<PlayerMovement>();

        an.SetLayerWeight(npcType, 1f);

        for (int i = 0; i < npcType; i++)
        {
            if (an.GetLayerWeight(i) != npcType)
            {
                an.SetLayerWeight(i, 0f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GiveItems()
    {
        switch (items.Length)
        {
            case 1:
                Instantiate(items[0], transform.position, Quaternion.identity);
                break;

            default:
                foreach (var item in items)
                {
                    Instantiate(item, transform.position, Quaternion.identity);
                }

                break;
        }   
    }

    public void InteractWithPlayer()
    {
        if (!alreadyInteracted)
        {
            an.SetTrigger(interactTrigger);
            if (giveItem)
            {
                GiveItems();
            }

            te.StartTyping();

            alreadyInteracted = true;
        }
        else
        {
            an.SetTrigger(interactTrigger);
        }
    }
}
