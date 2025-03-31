using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class StrictMovement : ICharacterMovement
{
    private BodyRotation _rotationLogic;

    public Transform cameraTransform;
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;

    private Rigidbody rb;
    private Vector3 moveDirection = Vector3.zero;
    public override bool IsMoving => moveDirection != Vector3.zero;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        _rotationLogic = GetComponentInChildren<BodyRotation>();
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
            //Debug.LogError("Camera Transform not assigned to the ThirdPersonController script!");
        }
    }
    

    void FixedUpdate()
    {
        if (cameraTransform == null)
            return;

        moveDirection = GetMoveDirection();

        // Rotate the character
        if (IsMoving)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            _rotationLogic.RotateTowards(targetRotation);
        }
        else
        {
            _rotationLogic.AutoRotate();
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
        //print("CAMERA " + cameraTransform.eulerAngles.y);
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

    public override void Activate()
    {
        rb.isKinematic = false;
    }

    public override void Deactivate()
    {
        rb.isKinematic = true;
    }
}
