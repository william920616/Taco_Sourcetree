using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceFallController : MonoBehaviour
{
    public float fallSpeed = 10f;
    public AudioSource icefallAudio;

    void Update()
    {
        if (IcejoController.iscome == true) 
        {
            transform.position += Vector3.down * fallSpeed * Time.deltaTime;
        }      
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Ground"|| other.gameObject.tag == "Player")
        {
            icefallAudio.Play();
            Destroy(gameObject,2f);
        }
    }
}

