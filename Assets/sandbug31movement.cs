using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RobotMovement : MonoBehaviour
{
    public Vector3 pointA = new Vector3(15.07f, 1.87f, -0.11f);  // 第一個目標點
    public Vector3 pointB = new Vector3(15.07f, 1.87f, 34f);  // 第二個目標點
    public float moveSpeed = 5f; // 沙蟲的移動速度
    public float turnSpeed = 5f; // 沙蟲的旋轉速度（轉向的平滑度）
    private bool movingToB = true; // 控制沙蟲移動方向，true: 從 A 到 B，false: 從 B 到 A

    private bool isPlayerOnBack = false; // 玩家是否站在沙蟲背上
    private bool isMovingForward = true; // 控制沙蟲是否前進
    private bool isTurning = false;     // 控制沙蟲是否正在轉向
    private bool isMoving = true;
    private bool isRiding = false; // 玩家是否騎在沙蟲上
    private GameObject player; // 玩家物件
    private Rigidbody sandwormRb;       // 沙蟲的 Rigidbody
    private Transform PlayerDefTransform;

    void Start()
    {
        sandwormRb = GetComponent<Rigidbody>(); // 獲取沙蟲的 Rigidbody
        PlayerDefTransform = GameObject.FindGameObjectWithTag("Player").transform.parent;
        isRiding = false;
        // 設定初始位置為 pointA
        transform.position = pointA;
    }

    void Update()
    {
        // 移動沙蟲並自動轉向
        MoveBetweenPoints();

        // 如果玩家站在沙蟲背上，讓玩家跟隨沙蟲移動
        if (isPlayerOnBack && player != null)
        {
            FollowSandworm();
        }

        // 當玩家離開沙蟲背部時
        ExitBug();
    }

    // 移動沙蟲在 pointA 和 pointB 之間
    void MoveBetweenPoints()
    {
        Vector3 target = movingToB ? pointA : pointB;

        if (Vector3.Distance(transform.position, target) > 0.1f) // 用距離來檢查是否達到目標
        {
            // 平滑移動到目標點
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        }
        // 持續自動轉向
        RotateTowardsTarget(target);

        // 當到達目標點時，切換移動方向
        if (Vector3.Distance(transform.position, target) < 0.1f)  // 用距離來檢查是否達到目標
        {
            movingToB = !movingToB; // 變更方向，從 A 到 B 或 B 到 A
        }
    }

    // 讓沙蟲持續朝向目標方向自動轉向
    void RotateTowardsTarget(Vector3 target)
    {

        // 根據當前目標點，計算目標位置與當前位置之間的方向向量
        Vector3 direction = target - transform.position;
        direction.y = 0; // 只考慮水平面上的旋轉

        // 計算目標旋轉
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // 平滑地轉向目標旋轉
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    // 讓玩家跟隨沙蟲移動
    void FollowSandworm()
    {
        if (player != null)
        {
            // 讓玩家位置稍微高於沙蟲背部並跟隨沙蟲
            player.transform.position = transform.position + new Vector3(0f, 2.5f, 0f);
            player.transform.rotation = transform.rotation; // 讓玩家朝向和沙蟲一致
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player") && isRiding == false)
        {
            isPlayerOnBack = true;  // 玩家站上沙蟲
            player = collision.gameObject;  // 設置玩家物件
            player.transform.SetParent(transform);  // 讓玩家成為沙蟲的子物件
            isRiding = true;
        }
    }

    // 當玩家離開沙蟲背部時
    void ExitBug()
    {
        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && isRiding == true)
        {
            player.gameObject.transform.parent = null;
            isRiding = false;
            isPlayerOnBack = false;  // 玩家離開沙蟲
        }
    }
}
    
