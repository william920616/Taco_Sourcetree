using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldbreal : MonoBehaviour
{
    public AudioSource shieldbreakAudio;
    public  GameObject shield;
    public static bool isbreak ;

    // Start 方法在第一幀更新前調用
    void Start()
    {
        isbreak = false;
    }

    // Update 方法在每一幀都會被調用
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Taco")
        {
            Destroy(other.gameObject); // 正確銷毀遊戲物件
            Man.shieldbreaknumber += 1;
            Destroy(this.gameObject);
            //StartCoroutine(Hurt());
        }
    }

    IEnumerator Hurt()
    {
        isbreak = false;
        yield return new WaitForSeconds(0.1f);
        isbreak = true;
        yield return new WaitForSeconds(2f);
        isbreak = false;
       
    }
}
