using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaColorChange : MonoBehaviour
{
    public Color startColor = Color.red;   // 初始顏色：紅色
    public Color endColor = new Color(1f, 0.5f, 0f);  // 結束顏色：橙色（橘色）
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        // 設置發光顏色
        rend.material.SetColor("_EmissionColor", startColor);
    }

    void Update()
    {
        // 漸變顏色效果，時間基於 _Time 來變化
        float lerpTime = Mathf.PingPong(Time.time, 1);  // 來回變化
        rend.material.color = Color.Lerp(startColor, endColor, lerpTime);
        rend.material.SetColor("_EmissionColor", Color.Lerp(startColor, endColor, lerpTime));
    }
}
