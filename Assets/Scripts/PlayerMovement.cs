using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float movementSpeed;
    public int dashCooldownAmount;
    public float rotationSpeed = 720f;       // Rotation speed of the player

    public string groundTag;

    [Header("Animation")]
    public Animator animator;                // Reference to the Animator component
    public float acceleration;

    public string animatorWalk;
    private bool isRunning;

    private float sprint;

    private Rigidbody rb;                    // Reference to the Rigidbody component

    void Start()
    {
        rb = GetComponent<Rigidbody>();      // Get the Rigidbody component
        if (animator == null)
        {
            animator = GetComponent<Animator>();  // Get the Animator component if not assigned
        }

        sprint = movementSpeed * 1.5f;
    }

    void Update()
    {
        RunFunction();
        PlayerMove();
        AnimationManage();

        rb.angularVelocity = Vector3.zero;
    }

    void PlayerMove()
    {
        // Get input from the player
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Apply a dead zone
        if (Mathf.Abs(horizontal) < 0.1f) horizontal = 0;
        if (Mathf.Abs(vertical) < 0.1f) vertical = 0;

        // Calculate the movement direction
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        // Check if the player is moving
        bool isMoving = direction.magnitude >= 0.1f;

        float targetSpeed = direction.magnitude * movementSpeed;

        // Update the Animator parameter for walking
        float currentSpeed = animator.GetFloat(animatorWalk);
        float smoothSpeed = Mathf.Lerp(currentSpeed, targetSpeed, 0.3f);
        animator.SetFloat(animatorWalk, smoothSpeed);

        // Check if the current animation is "special" (e.g., attack, jump, etc.)
        if (IsPlayingSpecialAnimation())
        {
            // Reset any potential unwanted rotation after special animations
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0); // Lock X and Z axis rotation

            rb.angularVelocity = Vector3.zero;
        }

        if (isMoving)
        {
            // Disable root motion during movement
            animator.applyRootMotion = false;

            // Calculate the target rotation based on the movement direction
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);

            // Smoothly rotate the player to face the movement direction
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Move the player in the direction they are facing
            Vector3 moveDirection = isRunning ? transform.forward * sprint * Time.deltaTime : transform.forward * movementSpeed * Time.deltaTime;
            rb.MovePosition(rb.position + moveDirection);
        }
        else
        {
            // Enable root motion for non-movement animations
            animator.applyRootMotion = true;
        }

        // Stop the character if speed drops too low
        if (smoothSpeed < 0.5f)
        {
            smoothSpeed = 0;
        }
    }


    void RunFunction()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
    }

    void AnimationManage()
    {
        if (animator.GetFloat(animatorWalk) > 1f && !isRunning)
        {
            animator.SetFloat(animatorWalk, 1f);
        }
        else if (animator.GetFloat(animatorWalk) > 2f && isRunning)
        {
            animator.SetFloat(animatorWalk, 2f);
        }
        else if (animator.GetFloat(animatorWalk) < 0f && !isRunning)
        {
            animator.SetFloat(animatorWalk, 0f);
        }
    }

    bool IsPlayingSpecialAnimation()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0); // 0 is the base layer

        // Assuming you know the name of your special animation states (e.g., "Attack", "Jump")
        if (stateInfo.IsName("Combo1") || stateInfo.IsName("Combo2") || stateInfo.IsName("Combo3") || stateInfo.IsName("Hurt"))
        {
            return true;
        }

        return false;
    }


}
