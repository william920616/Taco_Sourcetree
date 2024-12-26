using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockerController : MonoBehaviour

{
    public GameObject blocker;
    public AudioSource breakAudio;
    public string bulletTag = "spicy";
    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.tag == bulletTag)
        {
            
            Destroy(gameObject);
        }

       
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Tacospicy")
        {
            breakAudio.Play();
            Destroy(gameObject);
        }
    }
}
