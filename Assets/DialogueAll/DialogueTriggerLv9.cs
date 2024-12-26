using UnityEngine;
using UnityEngine.UI;  // 引入 UI 命名空間
using System.Collections;

public class DialogueTriggerLv9 : MonoBehaviour
{
    public Animator dialogueAnimator;  // 對話框的 Animator 組件
    public GameObject dialogueBox;  // 對話框 GameObject
    public Transform player;  // 玩家 Transform
    public float triggerDistance = 5f;  // 觸發對話的距離
    public Text dialogueText;  // 對話框內的 Text UI 元素
    public string[] dialogueLines;  // 對話文本數組

    private bool isDialogueVisible = false;  // 對話框是否可見
    private bool hasEntered = false;  // 是否已經觸發過進入動畫
    private int currentDialogueIndex = 0;  // 當前顯示的對話索引

    void Start()
    {
        // 一開始隱藏對話框
        dialogueBox.SetActive(false);
    }

    void Update()
    {
        // 計算玩家和 NPC 之間的距離
        float distance = Vector3.Distance(player.position, transform.position);

        // 如果玩家距離小於觸發距離且對話框目前不可見且沒有觸發過進入動畫，則顯示對話框
        if (distance <= triggerDistance && !isDialogueVisible && !hasEntered)
        {
            ShowDialogue();
        }
        // 如果玩家距離大於觸發距離且對話框目前可見，則隱藏對話框
        else if (distance > triggerDistance && isDialogueVisible)
        {
            HideDialogue();
        }

        // 如果對話框可見且檢測到左鍵點擊，顯示下一段對話
        if (isDialogueVisible && Input.GetMouseButtonDown(0))
        {
            ShowNextDialogue();
        }
    }

    // 顯示對話框並觸發進入動畫
    void ShowDialogue()
    {
        dialogueAnimator.SetTrigger("Enter");  // 觸發進入動畫
        dialogueBox.SetActive(true);  // 顯示對話框
        isDialogueVisible = true;  // 更新對話框狀態為可見
        hasEntered = true;  // 設置為已觸發進入動畫
        currentDialogueIndex = 0;  // 重置對話索引
        dialogueText.text = dialogueLines[currentDialogueIndex];  // 顯示第一段對話
    }

    // 顯示下一段對話
    void ShowNextDialogue()
    {
        currentDialogueIndex++;  // 增加對話索引
        if (currentDialogueIndex < dialogueLines.Length)
        {
            dialogueText.text = dialogueLines[currentDialogueIndex];  // 顯示下一段對話
        }
        else
        {
            // 如果沒有更多對話，則隱藏對話框
            HideDialogue();
        }
    }

    // 隱藏對話框並觸發退出動畫
    void HideDialogue()
    {
        dialogueAnimator.SetBool("Out", true);  // 觸發退出動畫
        isDialogueVisible = false;  // 更新對話框狀態為不可見
        StartCoroutine(WaitAndHide());  // 使用協程來延遲隱藏對話框，確保動畫播放完畢
    }

    // 協程，等待動畫播放完畢後隱藏對話框
    private IEnumerator WaitAndHide()
    {
        // 假設退出動畫持續時間為1.5秒
        yield return new WaitForSeconds(5f);
        dialogueBox.SetActive(false);  // 隱藏對話框
        hasEntered = false;  // 重置為未觸發進入動畫
    }
}
