using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotationSpeed = 5.0f;

    void Update()
    {
        // 检测鼠标右键是否被按下
        if (Input.GetMouseButton(1))
        {
            // 获取鼠标在屏幕上的移动量
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // 根据鼠标移动量调整相机的旋转
            transform.Rotate(Vector3.up * mouseX * rotationSpeed, Space.World);
            transform.Rotate(Vector3.left * mouseY * rotationSpeed, Space.Self);
        }
    }
}










