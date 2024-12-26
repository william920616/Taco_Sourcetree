using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public float moveSpeed = 5.0f; // 移動速度

    void Update()
    {
        // 獲取左右移動的輸入
        float horizontalInput = Input.GetAxis("Horizontal");

        // 計算移動方向
        Vector3 movement = new Vector3(horizontalInput, 0, 0) * moveSpeed * Time.deltaTime;

        // 移動角色
        transform.Translate(movement);
    }
}
