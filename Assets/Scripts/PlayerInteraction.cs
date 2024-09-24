using UnityEditor;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public KeyCode keyCode;
    public float rayDistance, radius;
    public LayerMask rayMask;
    public string npcTag, interactTrigger, crateTag;

    private NPCAnimationManager npc;
    private Animator an;

    // Start is called before the first frame update
    void Start()
    {
        npc = FindObjectOfType<NPCAnimationManager>();
        an = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(keyCode) && !npc.te.interacting) // Trigger on space key press
        {
            // Start point of the ray
            Vector3 rayOrigin = transform.position;
            // Direction of the ray
            Vector3 rayDirection = transform.forward;

            // Perform the raycast
            RaycastHit hit;
            if (Physics.Raycast(rayOrigin, rayDirection, out hit, rayDistance, rayMask))
            {
                Debug.Log("Ray hit: " + hit.collider.name);
                // Perform a sphere check at the hit point
                Collider[] hitColliders = Physics.OverlapSphere(hit.point, radius, rayMask);
                foreach (var collider in hitColliders)
                {
                    Debug.Log("Collider found in sphere: " + collider.name);

                    if (collider.CompareTag(npcTag))
                    {
                        InteractWith();
                        npc.InteractWithPlayer();
                    }
                    else if (collider.CompareTag(crateTag))
                    {
                        InteractWith();
                        collider.GetComponent<Crate>().crateHP = 0;
                    }

                }
            }
            else
            {
                Debug.Log("Ray did not hit any object.");
            }
        }
    }

    void InteractWith()
    {
        if (!npc.te.interacting)
        {
            an.SetTrigger(interactTrigger);
        }
    }

    void OnDrawGizmos()
    {
        // Draw the ray as a line
        Gizmos.color = Color.red; // Color of the Gizmo
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward * rayDistance;

        Gizmos.DrawLine(origin, origin + direction);

        // Draw the sphere at the end of the ray
        Gizmos.color = Color.clear; // Color for the sphere
        Gizmos.DrawSphere(origin + direction, rayDistance);
    }
}
