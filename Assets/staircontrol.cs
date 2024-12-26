using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class staircontrol : MonoBehaviour
{
    public GameObject stair;
    // Start is called before the first frame update
    void Start()
    {
        stair.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(stairshow());
        }
    }
    IEnumerator stairshow()
    {
        yield return new WaitForSeconds(5f);
        stair.SetActive(true);
    }
}
