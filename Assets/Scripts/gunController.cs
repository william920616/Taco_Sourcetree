using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//���檺�P�w����
public class gunController : MonoBehaviour
{

    public GameObject fire;
    public GameObject bullet; // �l�u�ӷ� (�bUnity��������J)
    public GameObject Firepoint; // �o�g�Ѧ��I (�bUnity��������J)
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // �ƻs�l�u��o�g�Ѧ��I�W
            Instantiate(bullet, Firepoint.transform.position, transform.rotation);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            // �ƻs�l�u��o�g�Ѧ��I�W
            Instantiate(fire, Firepoint.transform.position, transform.rotation);

        }
    }
}
