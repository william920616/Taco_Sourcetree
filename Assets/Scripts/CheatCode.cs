using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatCode : MonoBehaviour
{
    private void Update()
    {
         // 按下 "N" 鍵時，跳過當前關卡
        if (Input.GetKeyDown(KeyCode.G))
        {
            SkipLevel();
        }
    }
    void SkipLevel()
    {
        // 假設你有一個 Level 系統
        LevelManager.Instance.LoadNextLevel(); // 跳過到下一關
    }
}
