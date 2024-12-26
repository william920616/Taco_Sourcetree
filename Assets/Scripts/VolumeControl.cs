using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Slider slider;      // 音量控制的滑块
    public Toggle toggle;      // 背景音乐的开关
    public AudioSource BGsound; // 背景音频源

    void Start()
    {
        // 设置初始值
        slider.value = GameVolControl.Vol;
        BGsound.volume = slider.value;

        // 初始化时设置 toggle 状态
        if (GameVolControl.BackVol == false)
        {
            BGsound.gameObject.SetActive(false); // 初始时如果音量被关闭，禁用背景音乐
            toggle.isOn = false;  // 如果音量关闭，确保 toggle 也是关闭状态
        }
        else
        {
            BGsound.gameObject.SetActive(true);  // 启动背景音乐
            toggle.isOn = true;   // 如果音量开启，确保 toggle 被勾选
        }
    }

    void Update()
    {
        // 每帧根据 Toggle 状态来控制音频播放
        ControlAudio();

        // 确保音量更新
        BGsound.volume = slider.value;
    }

    // 控制音频的开启与关闭
    public void ControlAudio()
    {
        if (toggle.isOn)
        {
            // 打勾时播放音频并设置音量
            BGsound.gameObject.SetActive(true);
            BGsound.volume = slider.value; // 确保音量与滑块同步
        }
        else
        {
            // 取消打勾时停止音频
            BGsound.gameObject.SetActive(false);
        }
    }

    // 音量调整方法
    public void Volume()
    {
        BGsound.volume = slider.value; // 音量随滑块变化
    }
}
