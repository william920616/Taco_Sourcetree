using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cucumberMovement : MonoBehaviour
{
    public float startRollSpeed = 10f; // 設定水果的初始滾動速度
    public AudioSource hitAudio;

    void Update()
    {
        // 直接更新 transform.position 來沿 -Y 方向移動
        transform.position -= Vector3.forward * startRollSpeed * Time.deltaTime;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.gameObject.tag == "Player")
    //    {
    //        Destroy(gameObject);
    //    }
    //    if (other.gameObject.tag == "Taco")
    //    {
    //        hitAudio.Play();
    //        Destroy(this.gameObject);
    //        Destroy(other.gameObject);
    //    }
    //}
}
