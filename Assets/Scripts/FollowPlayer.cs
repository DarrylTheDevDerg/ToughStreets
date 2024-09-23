using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public string animatorName;

    public float stoppingDistance = 2.0f;  // Distance at which the NPC stops following
    public float rotationSpeed, animationDampTime;

    private NavMeshAgent agent;
    private Animator an;
    private TypewriterEffect te;

    private float currentSpeed;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = stoppingDistance;

        an = GetComponent<Animator>();
        te = FindObjectOfType<TypewriterEffect>();
    }

    void Update()
    {
        // Check if the player is within stopping distance
        if (player != null && !te.interacting)
        {
            float distance = Vector3.Distance(player.position, transform.position);

            if (distance > stoppingDistance)
            {
                // Continue moving if the player is farther than the stopping distance
                agent.SetDestination(player.position);
                currentSpeed = Mathf.Lerp(currentSpeed, agent.velocity.magnitude, animationDampTime);
                an.SetFloat(animatorName, currentSpeed);
            }
            else
            {
                // Stop moving if close enough to the player
                agent.ResetPath();
                currentSpeed = Mathf.Lerp(currentSpeed, agent.velocity.magnitude, animationDampTime);
                an.SetFloat(animatorName, currentSpeed);
            }
        }

        LookAtPlayer();
    }

    void LookAtPlayer()
    {
        // Get the direction to the player
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0;  // Lock rotation to only rotate on the Y-axis (so it doesn't tilt up/down)

        // Get the target rotation to face the player
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        // Smoothly rotate towards the player over time
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }
}
