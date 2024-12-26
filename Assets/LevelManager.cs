using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    private string currentSceneName;

    // 場景名稱對應下一關場景的映射
    private Dictionary<string, string> sceneTransitions;

    void Awake()
    {
        // 確保只有一個 LevelManager 實例存在
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 保持場景之間不被銷毀
        }
        else
        {
            Destroy(gameObject); // 如果已有實例存在，就摧毀當前的
        }
    }

    void Start()
    {
        // 初始化當前場景名稱
        currentSceneName = SceneManager.GetActiveScene().name;
        Debug.Log("當前場景名稱: " + currentSceneName); // 顯示當前場景名稱

        // 初始化場景對應的跳轉規則
        InitializeSceneTransitions();
    }

    // 初始化場景跳轉規則
    private void InitializeSceneTransitions()
    {
        sceneTransitions = new Dictionary<string, string>
        {
            { "LV1-1", "LV1-2" },
            { "LV1-2", "LV1-3" },
            { "LV1-3", "LV1-4" },
            { "LV1-4", "LV1-5" },
            { "LV1-5", "LV2-1-1" },
            { "LV2-1-1", "LV2-1-2" },
            { "LV2-1-2", "LV2-1-3" },
            { "LV2-1-3", "LV2-2" },
            { "LV2-2", "LV2-2-2" },
            { "LV2-2-2", "LV2-3" },
            { "LV2-3", "LV2-4" },
            { "LV2-4", "LV3-1" },
            { "LV3-1", "LV3-2" },
            { "LV3-2", "LV3-3" }
        };
    }

    // 加載下一關（異步加載場景）
    public void LoadNextLevel()
    {
        Debug.Log("LoadNextLevel() 方法被調用");
        Debug.Log("當前場景名稱: " + currentSceneName);

        // 檢查當前場景是否有對應的下一關
        if (sceneTransitions.ContainsKey(currentSceneName))
        {
            string nextSceneName = sceneTransitions[currentSceneName];
            Debug.Log("下一關場景: " + nextSceneName);

            if (Application.CanStreamedLevelBeLoaded(nextSceneName))
            {
                StartCoroutine(LoadSceneAsync(nextSceneName)); // 使用協程進行異步加載
            }
            else
            {
                Debug.LogError("無法加載場景: " + nextSceneName); // 報錯，告訴使用者場景加載失敗
            }
        }
        else
        {
            Debug.Log("這是最後一關，沒有更多關卡了！");
        }
    }

    // 異步加載場景協程
    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // 確保場景加載完成再更新 currentSceneName
        while (!asyncLoad.isDone)
        {
            // 可以加一個進度條顯示加載進度
            Debug.Log("場景加載進度: " + asyncLoad.progress);
            yield return null;
        }

        // 當場景加載完成後，更新當前場景名稱
        currentSceneName = sceneName;
        Debug.Log("已更新當前場景名稱為: " + currentSceneName);
    }

    // 測試方法：在 Update 中按鍵測試
    void Update()
    {
        // 當按下 G 鍵時，測試關卡跳轉
        if (Input.GetKeyDown(KeyCode.G)) // 按 G 鍵進行關卡跳轉測試
        {
            LoadNextLevel();
        }
    }
}
