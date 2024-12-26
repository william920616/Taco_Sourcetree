using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithCollider : MonoBehaviour
{
    private Animator animator;
    private BoxCollider boxCollider;

    // 預設要跟隨的目標動畫
    private string targetAnimationName = "yakiniku02";

    private void Start()
    {
        // 確保有 Animator 和 BoxCollider 組件
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        // 如果動畫正在播放並且它影響物體的位置或大小
        if (animator != null && animator.GetCurrentAnimatorStateInfo(0).IsName(targetAnimationName))
        {
            // 同步 Collider 的位置
            if (boxCollider != null)
            {
                boxCollider.center = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
                // 如果需要，你還可以根據動畫的變換動態調整 BoxCollider 的大小
                // boxCollider.size = new Vector3(...); // 可以根據需要調整大小
            }
        }
    }
}
