using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class shield34 : MonoBehaviour
{
    public GameObject Shield;
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
        if (other.gameObject.tag == "Tacospicy")
        {
            Debug.Log("被打到");
            Shield.SetActive(false);
        }
    }
}
