using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovementController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float sensa;
    [SerializeField] private float maxVerticalRotation;
    [SerializeField] private float minVerticalRotation;
    [SerializeField] private float gravityAcceleration;
    [Space]
    [SerializeField] private Transform objectToRotateVertically;

    private CharacterController characterController;

    private float inputX, inputZ, mouseInputX, mouseInputY, xRotation, velocityY;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        xRotation = transform.localEulerAngles.x;
        velocityY = 0f;
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Update()
    {
        inputX = Input.GetAxis(Axes.Horizontal);
        inputZ = Input.GetAxis(Axes.Vertical);

        mouseInputX = Input.GetAxis(Axes.MouseX);
        mouseInputY = Input.GetAxis(Axes.MouseY);


        if (Input.GetKeyDown(KeyCode.Space) && characterController.isGrounded)
        {
            velocityY = jumpSpeed;
        }

        Vector3 move = transform.TransformDirection(new Vector3(inputX, 0f, inputZ)) * moveSpeed;
        characterController.Move((move + Vector3.up * velocityY) * Time.deltaTime);

        transform.Rotate(new Vector3(0f, mouseInputX * sensa, 0f));
        RotateVertically(objectToRotateVertically ? objectToRotateVertically : transform);
    }

    private void FixedUpdate()
    {
        if (!characterController.isGrounded)
        {
            velocityY -= gravityAcceleration * Time.fixedDeltaTime;
        }
    }

    private void RotateVertically(Transform transform)
    {
        xRotation -= mouseInputY * sensa;
        xRotation = Mathf.Clamp(xRotation, minVerticalRotation, maxVerticalRotation);
        transform.localEulerAngles = new Vector3(
            xRotation,
            transform.localEulerAngles.y,
            transform.localEulerAngles.z
            );
    }
}
