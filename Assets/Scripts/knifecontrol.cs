using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeControl : MonoBehaviour
{
    public float swingSpeed = 2f; // 擺動速度
    public float swingAngleMin = -45f; // 左邊的最小角度
    public float swingAngleMax = -135f; // 右邊的最大角度

    void Update()
    {
        // 使用 Mathf.PingPong 來在 swingAngleMin 和 swingAngleMax 之間來回擺動
        float angle = Mathf.PingPong(Time.time * swingSpeed, swingAngleMax - swingAngleMin) + swingAngleMin;

        // 設定物體的旋轉，保持 Y 軸為 90，X 軸擺動，Z 軸為 0
        transform.localRotation = Quaternion.Euler(angle, 90, 0);
    }
}
