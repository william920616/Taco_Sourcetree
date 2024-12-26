using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitController : MonoBehaviour
{
    private Rigidbody rb;
    private float initialRotationAngle = -90f; // 初始角度

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialRotationAngle = transform.rotation.eulerAngles.z; // 獲取初始角度
    }

    void Update()
    {
        // 在 Update 方法中根據你的需求來改變角度
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 將水果的角度增加 90 度
            transform.rotation = Quaternion.Euler(0f, 0f, initialRotationAngle + 90f);
        }
    }

}
