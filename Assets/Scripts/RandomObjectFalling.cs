using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObjectFalling : MonoBehaviour
{
    public GameObject[] objectsToDrop; // 要掉落的物體
    public float minX = -28.5f; // X 軸最小值
    public float maxX = 24f;  // X 軸最大值
    public float minZ = -110f; // Z 軸最小值
    public float maxZ = -150f;  // Z 軸最大值
    public float fallSpeed = 5f; // 設定物體下降的速度
    public float respawnTime = 2f; // 重新生成時間

    void Start()
    {
        // 開始掉落協程
        StartCoroutine(FallRoutine());
    }

    IEnumerator FallRoutine()
    {
        while (true)
        {
            // 在指定範圍內生成隨機位置
            float randomX = Random.Range(minX, maxX);
            float randomZ = Random.Range(minZ, maxZ);

            // 隨機選擇要掉落的物體
            GameObject objectToDrop = objectsToDrop[Random.Range(0, objectsToDrop.Length)];

            // 實例化要掉落的物體並設定初始位置
            GameObject droppedObject = Instantiate(objectToDrop, new Vector3(randomX, transform.position.y, randomZ), Quaternion.identity);

            // 控制物體掉落
            Rigidbody rb = droppedObject.GetComponent<Rigidbody>();
            rb.velocity = Vector3.down * fallSpeed;

            // 等待物體掉落
            yield return new WaitUntil(() => rb.velocity == Vector3.zero);

            // 销毁物体
            Destroy(droppedObject);

            // 等待一段時間後再重新生成並掉落
            yield return new WaitForSeconds(respawnTime);
        }
    }
}
