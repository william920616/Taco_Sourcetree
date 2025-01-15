using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Arena : MonoBehaviour
{
    public GameObject door; // 控制門的物件
    public GameObject[] icicle; // 冰柱的物件
    public GameObject[] monster; // 怪物的物件
    public AudioSource growAudio;
    public float countdownTime = 45f; // 倒計時時間
    public Text countdownText; // 倒計時 UI Text 文字
    public static bool istrigger;

    private bool isCountdownStarted = false; // 標記是否開始倒計時

    void Start()
    {
        istrigger = false;
        door.SetActive(false); // 隱藏門
        countdownText.gameObject.SetActive(false); // 隱藏倒計時文字
        UpdateCountdownUI(); // 更新倒計時 UI 文字
    }

    void Update()
    {
        // 如果開始倒計時，則倒計時時間減少，並更新倒計時文字
        if (isCountdownStarted)
        {
            countdownTime -= Time.deltaTime;
            UpdateCountdownUI();
            // 如果倒計時時間小於等於0，則隱藏門和倒計時文字，並停止倒計時
            if (countdownTime <= 0f)
            {
                door.SetActive(false);
                countdownText.gameObject.SetActive(false);
                isCountdownStarted = false;
            }
        }
    }

    // 當觸發器被觸發時
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") )
        {
            // 顯示門和倒計時文字，並開始倒計時
            door.SetActive(true);
            growAudio.Play();
            isCountdownStarted = true;
            countdownText.gameObject.SetActive(true);
            istrigger = true;

            // 開始生成冰柱和怪物
            StartCoroutine(SpawnIceProjectiles());
            StartCoroutine(SpawnMonsters());
        }
    }

    // 生成冰柱的協程
    IEnumerator SpawnIceProjectiles()
    {
        while (isCountdownStarted)
        {

            int random_icicle = Random.Range(0, icicle.Length);
            Instantiate(icicle[random_icicle], RandomPosition(), Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
            int random_icicle1 = Random.Range(0, icicle.Length);
            Instantiate(icicle[random_icicle1], RandomPosition(), Quaternion.identity);
            yield return new WaitForSeconds(0.3f);
            int random_icicle2 = Random.Range(0, icicle.Length);
            Instantiate(icicle[random_icicle2], RandomPosition(), Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
    }

    // 生成怪物的協程
    IEnumerator SpawnMonsters()
    {
        while (isCountdownStarted)
        {
           
            int random_monster = Random.Range(0, monster.Length);
            Instantiate(monster[random_monster], RandomPosition(), Quaternion.identity);
            yield return new WaitForSeconds(8f);
            int random_monster1 = Random.Range(0, monster.Length);
            Instantiate(monster[random_monster], RandomPosition(), Quaternion.identity);
            yield return new WaitForSeconds(5f); // 增加一個隨機等待時間
        }
    }

    // 隨機生成位置的方法
    Vector3 RandomPosition()
    {
        float x = Random.Range(-22f, 22f);
        float y = 5f; // 物件高度
        float z = Random.Range(-22f, 22f);
        return new Vector3(x, y, z);
    }

    // 更新倒計時 UI 文字的方法
    void UpdateCountdownUI()
    {
        countdownText.text = Mathf.CeilToInt(countdownTime).ToString(); // 更新倒計時文字為整數
    }
}
