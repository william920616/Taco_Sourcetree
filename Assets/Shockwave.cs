using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
    public float radius = 5f;               // 衝擊波範圍
    public float force = 10f;               // 衝擊波推動的力量
    public LayerMask affectedLayer;         // 受影響的物體層級
    public float duration = 0.5f;           // 衝擊波持續時間

    private void Start()
    {
        // 呼叫衝擊波效果
        CreateShockwave();
    }

    void CreateShockwave()
    {
        // 使用Physics.OverlapSphere找出範圍內的所有物體
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, affectedLayer);

        foreach (Collider col in colliders)
        {
            // 檢查物體是否有Rigidbody組件
            Rigidbody rb = col.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // 計算從衝擊波中心到物體的方向
                Vector3 direction = col.transform.position - transform.position;
                direction.Normalize();

                // 計算距離衝擊波中心的距離
                float distance = direction.magnitude;
                float forceMultiplier = Mathf.Clamp01(1 - (distance / radius));  // 力量隨著距離減弱

                // 將衝擊波的力量加到Rigidbody上
                rb.AddForce(direction * force * forceMultiplier, ForceMode.Impulse);
            }
        }

        // 可以選擇延遲銷毀物件，這裡暫時不設
        Destroy(gameObject, duration);
    }

    // 可以選擇在衝擊波可視化範圍時顯示一個範圍
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

