using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerbullet : MonoBehaviour
{
    GameObject player;
    private float bulletSpeed = 25f;
    private bool returning = false;
    private Vector3 originalPosition;
    private Vector3 playerPosition;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        // 在 Start 函數中設置子彈的初始位置為子彈的當前位置
        originalPosition = transform.position;
        transform.rotation = Quaternion.LookRotation(player.transform.forward);
    }

    void Update()
    {
        // 獲取玩家物件
        player = GameObject.FindGameObjectWithTag("Player");
        playerPosition = player.transform.position;
        if (!returning)
        {
            //確保player不是空的
            if (player != null)
            {
                //將子彈的方向改為跟隨主角的朝向
              
                // 根據子彈的前方方向移動
                transform.position += transform.forward * bulletSpeed * Time.deltaTime;
            }

            // 如果子彈與原始位置之間的距離超過一定範圍，開始返回
            if (Vector3.Distance(transform.position, originalPosition) > 20f)
            {
                returning = true;
            }
        }
        else
        {
            // 子彈返回原始位置
            transform.position = Vector3.MoveTowards(transform.position, playerPosition, bulletSpeed * Time.deltaTime);

            // 如果子彈返回到原始位置，銷毀它
            if (Vector3.Distance(transform.position, playerPosition) < 0.01f)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Iceshield")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

}
