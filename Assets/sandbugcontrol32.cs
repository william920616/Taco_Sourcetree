using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class sandbugcontrol32 : MonoBehaviour
{
    public Vector3 pointA = new Vector3(15.07f, 1.87f, -0.11f);  // 第一個目標點
    public Vector3 pointB = new Vector3(15.07f, 1.87f, 34f);  // 第二個目標點
    public float moveSpeed = 5f;        // 沙蟲的移動速度
    public float turnSpeed = 5f;        // 沙蟲的旋轉速度（轉向的平滑度）
    private bool movingToB = true;      // 控制沙蟲移動方向，true: 從 A 到 B，false: 從 B 到 A
    private bool isTurning = false;     // 控制沙蟲是否正在轉向
    private bool isMoving = true;       // 沙蟲是否在移動
    private bool isRiding = false;      // 玩家是否騎在沙蟲上
    private GameObject player;          // 玩家物件
    private Rigidbody sandwormRb;       // 沙蟲的 Rigidbody
    private Transform PlayerDefTransform;

    void Start()
    {
        sandwormRb = GetComponent<Rigidbody>(); // 獲取沙蟲的 Rigidbody
        PlayerDefTransform = GameObject.FindGameObjectWithTag("Player").transform.parent;
        isRiding = false;
        // 設定初始位置為 pointA
        //transform.position = pointA;
    }

    void Update()
    {
        // 如果沙蟲正在移動，進行移動操作
        if (isMoving)
        {
            MoveBetweenPoints();
        }
    }

    // 移動沙蟲在 pointA 和 pointB 之間
    void MoveBetweenPoints()
    {
        Vector3 target = movingToB ? pointB : pointA;

        // 如果沙蟲沒有達到目標點，繼續移動
        if (Vector3.Distance(transform.position, target) > 0.1f)
        {
            Debug.Log("沒有");
            // 沙蟲向目標點移動
            transform.Translate(Vector3.back* moveSpeed * Time.deltaTime);
            
        }

        // 當沙蟲到達目標點時，切換移動方向
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            Debug.Log("有");
            movingToB = !movingToB; // 變更方向
            StopMoving(); // 停止前進
            StartCoroutine(TurnAround()); // 開始回頭轉向
        }
    }

    // 當沙蟲碰到障礙物時，觸發回頭動作
    void OnTriggerEnter(Collider other)
    {
        //// 當沙蟲碰到障礙物時，開始回頭並停止移動
        //if (!isTurning && col.collider.CompareTag("Wall"))
        //{
        //    StopMoving(); // 停止前進
        //    StartCoroutine(TurnAround()); // 開始回頭轉向
        //}
        if (other.CompareTag("stoptaco"))
        {

        }
    }

    // 停止前進
    void StopMoving()
    {
        isMoving = false;
    }

    // 使用協程平滑轉向 180 度
    IEnumerator TurnAround()
    {
        isTurning = true;  // 設置為正在轉向

        // 目標旋轉角度
        float targetRotation = transform.eulerAngles.y + 180f;

        // 平滑轉向
        while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, targetRotation)) > 0.1f)
        {
            float step = turnSpeed * Time.deltaTime;
            float newRotation = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotation, step);
            transform.rotation = Quaternion.Euler(0f, newRotation, 0f);
            yield return null;
        }

        // 完成轉向後繼續前進
        isMoving = true;
        isTurning = false;  // 轉向結束
    }
}

