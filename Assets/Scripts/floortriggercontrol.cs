using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floortriggercontrol : MonoBehaviour
{
    public static bool istouch;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag =="Player")
        {
            istouch = true;
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.tag =="Player")
    //    {
    //        floor.SetActive(false);
    //    }
    //}
}
