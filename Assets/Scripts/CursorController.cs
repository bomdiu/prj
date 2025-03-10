using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    void Start()
    {
        // Ban đầu hiển thị con trỏ và không khóa
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        // Kiểm tra nếu phím Escape được nhấn
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Đảo ngược trạng thái hiển thị và khóa của con trỏ
            bool isCursorVisible = Cursor.visible;
            Cursor.visible = !isCursorVisible;
            Cursor.lockState = isCursorVisible ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }
}
