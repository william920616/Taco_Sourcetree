using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floortrigger2 : MonoBehaviour
{
    public GameObject floorObject;  // 需要顯示或隱藏的物件
    public float collisionDistance = 10f;  // 玩家與地板之間的碰撞觸發距離
    private Renderer floorRenderer;  // 物件的 Renderer
    private GameObject player;  // 玩家物件

    public float transparentAlpha = 0f;  // 設置透明度，0 為完全透明
    public float visibleAlpha = 1f;  // 完全不透明

    void Start()
    {
        player = GameObject.FindWithTag("Player");  // 查找玩家物件
        if (player == null)
        {
            Debug.LogError("沒有找到標籤為 'Player' 的物件！");
            return;
        }

        floorRenderer = floorObject.GetComponent<Renderer>();  // 獲取物件的 Renderer
        if (floorRenderer == null)
        {
            Debug.LogError("未能找到物件的 Renderer！請確保物件上有 Renderer 組件");
        }

        // 初始設置物體為透明
        SetObjectTransparency(transparentAlpha);
        SetRenderQueue();  // 設置材質的渲染隊列
    }

    void Update()
    {
        if (player != null)
        {
            // 計算玩家與地板物件的距離
            float distance = Vector3.Distance(player.transform.position, transform.position);

            // 如果玩家距離小於設定的距離，顯示物件
            if (distance < collisionDistance)
            {
                if (floorRenderer.material.color.a != visibleAlpha)  // 只有當物體完全透明時才調整
                {
                    SetObjectTransparency(visibleAlpha);
                }
            }
            else
            {
                if (floorRenderer.material.color.a != transparentAlpha)  // 只有當物體完全可見時才調整
                {
                    SetObjectTransparency(transparentAlpha);
                }
            }
        }
    }

    // 設置物體的透明度
    private void SetObjectTransparency(float alpha)
    {
        // 處理每個材質的透明度
        foreach (var material in floorRenderer.materials)
        {
            // 確保材質使用標準 Shader
            if (material.shader.name != "Standard")
            {
                material.shader = Shader.Find("Standard");
            }

            // 只有當材質的顏色有 alpha 時才進行修改
            if (material.HasProperty("_Color"))
            {
                Color color = material.color;
                color.a = alpha;  // 改變透明度
                material.color = color;

                // 確保材質處於 Cutout 模式
                if (alpha < 1f)
                {
                    material.SetFloat("_Mode", 1);  // 設置為 Cutout 模式
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    material.SetInt("_ZWrite", 1);  // 開啟深度寫入
                    material.EnableKeyword("_ALPHATEST_ON");  // 啟用 Alpha 測試
                    material.DisableKeyword("_ALPHABLEND_ON");  // 禁用 Alpha 混合
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");  // 禁用 Alpha 預乘
                    material.SetFloat("_Cutoff", 0.5f);  // 設置 Cutoff 值，根據透明度進行裁剪（通常設定為 0.5）
                }
                else
                {
                    material.SetFloat("_Mode", 0);  // 不透明模式
                    material.SetInt("_ZWrite", 1);  // 恢復深度寫入
                    material.renderQueue = 2000;  // 排到普通渲染隊列
                }
            }
        }
    }

    // 設置材質的渲染隊列，確保透明物體在不透明物體之後渲染
    private void SetRenderQueue()
    {
        foreach (var material in floorRenderer.materials)
        {
            if (material.HasProperty("_Color") && material.color.a < 1f) // 透明材質
            {
                material.renderQueue = 3000;  // 確保透明材質的渲染順序在不透明材質之後
            }
        }
    }

    // 當玩家與地板發生碰撞時觸發
    void OnCollisionEnter(Collision collision)
    {
        // 檢查碰撞物體是否是玩家
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("玩家與地板發生碰撞");

            // 在此處處理玩家與地板碰撞時的邏輯
            // 例如：當玩家碰撞時，顯示某些東西或觸發事件
        }
    }

    // 當玩家離開地板的碰撞區域時觸發
    void OnCollisionExit(Collision collision)
    {
        // 檢查碰撞物體是否是玩家
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("玩家離開地板的碰撞區域");

            // 在此處處理玩家與地板碰撞結束時的邏輯
            // 例如：當玩家離開碰撞區域時，隱藏或做其他操作
        }
    }
}
