using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
public class hitController : MonoBehaviour
{
    // 要生成的球物件和生成位置
    public GameObject ball;
    public GameObject point;
    float lifeTime = 0;

    private bool startDropping = false; // 用於標記是否開始掉落
    private bool coroutineStarted = false; // 用於標記是否已經啟動了協程
    private bool stopGenerating = false; // 標記是否要停止生成球的循環



    // Start 方法在第一幀更新前被呼叫
    void Start()
    {
        // 不再在這裡啟動協程
    }

     void Update()
    {
        if(Fallcontriller.isfalling == true)
        {
            stopGenerating = true;
        }
    }
    // 觸發器的觸發事件
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !coroutineStarted) // 檢查是否已經啟動了協程
        {
            startDropping = true; // 當玩家觸發觸發器時，開始掉落
            StartCoroutine(MyCoroutine()); // 開始執行生成和消失球的協程
            coroutineStarted = true; // 設置協程已經啟動
        }
    }

    // 定義生成和消失球的協程
    IEnumerator MyCoroutine()
    {
        while (!stopGenerating) // 只有在 stopGenerating 為 false 時才繼續執行循環
        {
            // 生成一個新的球物件在指定的位置
            GameObject newBall = Instantiate(ball, point.transform.position, Quaternion.identity);

            // 啟動一個協程來在一段時間後刪除球物件
            StartCoroutine(DestroyAfterSeconds(newBall, 3f));

            // 等待0.25秒後，再次生成球
            yield return new WaitForSeconds(0.25f);

        }
    }

    // 在需要停止協程的地方設置 stopGenerating 為 true
    void StopGeneratingBalls()
    {
        stopGenerating = true;
    }

    // 定義在一定時間後刪除物件的協程
    IEnumerator DestroyAfterSeconds(GameObject obj, float seconds)
    {
        // 等待一段時間
        yield return new WaitForSeconds(seconds);

        // 刪除指定的物件
        Destroy(obj);
        //stopGenerating = true;
    }
}