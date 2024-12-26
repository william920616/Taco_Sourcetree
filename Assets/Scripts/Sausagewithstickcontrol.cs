using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SausageWithStickControl : MonoBehaviour
{
    public float rotationSpeed = 50f; // 自轉速度

    void Update()
    {
        // 每幀旋轉物體
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}