using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breadopen : MonoBehaviour
{
    public Animator Anim;
    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Bread32.isopen == true)
        {
            StartCoroutine(OpenDoor());
        }
    }
    IEnumerator OpenDoor()
    {
        yield return new WaitForSeconds(0.1f);
        Anim.SetBool("IsOpen", true);
    }
}
