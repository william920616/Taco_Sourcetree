using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerfloor34 : MonoBehaviour
{
    // 目標標籤
    public string newTag = "NewTag";

    // 當有物體進入觸發區域時
    private void OnTriggerEnter(Collider other)
    {
        // 檢查進入觸發區域的物體是否擁有"Player"標籤
        if (other.gameObject.CompareTag("Player"))
        {
            // 更改物體的標籤
            gameObject.tag = newTag;
            // 輸出一條日誌，顯示標籤已更改
            Debug.Log(gameObject.name + "的標籤已更改為：" + newTag);
        }
    }
}
