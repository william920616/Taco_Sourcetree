using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // 主角位置
    public float distance = 8f; // 摄像机到主角的距离
    public float height = 4.5f; // 摄像机的高度
    public float rotationSpeed = 50f; // 旋转速度

    private float yaw = 0.0f; // 水平旋转（控制左右旋转）
    private float pitch = 0.0f; // 垂直旋转（控制上下旋转）

    // 控制上下旋转的限制角度，防止相机翻转
    public float pitchMin = 0f; // 垂直旋转的最小角度（設為 0，防止向下看）
    public float pitchMax = 60f;  // 垂直旋转的最大角度（向上看）

    //private void Start()
    //{
    //    // 锁定鼠标并隐藏光标
    //    Cursor.lockState = CursorLockMode.Locked;
    //    Cursor.visible = false;
    //}

    private void LateUpdate()
    {
        if (player == null)
        {
            return;
        }

        // 计算水平旋转角度（滑鼠左右移動）
        yaw += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;

        // 计算垂直旋转角度（滑鼠上下移動）
        pitch -= Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, pitchMin, pitchMax); // 限制垂直旋转的范围，不能向下看

        // 计算摄像机的位置
        Vector3 direction = Quaternion.Euler(pitch, yaw, 0) * new Vector3(0, height, -distance);
        Vector3 targetPosition = player.position + direction;

        // 更新相机的位置
        transform.position = targetPosition;

        // 使相机始终看向主角（可以調整視角的高度）
        transform.LookAt(player.position + Vector3.up * height);
    }

    // 如果你有需要在某些时候解除鼠标锁定并显示光标，可以在其他地方调用以下方法
    //public void UnlockCursor()
    //{
    //    Cursor.lockState = CursorLockMode.None;
    //    Cursor.visible = true;
    //}

    //public void LockCursor()
    //{
    //    Cursor.lockState = CursorLockMode.Locked;
    //    Cursor.visible = false;
    //}
}

