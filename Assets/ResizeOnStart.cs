using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeOnStart : MonoBehaviour
{
    // 動畫結束後設置的縮放
    public Vector3 desiredScale = new Vector3(346, 346, 346);

    void Start()
    {
        // 如果動畫在開始時就需要重新設置縮放，使用這個
        transform.localScale = desiredScale;

        // 可選：等待動畫播放結束後再設定縮放
        StartCoroutine(ResetScaleAfterAnimation());
    }

    // 使用協程延遲
    private IEnumerator ResetScaleAfterAnimation()
    {
        // 假設動畫播放需要 1 秒鐘
        yield return new WaitForSeconds(1f);

        // 設定縮放
        transform.localScale = desiredScale;
    }
}
