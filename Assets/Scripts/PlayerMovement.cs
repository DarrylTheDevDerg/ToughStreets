using System.Collections;
using System.Collections.Generic;
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
    public string animatorRun;

    private float dashCooldown;
    private bool grounded;

    private float smoothSpeedVelocity = 0f;

    private Rigidbody rb;                    // Reference to the Rigidbody component

    void Start()
    {
        rb = GetComponent<Rigidbody>();      // Get the Rigidbody component
        if (animator == null)
        {
            animator = GetComponent<Animator>();  // Get the Animator component if not assigned
        }
    }

    void Update()
    {
        // Get input from the player
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate the movement direction
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        // Check if the player is moving
        bool isMoving = direction.magnitude >= 0.1f;

        float targetSpeed = direction.magnitude * movementSpeed;

        // Update the Animator parameter
        float currentSpeed = animator.GetFloat(animatorWalk);

        float smoothSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref smoothSpeedVelocity, 0.3f);

        animator.SetFloat(animatorWalk, smoothSpeed);

        if (isMoving)
        {

            // Calculate the target rotation based on the movement direction
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);

            // Smoothly rotate the player to face the movement direction
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Move the player in the direction they are facing
            Vector3 moveDirection = transform.forward * movementSpeed * Time.deltaTime;
            rb.MovePosition(rb.position + moveDirection);
        }

        if (animator.GetFloat(animatorWalk) > 1f)
        {
            animator.SetFloat(animatorWalk, 1f);
        }
        else if (animator.GetFloat(animatorWalk) < 0f)
        {
            animator.SetFloat(animatorWalk, 0f);
        }

    }
}