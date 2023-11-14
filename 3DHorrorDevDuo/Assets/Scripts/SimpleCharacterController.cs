using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    public float speed = 5.0f; // Movement speed of the character
    public float mouseSensitivity = 2.0f; // Mouse sensitivity for camera movement
    public float jumpForce = 8.0f; // Force applied when jumping
    public float gravity = 20.0f; // Gravity for the character

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;

    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to enable looking around
    }

    void Update()
    {
        // Camera movement based on mouse input
        rotationX -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f); // Limit vertical rotation to avoid flipping

        rotationY += Input.GetAxis("Mouse X") * mouseSensitivity;

        // Apply rotation to the character and camera
        transform.localRotation = Quaternion.Euler(0, rotationY, 0);
        Camera.main.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

        if (controller.isGrounded)
        {
            // Get horizontal and vertical input
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            // Calculate movement direction
            Vector3 move = transform.TransformDirection(new Vector3(horizontal, 0.0f, vertical));
            moveDirection = move * speed;

            // Allowing the character to jump
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpForce;
            }
        }

        // Apply gravity to the character controller
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the character
        controller.Move(moveDirection * Time.deltaTime);
    }
}
