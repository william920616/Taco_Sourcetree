using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentControl : MonoBehaviour
{
    public int Heart;
    public Transform player; // 玩家位置
    public  NavMeshAgent navAgent; // 导航代理组件
    void Start()
    {
        Heart = 5;
    }

    void Update()
    {
        if (Heart <=  0)
        {
            Destroy(gameObject);
        }
        if(sandbug32.istouchfloor == true)
        {
            this.navAgent.SetDestination(this.player.position);
        } 
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Taco")
        {
            Heart -= 1;
            
        }
    }
}
