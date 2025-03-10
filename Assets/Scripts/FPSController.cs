using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
    // Tốc độ di chuyển của nhân vật
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 20f;

    // Camera và các thiết lập liên quan
    public Camera playerCamera;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    // Kiểm tra xem nhân vật có thể di chuyển không
    public bool canMove = true;

    // Các biến nội bộ
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    // Component CharacterController của nhân vật
    CharacterController characterController;

    void Start()
    {
        // Lấy component CharacterController
        characterController = GetComponent<CharacterController>();

        // Khóa con trỏ chuột ở giữa màn hình và ẩn nó
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        #region Di chuyển nhân vật
        // Lấy các vector cho hướng di chuyển
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Kiểm tra xem nhân vật có đang chạy hay không
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;

        // Kiểm tra xem nhân vật đang đứng trên mặt đất hay không
        if (characterController.isGrounded)
        {
            // Tính toán hướng di chuyển của nhân vật khi đứng trên mặt đất
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            // Nhảy nếu nhấn phím Space
            if (Input.GetButton("Jump") && canMove)
            {
                moveDirection.y = jumpPower;
            }
        }
        else
        {
            // Áp dụng trọng lực nếu nhân vật không đứng trên mặt đất
            moveDirection.x = (forward * curSpeedX).x + (right * curSpeedY).x;
            moveDirection.z = (forward * curSpeedX).z + (right * curSpeedY).z;
        }

        // Áp dụng trọng lực cho trục Y (rơi xuống khi ở trên không)
        moveDirection.y -= gravity * Time.deltaTime;

        // Di chuyển nhân vật
        characterController.Move(moveDirection * Time.deltaTime);
        #endregion

        #region Điều khiển camera
        if (canMove)
        {
            // Xử lý xoay camera theo trục Y dựa trên chuyển động của chuột
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

            // Xử lý xoay nhân vật theo trục X
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
        #endregion
    }
}
