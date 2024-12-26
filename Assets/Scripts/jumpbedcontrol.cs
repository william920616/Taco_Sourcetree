using System.Collections;
using UnityEngine;

public class jumpbedcontrol : MonoBehaviour
{
    public float jumpForce = 100f; // 增加跳躍力度，調整這個數值來改變跳躍的高度
    private bool isJumping = false; // 用來確保每次只有一次跳躍
    private GameObject player; // 用來保存玩家物件

    private void OnCollisionEnter(Collision collision)
    {
        // 假設碰撞物體的標籤是 "Player" 且沒有正在跳躍時才觸發
        if (collision.gameObject.CompareTag("Player") && !isJumping)
        {
            // 保存玩家物件
            player = collision.gameObject;
            // 開始跳躍協程
            StartCoroutine(Jump(collision));
        }
    }

    private IEnumerator Jump(Collision playerCollision)
    {
        // 延遲一點時間再執行跳躍，這樣可以避免過早觸發跳躍
        yield return new WaitForSeconds(0.1f);

        // 設置為正在跳躍，防止重複跳躍
        isJumping = true;

        // 從玩家物件獲取 Rigidbody 元件
        Rigidbody rb = playerCollision.gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // 重置垂直速度以確保跳躍效果
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); // 重設垂直速度，防止多次跳躍

            // 給玩家施加向上的跳躍力
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // 跳躍完成後，設置為可以再次跳躍
        yield return new WaitForSeconds(1f); // 可以根據需要調整這個時間，控制跳躍頻率
        isJumping = false; // 跳躍完成後，允許再次跳躍
    }
}