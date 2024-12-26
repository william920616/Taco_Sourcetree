using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Man : MonoBehaviour
{
    public float followDistance = 5f;
    public float attackDistance = 4f;
    public Animator Anim;
    public GameObject shield;
    public GameObject bullet;
    
    public static int shieldbreaknumber;
    public static int HeartNum = 3;
    public Transform[] firepoint;

    private NavMeshAgent agent;
    private Transform player;
    private GameObject Shield;
    private bool CanMove = true;    

    void Start()
    {
        Anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        shieldbreaknumber = 0;
        StartCoroutine(shooting());
    }

    void Update()
    {
        if (HeartNum == 0)
            StartCoroutine(Death());
        
        if (player != null)
        {
            //檢測與玩家的距離
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            //Debug.Log("Distance to player: " + distanceToPlayer); // 添加這行用於除錯

            //如果盾碎兩個
            if (shieldbreaknumber == 2)
            {
                Shield = GameObject.FindGameObjectWithTag("Shield");
                Destroy(Shield);
                //原地發呆
                agent.SetDestination(transform.position);
                //啟動受傷動畫協程
                StartCoroutine(Hurt());
            }
            //追蹤玩家
            else if (distanceToPlayer > followDistance && CanMove == true)
            {
                //設定被追蹤玩家的位置
                agent.SetDestination(player.position);
            }
            else if (distanceToPlayer >= attackDistance && CanMove == true)
            {
                Attack();
                agent.SetDestination(transform.position);
            }
        }
    }
   
    void Attack()
    {
        //啟動協程跳躍攻擊
        StartCoroutine(JumpAttack());
    }

    //跳躍攻擊協程
    IEnumerator JumpAttack()
    {
        CanMove = false;
        Anim.SetBool("Attack", true);
        yield return new WaitForSeconds(2.5f);

        // 生成水果
        Instantiate(bullet, firepoint[0].position, firepoint[0].rotation);
        Instantiate(bullet, firepoint[1].position, firepoint[1].rotation);
        Instantiate(bullet, firepoint[2].position, firepoint[2].rotation);
        Instantiate(bullet, firepoint[3].position, firepoint[3].rotation);
        Instantiate(bullet, firepoint[4].position, firepoint[4].rotation);
        Instantiate(bullet, firepoint[5].position, firepoint[5].rotation);
        Instantiate(bullet, firepoint[6].position, firepoint[6].rotation);
        Instantiate(bullet, firepoint[7].position, firepoint[7].rotation);
        Instantiate(bullet, firepoint[8].position, firepoint[8].rotation);
        Instantiate(bullet, firepoint[9].position, firepoint[9].rotation);
        yield return new WaitForSeconds(0.5f);
        Instantiate(bullet, firepoint[10].position, firepoint[10].rotation);
        Instantiate(bullet, firepoint[11].position, firepoint[11].rotation);
        Instantiate(bullet, firepoint[12].position, firepoint[12].rotation);
        Instantiate(bullet, firepoint[13].position, firepoint[13].rotation);
        Instantiate(bullet, firepoint[14].position, firepoint[14].rotation);
        Instantiate(bullet, firepoint[15].position, firepoint[15].rotation);
        Instantiate(bullet, firepoint[16].position, firepoint[16].rotation);
        Instantiate(bullet, firepoint[17].position, firepoint[17].rotation);
        Instantiate(bullet, firepoint[18].position, firepoint[18].rotation);
        Anim.SetBool("Attack", false);
        CanMove = true;
        yield return new WaitForSeconds(1f);
        Instantiate(bullet, firepoint[0].position, firepoint[0].rotation);
        Instantiate(bullet, firepoint[1].position, firepoint[1].rotation);
        Instantiate(bullet, firepoint[2].position, firepoint[2].rotation);
        Instantiate(bullet, firepoint[3].position, firepoint[3].rotation);
        Instantiate(bullet, firepoint[4].position, firepoint[4].rotation);
        Instantiate(bullet, firepoint[5].position, firepoint[5].rotation);
        Instantiate(bullet, firepoint[6].position, firepoint[6].rotation);
        Instantiate(bullet, firepoint[7].position, firepoint[7].rotation);
        Instantiate(bullet, firepoint[8].position, firepoint[8].rotation);
        Instantiate(bullet, firepoint[9].position, firepoint[9].rotation);
        Instantiate(bullet, firepoint[10].position, firepoint[10].rotation);
        Instantiate(bullet, firepoint[11].position, firepoint[11].rotation);
        Instantiate(bullet, firepoint[12].position, firepoint[12].rotation);
        Instantiate(bullet, firepoint[13].position, firepoint[13].rotation);
        Instantiate(bullet, firepoint[14].position, firepoint[14].rotation);
        Instantiate(bullet, firepoint[15].position, firepoint[15].rotation);
        Instantiate(bullet, firepoint[16].position, firepoint[16].rotation);
        Instantiate(bullet, firepoint[17].position, firepoint[17].rotation);
        Instantiate(bullet, firepoint[18].position, firepoint[18].rotation);
    }
    IEnumerator shooting()
    {
        Instantiate(bullet, firepoint[0].position, firepoint[0].rotation);
        Instantiate(bullet, firepoint[1].position, firepoint[1].rotation);
        Instantiate(bullet, firepoint[2].position, firepoint[2].rotation);
        Instantiate(bullet, firepoint[3].position, firepoint[3].rotation);
        Instantiate(bullet, firepoint[4].position, firepoint[4].rotation);
        Instantiate(bullet, firepoint[5].position, firepoint[5].rotation);
        Instantiate(bullet, firepoint[6].position, firepoint[6].rotation);
        Instantiate(bullet, firepoint[7].position, firepoint[7].rotation);
        Instantiate(bullet, firepoint[8].position, firepoint[8].rotation);
        Instantiate(bullet, firepoint[9].position, firepoint[9].rotation);
        yield return new WaitForSeconds(0.5f);
        Instantiate(bullet, firepoint[10].position, firepoint[10].rotation);
        Instantiate(bullet, firepoint[11].position, firepoint[11].rotation);
        Instantiate(bullet, firepoint[12].position, firepoint[12].rotation);
        Instantiate(bullet, firepoint[13].position, firepoint[13].rotation);
        Instantiate(bullet, firepoint[14].position, firepoint[14].rotation);
        Instantiate(bullet, firepoint[15].position, firepoint[15].rotation);
        Instantiate(bullet, firepoint[16].position, firepoint[16].rotation);
        Instantiate(bullet, firepoint[17].position, firepoint[17].rotation);
        Instantiate(bullet, firepoint[18].position, firepoint[18].rotation);
        yield return new WaitForSeconds(1f);
        Instantiate(bullet, firepoint[0].position, firepoint[0].rotation);
        Instantiate(bullet, firepoint[1].position, firepoint[1].rotation);
        Instantiate(bullet, firepoint[2].position, firepoint[2].rotation);
        Instantiate(bullet, firepoint[3].position, firepoint[3].rotation);
        Instantiate(bullet, firepoint[4].position, firepoint[4].rotation);
        Instantiate(bullet, firepoint[5].position, firepoint[5].rotation);
        Instantiate(bullet, firepoint[6].position, firepoint[6].rotation);
        Instantiate(bullet, firepoint[7].position, firepoint[7].rotation);
        Instantiate(bullet, firepoint[8].position, firepoint[8].rotation);
        Instantiate(bullet, firepoint[9].position, firepoint[9].rotation);
        Instantiate(bullet, firepoint[10].position, firepoint[10].rotation);
        Instantiate(bullet, firepoint[11].position, firepoint[11].rotation);
        Instantiate(bullet, firepoint[12].position, firepoint[12].rotation);
        Instantiate(bullet, firepoint[13].position, firepoint[13].rotation);
        Instantiate(bullet, firepoint[14].position, firepoint[14].rotation);
        Instantiate(bullet, firepoint[15].position, firepoint[15].rotation);
        Instantiate(bullet, firepoint[16].position, firepoint[16].rotation);
        Instantiate(bullet, firepoint[17].position, firepoint[17].rotation);
        Instantiate(bullet, firepoint[18].position, firepoint[18].rotation);
        yield return new WaitForSeconds(8f);
        StartCoroutine(shooting());
    }
    //受傷動畫
    IEnumerator Hurt()
    {
        CanMove = false;
        shieldbreaknumber = 0;
        Anim.SetBool("Hurt", true);
        yield return new WaitForSeconds(2f);
        Anim.SetBool("Hurt", false);
        shieldbreaknumber = 0;
        yield return new WaitForSeconds(0.1f);
        Instantiate(shield, transform.position, transform.rotation);
        CanMove = true;
        //ishurt = false; // Reset ishurt flag after the hurt animation is finished.
    }

   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Taco"&&CanMove == false)
        {
            Destroy(other.gameObject);
            HeartNum--;
        }
        if (other.gameObject.tag == "stoptaco")
        {
            StartCoroutine(Stop());
        }
    }
    IEnumerator Death()
    {
        Anim.SetBool("Death", true);
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
        SceneManager.LoadScene(7);
    }

    IEnumerator Stop()
    {
         agent.isStopped = true; // 停止 NavMeshAgent 的運動
         followDistance = 0;
         attackDistance = 0;
         yield return new WaitForSeconds(4f); // 停留兩秒
         agent.isStopped = false; // 恢復 NavMeshAgent 的運動
         followDistance = 5;
         attackDistance = 4;
    }
}