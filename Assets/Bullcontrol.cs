using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Bullcontrol : MonoBehaviour
{
    public Animator Anim;
    public Transform player; // 玩家位置
    public NavMeshAgent navAgent; // 导航代理组件
    public float pauseTime =5f; // 碰撞后暂停的时间（秒）
    
  

    private bool isPaused = false; // 是否暂停跟随

    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused) // 如果没有暂停跟随
        {
            navAgent.SetDestination(player.position); // 设置目标为玩家位置
        }
    }

    // 触发器进入时暂停跟随
    void OnTriggerEnter(Collider other)
    {
        // 如果触发器碰到特定的物体（比如 "PauseObject" 标签的物体）
        if (other.CompareTag("stoptaco"))
        {
            StartCoroutine(PauseFollow()); // 启动暂停协程
        }
    }

    // 暂停跟随的协程
    IEnumerator PauseFollow()
    {
        navAgent.isStopped = true; // 停止导航代理
        Anim.SetBool("Ishitting", true);
        isPaused = true; // 设置为暂停状态
        yield return new WaitForSeconds(pauseTime); // 等待指定时间
        Anim.SetBool("Ishitting", false);
        yield return new WaitForSeconds(1f); // 等待指定时间
        navAgent.isStopped = false; // 恢复导航代理
        isPaused = false; // 恢复跟随
    }

    IEnumerator Death()
    {
        Anim.SetBool("IsDead", true);  // 播放死亡动画
        yield return new WaitForSeconds(2f);  // 死亡动画播放完成
        Destroy(gameObject);  // 删除角色对象，表示死亡
    }
}
