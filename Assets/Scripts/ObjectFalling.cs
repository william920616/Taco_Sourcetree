using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFalling : MonoBehaviour
{
    public float fallSpeed = 5f; // 設定物體下降的速度

    void Update()
    {
        // 將物體向下移動
        transform.Translate(Vector3.back * fallSpeed * Time.deltaTime);
    }
}
