using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 讓這個燈光物件在場景切換時不被銷毀
        DontDestroyOnLoad(gameObject);
    }
}
