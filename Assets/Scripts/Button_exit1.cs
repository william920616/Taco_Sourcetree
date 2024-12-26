using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_exit : MonoBehaviour
{
    public GameObject image;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Click()
    {
        image.SetActive(false);
    }
}
