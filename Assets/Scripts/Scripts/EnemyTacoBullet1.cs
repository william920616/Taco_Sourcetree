using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTacoBullet1 : MonoBehaviour
{
    public float speed = 25f;  // 子彈的速度
    private Vector3 direction; // 子彈的發射方向

    // 設置子彈的方向
    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection.normalized; // 確保方向是單位向量
        if (direction == Vector3.zero)
        {
            Debug.LogWarning("Bullet direction is zero!");  // 確保方向不為零向量
        }
    }

    // 每幀更新子彈的位置
    void Update()
    {
        if (direction != Vector3.zero)  // 確保方向不為零向量
        {
            transform.position += direction * speed * Time.deltaTime;  // 根據方向移動
        }
    }
}
