using UnityEngine;

public class RootMotionHover : MonoBehaviour
{
    public float groundLevel = 0f;  // The minimum Y position (ground level)
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Apply Root Motion, but limit Y position to stay above the detected ground level
        if (animator != null && animator.applyRootMotion)
        {
            Vector3 newPosition = transform.position;

            // Raycast down to detect the ground
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit))
            {
                groundLevel = hit.point.y;  // Set ground level to the hit point of the ground
            }

            // Ensure the Y position never goes below the ground level
            if (newPosition.y < groundLevel)
            {
                newPosition.y = groundLevel;
            }

            // Apply the clamped position
            transform.position = newPosition;
        }
    }

}
