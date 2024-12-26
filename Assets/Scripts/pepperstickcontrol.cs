using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pepperstickcontrol : MonoBehaviour
{
    public float jumpForce = 5f; // 跳躍力度
    public float waitTime = 2f;   // 停頓時間

    private bool isJumping = false;

    void OnCollisionEnter(Collision collision)
    {
        // 假設這個物體的標籤是 "Player"
        if (collision.gameObject.CompareTag("Player") && !isJumping)
        {
            StartCoroutine(JumpAndRotate());
        }
    }

    private IEnumerator JumpAndRotate()
    {
        isJumping = true;

        // 給予玩家一個向上的力以實現跳躍
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // 等待指定的時間
        yield return new WaitForSeconds(waitTime);

        // 向Z軸旋轉90度
        transform.Rotate(0, 0, 90);

        isJumping = false; // 重置跳躍狀態
    }
}