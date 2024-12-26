using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RotateShield : MonoBehaviour
{
     Transform boss; // 參考到 Boss 物件的變數
    public float rotationSpeed = 50f; // 旋轉速度，你可以調整這個值以改變旋轉速度

    private Vector3 axis = Vector3.up; // 旋轉軸

    void Update()
    {
        boss = GameObject.FindGameObjectWithTag("Boss").transform;
        if (boss != null) // 確保 Boss 物件存在
        {
            // 將盾牌的位置設置為 Boss 的位置
            transform.position = boss.position;

            // 以旋轉軸為中心旋轉盾牌
            transform.Rotate(axis, rotationSpeed * Time.deltaTime);
        }
    }
}

