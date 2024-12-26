using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObject1 : MonoBehaviour
{
    public float rotationSpeed = 30f; // 設定旋轉速度

    void Update()
    {
        // 每幀旋轉物體
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
