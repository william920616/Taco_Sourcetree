using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss34 : MonoBehaviour
{
    // 定義角度範圍和步長
    public float startAngle = -45f;   // 發射角度的起始位置
    public float endAngle = 45f;      // 發射角度的結束位置
    public Animator Anim;
    public Transform player; // 玩家位置
    public NavMeshAgent navAgent; // 导航代理组件
    public GameObject shockwavePrefab;  // 衝擊波預製物件
    public GameObject[] itemsToDrop;   // 預設物品
    public GameObject bullet;
    public GameObject bullet2;
    public GameObject triggerfloor;
    public GameObject shield;
    public Transform[] firepoint;  // 用於發射的各個 firepoint
    public Vector3 dropAreaMin;   // 掉落範圍最小點
    public Vector3 dropAreaMax;   // 掉落範圍最大點
    public int minItemsToDrop = 7;    // 每次掉落的最小物品數量
    public int maxItemsToDrop = 9;    // 每次掉落的最大物品數量                                  
    public int shootCount = 0; // 設置一個計數器來追踪發射的組數
    public int maxShootCount = 4; // 設置最大發射組數為 4
    public float dropInterval = 1.5f;  // 每次掉落間隔時間
    public float coneAngle = 30f;  // 扇形範圍角度，例如：30 度
    public int angleStep = 10;  // 每次發射的角度間隔，例如：10度

    private bool istriggerfloor = false;
    private bool isHurtShieldActive = false; // 用來控制協程是否正在執行
    private bool isDropping = true; // 控制掉落是否繼續
    private bool isbroken; // 護盾是否被擊破
    private bool ishurt;   // 是否受傷

    void Start()
    {
        isbroken = false;
        ishurt = false;
        Anim = GetComponent<Animator>();
        // 開始協程，讓它無限掉落
        StartCoroutine(TriggerShockwaveAndDropItem());
        //StartCoroutine(Attack2());  // 根據需要呼叫 Attack1 或 Attack2
    }

    void Update()
    {
        // 檢查護盾是否已經被禁用
        if (!shield.activeSelf)
        {
            // 這裡執行物件被禁用時的邏輯
            Debug.Log("護盾已被禁用！");
        }
        else
        {
            // 這裡執行物件啟用時的邏輯
            Debug.Log("護盾處於啟用狀態！");
        }

        // 可以處理進一步的邏輯，例如觸發受傷狀態
        if (!shield.activeSelf && isHurtShieldActive && !ishurt)  // 盾牌已破且未處於受傷狀態
        {
            StartCoroutine(Hurtshield());  // 假設這裡有處理受傷邏輯的協程
        }
        else if (!shield.activeSelf && isHurtShieldActive && !ishurt)
        {
            // 當 shield 被禁用且需要執行受傷邏輯時
            Debug.Log("護盾已破且未處於受傷狀態！");
            StartCoroutine(Hurtshield());  // 假設這裡有處理受傷邏輯的協程
        }
        if (triggerfork34.isstopknife == true || triggerfork35.isstopknife == true)
        {
            navAgent.SetDestination(player.position); // 设置目标为玩家位置
            
        }
        if (triggerfork34.isstopknife == true)
        {
            isDropping = false;
        }
        if(triggerfork35.isstopknife == true)
        {
            isDropping = false;
        }
    }

    // 用於掉落物品的函數
    public void DropItem()
    {
        if (itemsToDrop.Length == 0)
        {
            Debug.LogWarning("No items to drop!");
            return;
        }

        int itemsToDropCount = Random.Range(minItemsToDrop, maxItemsToDrop + 1);

        for (int i = 0; i < itemsToDropCount; i++)
        {
            int randomIndex = Random.Range(0, itemsToDrop.Length);
            GameObject itemToDrop = itemsToDrop[randomIndex];

            if (itemToDrop == null)
            {
                Debug.LogWarning("Item to drop is null!");
                continue;
            }

            float randomX = Random.Range(dropAreaMin.x, dropAreaMax.x);
            float randomY = Random.Range(dropAreaMin.y, dropAreaMax.y);
            float randomZ = Random.Range(dropAreaMin.z, dropAreaMax.z);
            Vector3 dropPosition = new Vector3(randomX, randomY, randomZ);

            Instantiate(itemToDrop, dropPosition, Quaternion.identity);
        }
    }

    // 觸發衝擊波和掉落物品的協程
    IEnumerator TriggerShockwaveAndDropItem()
    {
        shield.SetActive(false);
        yield return new WaitForSeconds(2f);
        shield.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        if (shockwavePrefab != null)
        {
            Instantiate(shockwavePrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Shockwave prefab is missing!");
        }

        yield return new WaitForSeconds(1f);

        while (isDropping)
        {
            DropItem();
            yield return new WaitForSeconds(dropInterval);
        }
        StartCoroutine(Walk());
    }

    IEnumerator Walk()
    {
        Anim.SetBool("IsWalking", true);
        yield return new WaitForSeconds(2f);
        if(PlayerInRange.isPlayerInRange == true)
        {
            StartCoroutine(Attack1());
        }
    }
    // Attack1：扇形發射 bullet 子彈
    IEnumerator Attack1()
    {
        // 設置攻擊動畫
        Anim.SetBool("IsAttack1", true);
        navAgent.isStopped = true; // 停止导航代理
        yield return new WaitForSeconds(3f); // 等待3秒鐘，模擬準備過程

        // 發射直到達到最大組數
        while (shootCount < maxShootCount)
        {
            // 扇形發射子彈
            for (float angle = startAngle; angle <= endAngle; angle += angleStep)
            {
                // 計算子彈的發射方向
                Vector3 direction = firepoint[0].forward;
                direction = Quaternion.Euler(0, angle, 0) * direction;  // 旋轉向量來實現扇形發射

                // 創建子彈並設置其發射方向
                GameObject bulletInstance = Instantiate(bullet, firepoint[0].position, firepoint[0].rotation);

                // 設置子彈的發射方向
                EnemyTacoBullet1 bulletScript = bulletInstance.GetComponent<EnemyTacoBullet1>();
                if (bulletScript != null)
                {
                    bulletScript.SetDirection(direction);  // 設置子彈的發射方向
                }
                else
                {
                    Debug.LogWarning("EnemyTacoBullet1 script not found!");
                }

                // 等待 3 秒後銷毀該子彈
                Destroy(bulletInstance, 3f);
            }

            // 增加計數器
            shootCount++;

            // 發射完成後恢復導航
            Anim.SetBool("IsAttack1", false); // 停止攻擊動畫
            yield return new WaitForSeconds(4f);
            navAgent.isStopped = false; // 開啟导航代理
            yield return new WaitForSeconds(1f); // 等待一段時間再發射下一組
            Debug.Log(shootCount);
            // 檢查是否還有更多組數要發射
            if (shootCount < maxShootCount)
            {
                // 當前組結束後再重新開始下一組的發射
                Anim.SetBool("IsAttack1", true); // 重新啟動攻擊動畫
                navAgent.isStopped = true; // 停止導航
                yield return new WaitForSeconds(3f); // 等待3秒鐘，模擬準備過程
            }
            
        }
        ishurt = false;
        isHurtShieldActive = true;
        Debug.Log("可以攻擊");
    }

    // Attack2：圓形彈幕發射 bullet2 子彈
    IEnumerator Attack2()
    {
        Anim.SetBool("IsAttack2", true);
        yield return new WaitForSeconds(0.5f);
        // 每次增加 15 度，直到發射完 360 度
        int numShots = 360 / 15;  // 一圈需要發射的子彈數量（360度 / 15度間隔）

        // 設定每個發射點的子彈數量
        for (int i = 0; i < numShots; i++)
        {
            // 計算從 firepoint[1] 發射的角度 (順時針方向)
            float angle1 = i * 15f;  // 每次增加 15 度
            float radian1 = angle1 * Mathf.Deg2Rad;
            Vector3 bulletDirection1 = new Vector3(Mathf.Cos(radian1), 0, Mathf.Sin(radian1));

            // 從 firepoint[1] 發射子彈
            GameObject bulletInstance1 = Instantiate(bullet2, firepoint[1].position, firepoint[1].rotation);
            EnemyTacoBullet1 bulletScript1 = bulletInstance1.GetComponent<EnemyTacoBullet1>();
            if (bulletScript1 != null)
            {
                bulletScript1.SetDirection(bulletDirection1);
            }
            else
            {
                Debug.LogWarning("EnemyTacoBullet1 script not found!");
            }

            // 等待 3 秒後銷毀該子彈
            Destroy(bulletInstance1, 3f);

            // 計算從 firepoint[2] 發射的角度 (逆時針方向)
            float angle2 = -i * 15f;  // 每次減少 15 度，形成逆時針方向
            float radian2 = angle2 * Mathf.Deg2Rad;
            Vector3 bulletDirection2 = new Vector3(Mathf.Cos(radian2), 0, Mathf.Sin(radian2));

            // 從 firepoint[2] 發射子彈
            GameObject bulletInstance2 = Instantiate(bullet2, firepoint[2].position, firepoint[2].rotation);
            EnemyTacoBullet1 bulletScript2 = bulletInstance2.GetComponent<EnemyTacoBullet1>();
            if (bulletScript2 != null)
            {
                bulletScript2.SetDirection(bulletDirection2);
            }
            else
            {
                Debug.LogWarning("EnemyTacoBullet1 script not found!");
            }

            // 等待 3 秒後銷毀該子彈
            Destroy(bulletInstance2, 3f);

            // 計算從 firepoint[3] 發射的角度 (再次增加 15 度)
            float angle3 = i * 15f;  // 每次增加 15 度
            float radian3 = angle3 * Mathf.Deg2Rad;
            Vector3 bulletDirection3 = new Vector3(Mathf.Cos(radian3), 0, Mathf.Sin(radian3));

            // 從 firepoint[3] 發射子彈
            GameObject bulletInstance3 = Instantiate(bullet2, firepoint[3].position, firepoint[3].rotation);
            EnemyTacoBullet1 bulletScript3 = bulletInstance3.GetComponent<EnemyTacoBullet1>();
            if (bulletScript3 != null)
            {
                bulletScript3.SetDirection(bulletDirection3);
            }
            else
            {
                Debug.LogWarning("EnemyTacoBullet1 script not found!");
            }

            // 等待 3 秒後銷毀該子彈
            Destroy(bulletInstance3, 3f);

            // 等待一小段時間再發射下一顆子彈，避免過快
            yield return new WaitForSeconds(0.1f);  // 可以調整發射間隔
        }
        Anim.SetBool("IsAttack2", false);
     }
    IEnumerator Hurtshield()
    {
        Anim.SetBool("IsHurt", true);
        yield return new WaitForSeconds(1f);
        Anim.SetBool("IsHurt", false);
    }
        private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.CompareTag("Ground2"))
        //{
        //    StartCoroutine(Attack1());
        //}

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 减少玩家生命值
            //PlayerController.HeartNum -= 2;

            // 获取玩家的 Rigidbody
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();

            //if (playerRb != null)
            //{
            //    // 计算攻击方向
            //    Vector3 knockbackDirection = collision.transform.position - transform.position;

            //    // 对玩家施加反向的力，后退
            //    playerRb.AddForce(knockbackDirection.normalized * 20f, ForceMode.Impulse);
            //}
        }
    }
}
