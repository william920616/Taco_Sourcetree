using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject[] monster; // 怪物的物件
    private int random_monster;
    private bool isCountdownStarted = false;
    private bool isMonsterSpawnStart = false;
    // Start is called before the first frame update
    void Start()
    {
        isCountdownStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        int random_monster = Random.Range(0, 1);
        if (Arena.istrigger == true)
        {
            isCountdownStarted = true;
        }
        if (isMonsterSpawnStart == false)
        {
            StartCoroutine(SpawnMonsters());
        }
    }
    IEnumerator SpawnMonsters()
    {
        while (isCountdownStarted)
        {
            isMonsterSpawnStart = true;
            Instantiate(monster[random_monster], RandomPosition(), Quaternion.identity);
            yield return new WaitForSeconds(10f);
            Instantiate(monster[random_monster], RandomPosition(), Quaternion.identity);
            yield return new WaitForSeconds(10f); // 增加一個隨機等待時間
            StartCoroutine(SpawnMonsters());
        }
    }
    Vector3 RandomPosition()
    {
        float x = Random.Range(-22f, 22f);
        float y = -10f; // 物件高度
        float z = Random.Range(-22f, 22f);
        return new Vector3(x, y, z);
    }
}
