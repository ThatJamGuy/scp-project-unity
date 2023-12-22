using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{


    [Header("Player Speeds")]
    [SerializeField] private float walkSpeed = 5.0f;
    [SerializeField] private float sprintSpeed = 10.0f;

    [Header("Looking/Movement")]
    [SerializeField] private Camera playerCamera;
    public bool isSprinting = false;
    [SerializeField] private float lookSpeed = 2.0f;
    [SerializeField] private float lookXLimit = 45.0f;
    [SerializeField] private float gravity = 20.0f;

    [Header("Headbobbing")]
    [SerializeField] private float bobbingSpeed = 0.18f;
    [SerializeField] private float bobbingAmount = 0.2f;
    [SerializeField] private GameObject groundDetect;

    [HideInInspector] public bool canMove = true;

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private float headbobCycle = 0.0f;
    private Vector3 originalCameraPosition;
    private float originalYPos;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        originalYPos = playerCamera.transform.localPosition.y;
        originalCameraPosition = playerCamera.transform.localPosition;
    }

    void Update()
    {
        HandleMovementInput();
        ApplyGravity();
        MoveCharacterController();
        RotatePlayerAndCamera();
        ApplyHeadbobbing();

        groundDetect.transform.position = playerCamera.transform.position;
    }

    void HandleMovementInput()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        isSprinting = isRunning;

        float curSpeedX = canMove ? (isRunning ? sprintSpeed : walkSpeed) * Input.GetAxisRaw("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? sprintSpeed : walkSpeed) * Input.GetAxisRaw("Horizontal") : 0;

        moveDirection = (forward * curSpeedX) + (right * curSpeedY);
    }

    void ApplyGravity()
    {
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
    }

    void MoveCharacterController()
    {
        characterController.Move(moveDirection * Time.deltaTime);
    }

    void RotatePlayerAndCamera()
    {
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }

    void ApplyHeadbobbing()
    {
        if (Mathf.Abs(moveDirection.x) > 0.01f || Mathf.Abs(moveDirection.z) > 0.01f)
        {
            if (isSprinting)
                bobbingSpeed = 10;
            else
                bobbingSpeed = 7;

            // Update headbob cycle
            headbobCycle += bobbingSpeed * Time.deltaTime;

            // Calculate headbob position
            float yPos = originalYPos + Mathf.Sin(headbobCycle) * bobbingAmount;

            // Apply headbob to camera
            playerCamera.transform.localPosition = new Vector3(
                playerCamera.transform.localPosition.x,
                yPos,
                playerCamera.transform.localPosition.z
            );
        }
        else
        {
            // Save the current headbob position if not moving
            originalCameraPosition = playerCamera.transform.localPosition;

            // Lerp camera position to the original position
            playerCamera.transform.localPosition = Vector3.Lerp(
                playerCamera.transform.localPosition,
                originalCameraPosition,
                Time.deltaTime * bobbingSpeed
            );
        }
    }
}