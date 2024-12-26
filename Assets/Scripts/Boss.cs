using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Transform firePoint;
    public Transform firePoint2;
    public Transform firePoint3;
    public Transform firePoint4;
    public Transform firePoint5;
    public Transform firePoint6;
    public Transform firePoint7;
    public Transform firePoint8;
    public Transform firePoint9;
    public GameObject bullet;
    public Transform player; // 玩家位置
    public float speed = 5.0f; // 移動速度
    public float stoppingDistance = 2.0f; // 停止距離
    public float rotationSpeed = 5.0f; // 旋轉速度

    //public Animator Anim;

    private float HP = 50;

    void Start()
    {
        // 找到玩家
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(KeepShooting());
        HP = 50;
    }
    void Update()
    {
        if (player == null)
        {
            Debug.LogWarning("玩家目標尚未設置");
            return;
        }

        float distance = Vector3.Distance(transform.position, player.position);

        // 當敵人與玩家距離大於停止距離時，敵人向玩家移動
        if (distance > stoppingDistance)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            // 讓敵人轉向玩家
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            // 讓敵人轉向玩家，即使距離小於停止距離
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        }
    }
    IEnumerator KeepShooting()
    {
        while (true)
        {
            yield return new WaitForSeconds(4f);
            Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
            Instantiate(bullet, firePoint2.transform.position, firePoint2.transform.rotation);
            Instantiate(bullet, firePoint3.transform.position, firePoint3.transform.rotation);
            Instantiate(bullet, firePoint4.transform.position, firePoint4.transform.rotation);
            Instantiate(bullet, firePoint5.transform.position, firePoint5.transform.rotation);
            Instantiate(bullet, firePoint6.transform.position, firePoint6.transform.rotation);
            Instantiate(bullet, firePoint7.transform.position, firePoint7.transform.rotation);
            Instantiate(bullet, firePoint8.transform.position, firePoint8.transform.rotation);
            Instantiate(bullet, firePoint9.transform.position, firePoint9.transform.rotation);
        }
    }
    //IEnumerator Death()
    //{
    //    Anim.SetBool("Death", true);
    //    yield return new WaitForSeconds(0.6f);
    //    Destroy(this.gameObject);
    //}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Sword")
        {
            HP -= 25;
        }
        if (other.gameObject.tag == "Bullet")
        {
            HP -= 10;
        }
    }
} 

