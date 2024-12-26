using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObject : MonoBehaviour
{
    public float rotationSpeed = 30f; // 旋轉速度
    public float rotationRadius = 1f; // 旋轉半徑

    private Vector3 centerPoint; // 旋轉的中心點

    void Start()
    {
        // 設置旋轉的中心點為物體的當前位置
        centerPoint = transform.position;
    }

    void Update()
    {
        // 根據設置的旋轉速度進行旋轉
        float angle = rotationSpeed * Time.deltaTime;

        // 讓物體繞 X 軸旋轉
        transform.RotateAround(centerPoint, Vector3.right, angle);

        // 保持物體距離中心點一定的半徑
        Vector3 direction = (transform.position - centerPoint).normalized;
        transform.position = centerPoint + direction * rotationRadius;
    }
}
