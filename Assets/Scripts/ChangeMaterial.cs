using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    public Material newMaterial; // 要改變成的材質
    public static bool hasChangedMaterial = true; // 標誌，用於追踪是否已經改變過材質

    private void OnTriggerEnter(Collider other)
    {
        // 檢查碰撞的對象是否標記為 "spicy" 且還沒有改變過材質
        if (other.gameObject.CompareTag("spicy") && hasChangedMaterial == true)
        {
            StartCoroutine(changecolor());
        }
    }
    IEnumerator changecolor()
    {
        yield return new WaitForSeconds(1f);
        // 取得物體的 Renderer 組件
        Renderer renderer = GetComponent<Renderer>();
        // 將材質改變成新的材質
        renderer.material = newMaterial;
        // 設置標誌為已改變
        hasChangedMaterial = false;
    }
}
