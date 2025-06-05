using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[SelectionBase]
public class CharacterMovementBehaviour : MonoBehaviour
{
    [SerializeField]
    float speed = 2f;
    [SerializeField]
    float sprintSpeed = 5f;
    [SerializeField]
    float sprintDuration = 1f;
    [SerializeField]
    float sprintCooldown = 3f;
    [SerializeField]
    Rigidbody rb;
    Vector3 inputVector = new Vector3(0f, 0f, 0f);
    [SerializeField]
    float rotationSpeed = 60f;
    [SerializeField]
    float JumpForce = 3f;
    [SerializeField]
    Animator animator;

    private float sprintTime = 0f;
    private float cooldownTime = 0f;
    private bool isSprinting = false;

    private Vector3 lastMovementDirection = Vector3.forward; // NEW: store last nonzero direction

    private void OnDisable()
    {
        inputVector = Vector3.zero;
    }

    void Update()
    {
        if (cooldownTime > 0f)
        {
            cooldownTime -= Time.deltaTime;
        }

        if (cooldownTime <= 0f && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
        {
            if (sprintTime < sprintDuration)
            {
                isSprinting = true;
                sprintTime += Time.deltaTime;
            }
            else
            {
                isSprinting = false;
                cooldownTime = sprintCooldown;
                sprintTime = 0f;
            }
        }
        else
        {
            isSprinting = false;
        }

        inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;

        // Update last movement direction only if input exists
        if (inputVector.magnitude > 0f)
        {
            lastMovementDirection = inputVector;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, 1.2f))
            {
                Jump();
            }
        }
    }

    private void FixedUpdate()
    {
        float currentSpeed = isSprinting ? sprintSpeed : speed;

        Vector3 targetVelocity = inputVector * currentSpeed;
        targetVelocity.y = rb.linearVelocity.y;

        rb.linearVelocity = targetVelocity;

        // Use last nonzero movement direction for rotation
        if (lastMovementDirection.magnitude > 0f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(lastMovementDirection, Vector3.up));
            Quaternion smoothRotation = Quaternion.RotateTowards(rb.rotation, targetRotation, rotationSpeed);
            rb.MoveRotation(smoothRotation);
        }

        animator.SetFloat("Speed", targetVelocity.magnitude);
    }

    void Jump()
    {
        rb.AddForce(0f, JumpForce, 0f, ForceMode.Impulse);
    }
}
