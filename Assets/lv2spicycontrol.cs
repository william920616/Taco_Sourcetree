using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lv2spicycontrol : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     void OnTriggerEnter(Collider other)
    {
        // 檢查碰撞物體是否擁有 "Ground" 標籤
        if (other.gameObject.CompareTag("Ground"))
        {
            if (goatcontrol.isdropten == true)
            {
                DontDestroyOnLoad(gameObject);
            }
            // 如果碰撞物體是 "Ground"，銷毀物品
            Destroy(gameObject);
        }
    }
}
