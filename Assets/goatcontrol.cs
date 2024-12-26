using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goatcontrol : MonoBehaviour
{
    public GameObject bullet;            // 子彈的預製件
    public GameObject itemPrefab;        // 掉落物品的預製件
    public GameObject anotherItemPrefab; // 物品替換的第二種物件
    public GameObject shield;
    public GameObject objectToDie;  // 另一個死亡物件
    public Transform[] firepoint;        // 子彈發射點
    public Animator Anim;                // 動畫控制器
    public int HP;  // 血量
    public static bool isdropten;
    public static bool goatisdead;

    public Transform[] pentagonPoints;   // 五角形的五個頂點
    public float attackInterval = 4f;    // 攻擊與掉落物品的時間間隔
    public float dropHeight = 10f;       // 掉落物品的高度
    public float dropInterval = 1f;      // 物品掉落的間隔時間
    public bool isbroken; // 護盾是否被擊破
    public bool ishurt;   // 是否受傷
    private int dropCount = 0;           // 記錄掉落物品的次數
    private int attackCount = 0;  // 攻擊計數器
    private bool isHurtShieldActive = false; // 用來控制協程是否正在執行
    private bool isAttacking = false;  // 用來防止重複攻擊
    private bool iscount = false;  // 用來防止重複計數

    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();
        StartCoroutine(RepeatedAttack());  // 開始第一次攻擊協程
        StartCoroutine(ContinuousDrop());  // 開始不間斷掉落協程
        isbroken = false;
        ishurt = false;
        goatisdead = false;
        HP = 3;  // 初始血量為 3
    }

    void Update()
    {
        if (!shield.activeSelf && isHurtShieldActive && !ishurt)  // 盾牌已破且未處於受傷狀態
        {
            StartCoroutine(Hurtshield());  // 開始受傷協程
        }

        // 如果攻擊次數大於 3 且血量為 0，則開始死亡流程
        if (attackCount >= 3 && HP <= 0)
        {
            StartCoroutine(Death());
        }
    }

    // 攻擊協程
    IEnumerator RepeatedAttack()
    {
        shield.SetActive(false);  // 關閉盾牌，表示破盾
        yield return new WaitForSeconds(2f);  // 等待 2 秒
        shield.SetActive(true);  // 重新啟用盾牌
        yield return new WaitForSeconds(1f);  // 等待 1 秒

        isHurtShieldActive = false;  // 設定協程為結束狀態
        ishurt = true;  // 設定受傷狀態

        StartCoroutine(attack());  // 啟動攻擊協程
    }

    // 攻擊協程
    IEnumerator attack()
    {
        if (isAttacking) yield break;  // 如果正在攻擊，則跳出

        isAttacking = true;  // 設定攻擊狀態為正在攻擊
        yield return new WaitForSeconds(1);  // 等待 1 秒

        Anim.SetBool("IsAttacking", true);  // 開始攻擊動畫
        yield return new WaitForSeconds(3f);  // 等待 3 秒
        // 生成子彈
        foreach (Transform fire in firepoint)
        {
            Instantiate(bullet, fire.position, fire.rotation);
        }

        yield return new WaitForSeconds(2f);  // 等待 2 秒
        Anim.SetBool("IsAttacking", false);  // 結束攻擊動畫
        yield return new WaitForSeconds(0.5f);

        isHurtShieldActive = true;  // 設定為可以觸發受傷
        ishurt = false;
        isAttacking = false;  // 攻擊結束，解除攻擊狀態
    }

    // 受傷協程
    IEnumerator Hurtshield()
    {
        Anim.SetBool("IsHurt", true);  // 播放受傷動畫
        yield return new WaitForSeconds(2f);  // 等待 2 秒
        Anim.SetBool("IsHurt", false);  // 結束受傷動畫
        yield return new WaitForSeconds(0.5f);  // 再等 1 秒
        // 等待盾牌完全啟用後開始下一輪攻擊
        yield return new WaitForSeconds(0.5f);
        if (!isAttacking)  // 如果當前沒有在攻擊中
        {
            shield.SetActive(true);
            if (isbroken && !iscount)
            {
                isbroken = false;
                attackCount += 1;  // 每次破盾後攻擊計數器+1
                HP -= 1;  // 減少血量
                Debug.Log("Attack Count: " + attackCount);  // 輸出當前攻擊次數
                Debug.Log(HP);
            }

            isbroken = false;  // 盾牌標記已破壞
            iscount = true;  // 標記計數器已增加
            yield return new WaitForSeconds(1f);  // 等待 1 秒

            isHurtShieldActive = false;  // 設定協程為結束狀態
            ishurt = true;  // 設定受傷狀態

            StartCoroutine(attack());  // 啟動攻擊協程
        }

        // 重置標誌，允許下次攻擊增加計數器
        isbroken = true;
        iscount = false;
    }
    
    // 死亡協程
    IEnumerator Death()
    {
        Anim.SetBool("IsDead", true);  // 播放死亡動畫
        yield return new WaitForSeconds(2f);  // 死亡動畫播放完成
        Destroy(gameObject);  // 刪除角色對象，表示死亡

        // 同時讓另一個物件也死亡
        if (objectToDie != null)
        {
            Destroy(objectToDie);  // 刪除另一個物件
        }
    }

    IEnumerator ContinuousDrop()
    {
        while (true) // 讓掉落持續執行
        {
            DropItem(); // 執行掉落物品的函數
            yield return new WaitForSeconds(dropInterval); // 每次掉落後等待一段時間
        }
    }

    // 隨機掉落物品的方法
    void DropItem()
    {
        // 隨機選擇五角形範圍內的掉落點
        Vector3 randomPosition = GetRandomPositionInPentagon();

        // 根據掉落次數判斷使用哪個物品
        GameObject droppedItem;

        // 如果達到10次，換掉物品並重置計數
        if (dropCount >= 5)
        {
            StartCoroutine(dropitem());
            // 替換物品
            droppedItem = Instantiate(anotherItemPrefab, new Vector3(randomPosition.x, dropHeight, randomPosition.z), Quaternion.identity);
            dropCount = 0; // 重置掉落次數
        }
        else
        {
            // 正常掉落物品
            droppedItem = Instantiate(itemPrefab, new Vector3(randomPosition.x, dropHeight, randomPosition.z), Quaternion.identity);
        }

        // 更新掉落計數
        dropCount++;
        

    }

    // 隨機選擇五角形內部的掉落位置
    Vector3 GetRandomPositionInPentagon()
    {
        // 隨機選擇五角形範圍內的點
        Vector3 randomPosition = Vector3.zero;

        // 隨機選擇三個重心坐標參數 alpha、beta 和 gamma
        float alpha = Random.Range(0f, 1f);
        float beta = Random.Range(0f, 1f);
        float gamma = 1 - alpha - beta;

        // 如果 alpha + beta > 1，則重新生成
        if (alpha + beta > 1)
        {
            alpha = 1 - alpha;
            beta = 1 - beta;
            gamma = 1 - alpha - beta;
        }

        // 使用五個頂點，將其分成三個三角形進行隨機生成
        Vector3 A = pentagonPoints[0].position;
        Vector3 B = pentagonPoints[1].position;
        Vector3 C = pentagonPoints[2].position;

        // 使用五角形的前三個點來生成隨機點
        randomPosition = A * alpha + B * beta + C * gamma;

        return randomPosition;
    }
    IEnumerator dropitem()
    {
        isdropten = true;
        yield return new WaitForSeconds(2f);
        if(dropCount == 0)
        {
            isdropten = false;
        }
    }
}