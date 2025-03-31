using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public Transform cameraTransform;
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (cameraTransform == null)
        {
            Debug.LogError("Camera Transform not assigned to the ThirdPersonController script!");
        }
    }

    void FixedUpdate()
    {
        if (cameraTransform == null)
            return;

        Vector3 moveDirection = GetMoveDirection();

        // Rotate the character
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            rb.rotation = Quaternion.Lerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }

        // Move the character
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);

    }

    private Vector3 GetMoveDirection()
    {
        // Get camera forward direction without vertical component
        Vector3 cameraForward = cameraTransform.forward;
        cameraForward.y = 0f;
        cameraForward.Normalize();

        // Get camera right direction without vertical component
        Vector3 cameraRight = cameraTransform.right;
        cameraRight.y = 0f;
        cameraRight.Normalize();

        // Calculate movement direction based on input and camera orientation
        Vector3 moveDirection = Input.GetAxis("Vertical") * cameraForward +
                                Input.GetAxis("Horizontal") * cameraRight;
        moveDirection.Normalize();

        return moveDirection;
    }
}