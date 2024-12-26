using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitMovement : MonoBehaviour
{
    public float bulletSpeed = 10f;

    void Update()
    {
        // 使物體沿自身的up方向移動
        transform.position -= transform.right * bulletSpeed * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Taco")
        {
            Destroy(gameObject);
        }
    }
}