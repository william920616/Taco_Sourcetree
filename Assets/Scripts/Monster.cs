using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float speed = 5.0f; // 移動速度
    public float stoppingDistance = 2f; // 停止距離
    public float rotationSpeed = 10.0f; // 旋轉速度

    private Transform player; // 玩家的Transform

    void Start()
    {
        // 找到玩家
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (player == null)
        {
            Debug.LogWarning("找不到玩家物件！請確保在場景中有標記為 'Player' 的物件。");
        }
    }

    void Update()
    {
        // 如果玩家為空，發出警告並退出更新
        if (player == null)
        {
            return;
        }

        // 計算怪物與玩家的距離
        float distance = Vector3.Distance(transform.position, player.position);

        // 如果距離大於停止距離，向玩家移動並朝向玩家
        if (distance > stoppingDistance)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            // 轉向玩家的方向
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            // 如果距離小於等於停止距離，只朝向玩家但不移動
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerController.HeartNum -= 1;
            Destroy(this.gameObject);
        }
    }
}




