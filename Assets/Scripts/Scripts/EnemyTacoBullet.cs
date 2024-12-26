using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTacoBullet : MonoBehaviour
{
    public float speed = 25f;  // 设置速度为公开变量，方便调整

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }
}
