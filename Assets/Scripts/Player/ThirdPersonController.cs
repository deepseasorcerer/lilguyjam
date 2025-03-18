using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    // Public variables for tweaking in the Inspector
    public float speed = 5f;              // Normal movement speed
    public float sprintMultiplier = 1.5f; // Multiplier for sprint speed
    public float jumpSpeed = 5f;          // Speed at which the player jumps
    public float rotationSpeed = 10f;     // Rotation smoothing speed
    public float gravity = 9.81f;         // Gravity strength

    [Header("Assign the Camera Here")]
    public Camera playerCamera;           // Drag your camera into this field in the Inspector

    private CharacterController characterController;
    private float verticalVelocity = 0f;

    void Start()
    {
        // Get the CharacterController component attached to this GameObject
        characterController = GetComponent<CharacterController>();

        // Check if the camera is assigned; if not, try to use Camera.main as a fallback
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
            if (playerCamera == null)
            {
                Debug.LogError("No camera assigned and no main camera found! Please drag a camera into the 'Player Camera' field in the Inspector.", this);
            }
            else
            {
                Debug.LogWarning("No camera assigned in the Inspector, using main camera as fallback.", this);
            }
        }
    }

    void Update()
    {
        // Skip movement if no camera is available
        if (playerCamera == null)
        {
            return;
        }

        // Get player input from horizontal (A/D or Left/Right) and vertical (W/S or Up/Down) axes
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Check if the sprint key is pressed (Left Shift)
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);

        // Determine the effective speed based on whether sprinting or not
        float effectiveSpeed = isSprinting ? speed * sprintMultiplier : speed;

        // Get the camera's forward and right vectors
        Vector3 camForward = playerCamera.transform.forward;
        Vector3 camRight = playerCamera.transform.right;

        // Project vectors onto the horizontal plane (remove y component)
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        // Calculate movement direction based on camera orientation and input
        Vector3 moveDirection = camForward * vertical + camRight * horizontal;
        if (moveDirection.magnitude > 1) moveDirection.Normalize();

        // Rotate the player to face the movement direction
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Calculate horizontal velocity
        Vector3 horizontalVelocity = moveDirection * effectiveSpeed;

        // Handle jump input
        if (Input.GetKeyDown(KeyCode.Space) && characterController.isGrounded)
        {
            verticalVelocity = jumpSpeed;
        }

        // Apply gravity and keep character grounded
        if (characterController.isGrounded)
        {
            if (verticalVelocity < 0)
            {
                verticalVelocity = -0.1f; // Small downward force to stay grounded
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime; // Accumulate downward velocity
        }

        // Combine horizontal and vertical motion
        Vector3 motion = horizontalVelocity + Vector3.up * verticalVelocity;

        // Move the character
        characterController.Move(motion * Time.deltaTime);
    }
}