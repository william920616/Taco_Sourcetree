using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeColorOnTrigger : MonoBehaviour
{
    // 預設目標顏色為紅色
    public Color targetColor = Color.red;

    // 設定一個 tag 來篩選哪些物體能觸發顏色變更
    public string triggerTag = "Player";

    private Renderer objectRenderer;

    private void Start()
    {
        // 確保物體上有 Renderer 組件，方便更改材質顏色
        objectRenderer = GetComponent<Renderer>();
    }

    // 當有物體進入觸發區域時觸發
    private void OnTriggerEnter(Collider other)
    {
        // 只在觸發的物體有指定 tag 時，改變顏色
        if (other.CompareTag(triggerTag))
        {
            if (objectRenderer != null)
            {
                StartCoroutine(change());
            }
        }
    }

    IEnumerator change()
    {
        // 改變材質顏色
        objectRenderer.material.color = targetColor;
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(9);

    }
}
