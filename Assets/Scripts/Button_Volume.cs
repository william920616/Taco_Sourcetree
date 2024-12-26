using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonImageToggle : MonoBehaviour
{
    public Sprite image1; // 圖片1
    public Sprite image2; // 圖片2

    private Image buttonImage;
    private bool isImage1 = true; // 初始圖片是1

    void Start()
    {
        buttonImage = GetComponent<Image>();
        if (buttonImage == null)
        {
            Debug.LogError("未找到按鈕圖片組件！");
        }
    }

    public void OnButtonClick()
    {
        if (isImage1)
        {
            buttonImage.sprite = image2; // 切換到圖片2
        }
        else
        {
            buttonImage.sprite = image1; // 切換到圖片1
        }
        isImage1 = !isImage1; // 切換標誌
    }
}
