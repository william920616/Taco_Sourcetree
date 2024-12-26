using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forkcontrol : MonoBehaviour
{
    public GameObject fork;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // 初始時使物體不受物理影響
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 檢查觸發者是否是玩家
        {
            rb.isKinematic = false; // 允許物體受物理影響
            rb.useGravity = true; // 開啟重力，讓物體掉落
            Destroy(gameObject, 2f);
        }
    }
}
