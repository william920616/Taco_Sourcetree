using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 控制物體沿著路徑移動的腳本
public class PathController : MonoBehaviour
{
    // 路徑上的各個目標點
    public Transform[] pathPoints;

    // 移動速度
    public float moveSpeed = 20f;

    // 當前目標點的索引
    private int currentTargetIndex = 0;

    // Start 方法在第一幀更新前被呼叫
    void Start()
    {

    }

    // Update 方法在每個幀都被呼叫
    void Update()
    {
        // 如果路徑上沒有目標點，則退出 Update 方法
        if (pathPoints.Length == 0)
            return;

        // 計算移動的距離，基於時間和速度
        float distanceToMove = moveSpeed * Time.deltaTime;

        // 使用 Vector3.Lerp 函數逐漸移動物體到目標位置
        transform.position = Vector3.Lerp(transform.position, pathPoints[currentTargetIndex].position, distanceToMove);

        // 如果物體到達目標點，則切換到下一個目標點
        if (Vector3.Distance(transform.position, pathPoints[currentTargetIndex].position) < 0.1f)
        {
            currentTargetIndex = (currentTargetIndex + 1) % pathPoints.Length;
        }
    }
}