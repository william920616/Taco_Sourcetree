using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SavePoint : MonoBehaviour
{
    public static Vector3 savePointPosition;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 記錄玩家經過的儲存點位置
            savePointPosition = transform.position;
        }
    }
}
