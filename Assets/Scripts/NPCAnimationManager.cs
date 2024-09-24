using System.Collections;
using System.Collections.Generic;
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
    public Animator an;
    public TypewriterEffect te;
    public PlayerMovement pM;

    public PlayerAttack pA;

    // Start is called before the first frame update
    void Start()
    {
        an = GetComponent<Animator>();
        te = FindObjectOfType<TypewriterEffect>();
        pM = FindObjectOfType<PlayerMovement>();

        pA = FindObjectOfType<PlayerAttack>();

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
            an.SetBool(interactTrigger, true);
            if (giveItem)
            {
                GiveItems();
            }

            LookAtPlayer();
            te.StartTyping();

            alreadyInteracted = true;
        }
        else
        {
            LookAtPlayer();
            an.SetBool(interactTrigger, true);
            te.StartTyping();
        }
    }

    public void LookAtPlayer()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;

        // Get the direction to the player
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0;  // Lock rotation to only rotate on the Y-axis (so it doesn't tilt up/down)

        // Get the target rotation to face the player
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        // Smoothly rotate towards the player over time
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
