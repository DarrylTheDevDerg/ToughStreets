using UnityEditor;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public KeyCode keyCode;
    public float rayDistance;
    public LayerMask rayMask;
    public string npcTag, interactTrigger, crateTag;

    private Animator an;
    private TypewriterEffect te;

    // Start is called before the first frame update
    void Start()
    {
        an = GetComponent<Animator>();
        te = FindObjectOfType<TypewriterEffect>();
    }

    void Update()
    {
        if (Input.GetKeyDown(keyCode) && !te.interacting) // Trigger on space key press
        {
            PerformRaycast();
        }
    }

    void InteractWith()
    {
        if (an.GetFloat("Walk") > 0)
        {
            an.SetFloat("Walk", 0);
        }

        if (te.interacting)
        {
            an.SetTrigger(interactTrigger);
        }
    }

    private void PerformRaycast()
    {
        // Define the ray origin (object's position) and direction (forward)
        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = transform.forward;

        RaycastHit hit;

        // Perform the Raycast
        if (Physics.Raycast(rayOrigin, rayDirection, out hit, rayDistance, rayMask))
        {
            Debug.Log("Ray hit: " + hit.collider.name);
            InitiateInteraction(hit);
        }
        else
        {
            Debug.Log("Ray did not hit any object.");
        }
    }

    // Draw the Raycast in the Scene view using Gizmos
    private void OnDrawGizmos()
    {
        // Calculate a rainbow color based on time
        Gizmos.color = GetRainbowColor(Time.time);

        // Ray origin and direction
        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = transform.forward;

        // Draw the ray line
        Gizmos.DrawRay(rayOrigin, rayDirection * rayDistance);

        // Optional: Draw a small sphere at the end of the ray to indicate its length
        Gizmos.DrawWireSphere(rayOrigin + rayDirection * rayDistance, 0.1f);
    }

    // Function to generate a rainbow color based on time
    private Color GetRainbowColor(float time)
    {
        // Use Mathf.PingPong to loop the hue value smoothly between 0 and 1
        float hue = Mathf.Repeat(time * 0.1f, 1f); // Speed of hue change can be adjusted by multiplying time
        return Color.HSVToRGB(hue, 1f, 1f); // Full saturation and brightness for vivid colors
    }

    void InitiateInteraction(RaycastHit hit)
    {
        if (hit.collider.CompareTag(npcTag))
        {
            NPCAnimationManager npc = FindObjectOfType<NPCAnimationManager>();

            if (npc != null)
            {
                InteractWith();
                npc.InteractWithPlayer();
            }
        }

        if (hit.collider.CompareTag(crateTag))
        {
            Crate crate = FindObjectOfType<Crate>();

            if (crate != null)
            {
                InteractWith();
                crate.crateHP = 0;
            }
        }
    }
}
