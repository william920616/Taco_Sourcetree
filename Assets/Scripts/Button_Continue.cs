using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Continue : MonoBehaviour
{
    public GameObject menu;
    // Start is called before the first frame update
    public void Click()
    {
        Time.timeScale = 1;
        menu.SetActive(false);
    }
}
