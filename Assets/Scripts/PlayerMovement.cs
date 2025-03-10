using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator playerAnim;  // Animator component
    public Rigidbody playerRigid;  // Rigidbody component
    public float moveSpeed = 5f;  // Walking speed
    public float runSpeed = 7f;  // Running speed
    public float jumpForce = 7f;  // Jumping force
    public float extraGravity = 10f;  // Extra gravity force while in the air

    public Transform cameraTransform;  // First-Person Camera
    public float mouseSensitivity = 100f;  // Mouse sensitivity for camera movement
    private float xRotation = 0f;  // X rotation for camera (up/down)

    private bool isGrounded = true;  // Check if the player is on the ground
    private bool hasJumped = false;  // Check if the player has jumped

    void Start()
    {
        // Lock the cursor to the center of the screen and hide it
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        // Kiểm tra phím di chuyển
        bool moveForward = Input.GetKey(KeyCode.W);
        bool moveBackward = Input.GetKey(KeyCode.S);
        
        // Xác định hướng di chuyển theo hướng camera
        Vector3 forwardDirection = cameraTransform.forward;
        forwardDirection.y = 0;  // Đảm bảo không di chuyển lên/xuống
        forwardDirection.Normalize();

        if (moveBackward)
        {
            forwardDirection = -forwardDirection;  // Đảo ngược hướng di chuyển
        }

        // Chỉ di chuyển nếu người chơi nhấn W hoặc S
        if (moveForward || moveBackward)
        {
            // Di chuyển nhân vật
            float currentSpeed = (Input.GetKey(KeyCode.LeftShift) && moveForward) ? runSpeed : moveSpeed;  // Chỉ chạy khi nhấn Left Shift và W
            playerRigid.MovePosition(transform.position + forwardDirection * currentSpeed * Time.fixedDeltaTime);
        }

        // Cập nhật trạng thái animation
        UpdateAnimations(moveForward, moveBackward);
        
        // Thêm lực rơi khi nhân vật đang trên không
        if (!isGrounded && playerRigid.velocity.y < 0)
        {
            playerRigid.AddForce(Vector3.down * extraGravity, ForceMode.Acceleration);
        }
    }

    void Update()
    {
        // Điều khiển camera xoay theo chuột
        HandleMouseLook();

        // Kiểm tra nhảy
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !hasJumped)
        {
            Jump();
        }
    }

    // Xử lý quay camera theo chuột
    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Xoay camera theo trục X (lên/xuống)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);  // Giới hạn góc nhìn lên/xuống trong khoảng -90 đến 90 độ

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Xoay nhân vật theo trục Y (trái/phải) dựa trên chuột
        transform.Rotate(Vector3.up * mouseX);
    }

    void Jump()
    {
        // Thực hiện nhảy
        playerRigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;  // Không còn trên mặt đất
        hasJumped = true;  // Đánh dấu là đã nhảy
    }

    // Cập nhật trạng thái animation
    void UpdateAnimations(bool moveForward, bool moveBackward)
    {
        if (isGrounded)
        {
            if (moveForward && Input.GetKey(KeyCode.LeftShift))
            {
                playerAnim.SetBool("isRunning", true);
                playerAnim.SetBool("isWalking", false);
            }
            else if (moveForward)
            {
                playerAnim.SetBool("isWalking", true);
                playerAnim.SetBool("isRunning", false);
            }
            else if (moveBackward)
            {
                playerAnim.SetBool("isWalking", true);
                playerAnim.SetBool("isRunning", false);
            }
            else
            {
                playerAnim.SetBool("isWalking", false);
                playerAnim.SetBool("isRunning", false);
            }
        }
    }

    // Kiểm tra khi nhân vật chạm đất để có thể nhảy tiếp
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))  // Tag của mặt đất là "Ground"
        {
            isGrounded = true;  // Nhân vật đã chạm đất
            hasJumped = false;  // Reset trạng thái đã nhảy
        }
    }
}
