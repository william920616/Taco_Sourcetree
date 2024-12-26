using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bossbullet : MonoBehaviour
{
    private float BulletSpeed = 10;
    private float Timer = 0;
    public GameObject Bullet;
    void Start()
    {
        Timer = 0;
        GameObject.Destroy(Bullet, 4.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * BulletSpeed * Time.deltaTime;
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        PlayerController.HP -= 3;
    //        Destroy(this.gameObject);
    //    }
    //}
}

