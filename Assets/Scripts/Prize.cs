using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Prize : MonoBehaviour
{
    public float fallSpeed = 10;
    // Start is called before the first frame update
    void Start()
    {
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(0);
        }
    }
}
