using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//绘﹚代刚
public class gunController : MonoBehaviour
{

    public GameObject fire;
    public GameObject bullet; // 紆ㄓ方 (Unityざい╈)
    public GameObject Firepoint; // 祇甮把σ翴 (Unityざい╈)
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // 狡籹紆祇甮把σ翴
            Instantiate(bullet, Firepoint.transform.position, transform.rotation);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            // 狡籹紆祇甮把σ翴
            Instantiate(fire, Firepoint.transform.position, transform.rotation);

        }
    }
}
