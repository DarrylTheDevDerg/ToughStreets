using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public string animatorName, attackTrigger;

    public float stoppingDistance = 2.0f;  // Distance at which the NPC stops following
    public float rotationSpeed, animationDampTime, attackCooldown, attackTime, retreatDistance;

    private NavMeshAgent agent;
    private Animator an;

    private TypewriterEffect te;
    private float currentSpeed;
    public bool attacking, shouldStepBack;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = stoppingDistance;

        an = GetComponent<Animator>();
        te = FindObjectOfType<TypewriterEffect>();

        player = FindObjectOfType<PlayerMovement>().gameObject.transform;
    }

    void Update()
    {
        // Check if the player is within stopping distance
        if (player != null && !te.interacting && !attacking)
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

                attackTime += Time.deltaTime;
            }
        }

        if (!attacking)
        {
            LookAtPlayer();
        }

        if (attackTime > attackCooldown)
        {
            attacking = true;
            AttackPlayer();
        }


        if (shouldStepBack)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // If the player is within the stopping distance, make the agent retreat
            if (distanceToPlayer <= stoppingDistance)
            {
                RetreatFromPlayer();
            }
            else
            {
                // Move towards the player or perform normal behavior (like patrolling)
                agent.SetDestination(player.position);
            }
        }
    }

    public void LookAtPlayer()
    {
        // Get the direction to the player
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0;  // Lock rotation to only rotate on the Y-axis (so it doesn't tilt up/down)

        // Get the target rotation to face the player
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        // Smoothly rotate towards the player over time
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    public void AttackPlayer()
    {
        an.SetTrigger(attackTrigger);
        attackTime = 0;
        attacking = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.GetComponent<PlayerAttack>().invul <= 0)
        {
            Enemy en = GetComponent<Enemy>();
            en.PlayerHarm();
        }
    }

    void RetreatFromPlayer()
    {
        // Calculate the direction away from the player
        Vector3 directionAwayFromPlayer = transform.position - player.position;
        directionAwayFromPlayer.Normalize();  // Normalize to get just the direction

        // Calculate the new position a few steps back
        Vector3 retreatPosition = transform.position + directionAwayFromPlayer * retreatDistance;

        // Move the agent to the retreat position
        agent.SetDestination(retreatPosition);
    }
}
