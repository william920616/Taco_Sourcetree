using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tacothrow : MonoBehaviour
{
    public float speed;
    public float rotateSpeed;
    public static bool isshoot;
    public GameObject taco;

    private Rigidbody rb;
    private Transform playerTransform;
    private Transform tacoTransform;
    private bool canMove = false; // 控制taco是否可以移动的变量

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero; // 将初始速度设置为零向量
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        tacoTransform = GetComponent<Transform>();
        isshoot = false;
    }

    void Update()
    {
        transform.Rotate(0, rotateSpeed, 0);

        // 如果按下C键并且taco尚未发射，则允许taco移动
        if (Input.GetKeyDown(KeyCode.C) && !isshoot)
        {
            canMove = true;
            rb.velocity = transform.right * speed; // 设置taco的初始速度
            isshoot = false;
        }

        // 只有当canMove为true时，taco才能移动
        if (canMove)
        {
            // 当taco的x位置与玩家的x位置相差小于1.5时，销毁taco
            if (Mathf.Abs(transform.position.x - playerTransform.position.x) > 10f)
            {
                isshoot = true;
                Destroy(gameObject);
            }
        }
    }
}





