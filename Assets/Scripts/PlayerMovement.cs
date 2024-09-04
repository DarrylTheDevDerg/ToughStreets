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

    private float dashCooldown;
    private bool grounded;
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

        // Update the Animator parameter
        float currentSpeed = animator.GetFloat(animatorWalk);

        float smoothSpeed = Mathf.Lerp(currentSpeed, targetSpeed, 0.3f);

        animator.SetFloat(animatorWalk, smoothSpeed);

        if (isMoving)
        {
            // Calculate the target rotation based on the movement direction
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);

            // Smoothly rotate the player to face the movement direction
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Move the player in the direction they are facing
            if (!isRunning)
            {
                Vector3 moveDirection = transform.forward * movementSpeed * Time.deltaTime;
                rb.MovePosition(rb.position + moveDirection);
            }
            else
            {
                Vector3 moveDirection = transform.forward * sprint * Time.deltaTime;
                rb.MovePosition(rb.position + moveDirection);
            }
        }

        if (smoothSpeed < 0.5)
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
}
