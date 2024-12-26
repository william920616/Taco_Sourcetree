using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerfork35 : MonoBehaviour
{
    public static bool isstopknife = false;
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
        if (other.gameObject.CompareTag("Player"))
        {
            isstopknife = true;
        }
    }
}
