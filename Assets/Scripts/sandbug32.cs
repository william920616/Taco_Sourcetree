using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class sandbug32 : MonoBehaviour
{
    public static  bool istouchfloor; // 是否接触地面
    public NavMeshAgent navAgent; // 导航代理组件
    public Transform player; // 玩家位置

    // Start is called before the first frame update
    void Start()
    {
        // 初始化为 false，表示沙虫没有接触地面
        istouchfloor = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 触发碰撞时，判断是否接触玩家
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            istouchfloor = true; // 触地状态为 true
            Debug.Log("有");
        }
    }
}
