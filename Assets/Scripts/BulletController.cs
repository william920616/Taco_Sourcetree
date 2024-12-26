using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//���q����
public class BulletController : MonoBehaviour
{
    Rigidbody rb;
    float lifeTime = 0; // �p�ɬ��
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * 100;
    }

    // Update is called once per frame
    void Update()
    {
        
        lifeTime += Time.deltaTime;
        if (lifeTime > 1.5)
        {
            Destroy(gameObject);
        }
    }
}
