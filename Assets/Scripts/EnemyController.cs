using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 5f; // 敌人移动速度
    public float radius;

    private Rigidbody2D rb; // 敌人的刚体组件
    private Transform playertransform;

    void Start()
    {
        playertransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>(); // 获取敌人的刚体组件
    }

    void Update()
    {
        if (playertransform != null)
        {
            float distance = Mathf.Abs(transform.position.x - playertransform.position.x);
            if (distance < radius)
            {
                transform.position = new Vector3(Mathf.MoveTowards(transform.position.x, playertransform.position.x, speed * Time.deltaTime), transform.position.y, transform.position.z);

                if (playertransform.position.x > 5)
                {
                    transform.rotation = Quaternion.Euler(0, 180f, 0);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }
        }
    }
}
    
