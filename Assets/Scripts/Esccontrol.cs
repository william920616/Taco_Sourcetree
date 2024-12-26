using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Esccontrol : MonoBehaviour
{
    public GameObject setting;

    // Start is called before the first frame update
    void Start()
    {
        setting.SetActive(false); // 初始时隐藏设置界面
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0; // 停止游戏时间
            setting.SetActive(true); // 显示设置界面

            // 显示鼠标光标并解除锁定
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // 如果设置界面关闭，可以恢复鼠标的默认状态（锁定和隐藏）
        if (setting.activeSelf && Input.GetKeyDown(KeyCode.Return)) // 比如按回车退出设置
        {
            ResumeGame();
        }
    }

    // 恢复游戏的方法
    public void ResumeGame()
    {
        Time.timeScale = 1; // 恢复游戏时间
        setting.SetActive(false); // 隐藏设置界面

        // 恢复鼠标的默认状态
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
