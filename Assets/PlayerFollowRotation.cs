using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowRotation : MonoBehaviour
{
    public Transform rotatingObject; // 旋轉的物體
    private Vector3 offset; // 玩家和物體之間的相對位置
    private bool isOnRotatingObject = false; // 玩家是否在旋轉物體上

    void Start()
    {
        // 確保 rotatingObject 已經賦值
        if (rotatingObject == null)
        {
            Debug.LogError("RotatingObject is not assigned!");
        }
    }

    void Update()
    {
        // 當玩家在旋轉物體上時，跟隨物體的旋轉
        if (isOnRotatingObject && rotatingObject != null)
        {
            // 玩家保持在物體的相對位置
            transform.position = rotatingObject.position + offset;

            // 讓玩家跟隨物體旋轉（只旋轉 Y 軸）
            transform.rotation = Quaternion.Euler(0, rotatingObject.rotation.eulerAngles.y, 0);
        }
    }

    // 當玩家進入旋轉物體的觸發區域時，開始跟隨旋轉
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RotatingObject"))
        {
            // 記錄玩家與物體之間的初始相對位置
            offset = transform.position - rotatingObject.position;
            isOnRotatingObject = true; // 開始跟隨旋轉物體
            Debug.Log("Player started following rotation.");
        }
    }

    // 當玩家在旋轉物體的觸發區域內時，持續跟隨
    private void OnTriggerStay(Collider other)
    {
        if (isOnRotatingObject && other.gameObject.CompareTag("RotatingObject"))
        {
            // 玩家保持在物體的相對位置
            transform.position = rotatingObject.position + offset;

            // 讓玩家跟隨物體旋轉（只旋轉 Y 軸）
            transform.rotation = Quaternion.Euler(0, rotatingObject.rotation.eulerAngles.y, 0);
        }
    }

    // 當玩家離開旋轉物體的觸發區域時，停止跟隨
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("RotatingObject"))
        {
            isOnRotatingObject = false; // 停止跟隨旋轉物體
            Debug.Log("Player stopped following rotation.");
        }
    }
}
