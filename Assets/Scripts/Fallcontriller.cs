using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fallcontriller : MonoBehaviour
{
    public static bool isfalling = false;
    // Start is called before the first frame update
    void Start()
    {
        isfalling = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "closefall")
        {
            isfalling = false;
        }
    }
}
