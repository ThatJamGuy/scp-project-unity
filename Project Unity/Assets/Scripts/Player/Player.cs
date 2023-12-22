using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] GameObject playerCamera;

    [Header("Look/Movement Values")]
    [SerializeField] float movementSpeed;
    [SerializeField] float sprintingSpeed;
    [SerializeField] float sensetivity;
    [SerializeField] float lookXLimit;

    [Header("Headbobbing Values")]
    [SerializeField] float bobbingAmount;
    [SerializeField] float bobbingSpeed;

    [HideInInspector] public bool canMove = true;

    private CharacterController charController;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private float timer = 0.0f;

    private void Start()
    {
        // Define private variables if needed
        charController = GetComponent<CharacterController>();

        // Lock the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Basic movement and look functions required for at least decent gameplay!
        Move();
        Look();
        HeadBobbing();
    }

    // Method for controller basic movement
    void Move()
    {
        // Recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Left shift to start sprinting
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);

        float curSpeedX = canMove ? (isSprinting ? sprintingSpeed : movementSpeed) * Input.GetAxisRaw("Vertical") : 0;
        float curSpeedY = canMove ? (isSprinting ? sprintingSpeed : movementSpeed) * Input.GetAxisRaw("Horizontal") : 0;

        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        // Move the controller
        charController.Move(moveDirection * Time.deltaTime);
    }

    void Look()
    {
        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * sensetivity;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * sensetivity, 0);
        }
    }

    void HeadBobbing()
    {
        // Player is moving
        if (moveDirection.magnitude > 0.1f)
        {
            timer += Time.deltaTime * bobbingSpeed;
            float newY = playerCamera.transform.localPosition.y + Mathf.Sin(timer) * bobbingAmount;
            playerCamera.transform.localPosition = new Vector3(playerCamera.transform.localPosition.x, newY, playerCamera.transform.localPosition.z);
        }
        else
        {
            // Idle
            float newY = playerCamera.transform.localPosition.y;
            playerCamera.transform.localPosition = new Vector3(playerCamera.transform.localPosition.x, newY, playerCamera.transform.localPosition.z);
        }
    }
}