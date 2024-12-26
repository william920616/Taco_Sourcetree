using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInRange : MonoBehaviour
{
    public Transform player;  // 玩家物件
    public float range = 5f;  // 設置範圍大小
    public static bool isPlayerInRange = false;

    void Update()
    {
        // 這裡使用距離檢查來輔助確保進出範圍的檢測
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= range)
        {
            if (!isPlayerInRange)
            {
                isPlayerInRange = true;
                
            }
        }
        else
        {
            if (isPlayerInRange)
            {
                isPlayerInRange = false;
                
            }
        }
    }

    // 可以繼續使用 OnTriggerEnter 和 OnTriggerExit 來補充檢測
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
           
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
           
        }
    }
}
