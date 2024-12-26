using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class ScreenLoarder : MonoBehaviour
{
    public GameObject eventObj;
    public Button newlevel;
    public Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject.DontDestroyOnLoad(this.gameObject);
        GameObject.DontDestroyOnLoad(this.eventObj);

        newlevel.onClick.AddListener(LoadSceneA);
    }

    private void LoadSceneA()
    {
        StartCoroutine(LoadScene(4));
    }
   

    IEnumerator LoadScene(int index)
    {
        animator.SetBool("Fadein", true);
        animator.SetBool("Fadeout", false);

        yield return new WaitForSeconds(1);

        AsyncOperation async = SceneManager.LoadSceneAsync(index);
        async.completed += OnLoadedScenes;
    }

    private void OnLoadedScenes(AsyncOperation obj)
    {
        animator.SetBool("Fadein", false);
        animator.SetBool("Fadeout", true);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "Player") 
    //    {
    //        StartCoroutine(LoadScene(2));
    //    }
    //}


        // Update is called once per frame
    void Update()
    {
        
    }
}
