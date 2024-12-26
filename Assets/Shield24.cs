using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield24 : MonoBehaviour
{
    public GameObject Shield;
    public static bool isbroken;
    // Start is called before the first frame update
    void Start()
    {
        isbroken = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Tacospicy")
        {
            Shield.SetActive(false);
            isbroken = true;
        }
    }
}
