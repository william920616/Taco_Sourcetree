using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    public GameObject[] fruits; // 保存三種水果的預置物件
    public Transform[] spawnPoints; // 保存四個掉落位置
    public AudioSource rollAudio;
    public float spawnInterval = 5f; // 每次生成水果的時間間隔
    private float nextSpawnTime = 0f; // 下一次生成水果的時間

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnFruits();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnFruits()
    {
        rollAudio.Play();
        // 隨機選擇一個水果預置物件
        int randomIndex = Random.Range(0, fruits.Length);
        GameObject selectedFruit = fruits[randomIndex];

        // 在四個位置中隨機選擇一個位置生成水果
        int randomSpawnPointIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomSpawnPointIndex];

        // 生成水果
        Instantiate(selectedFruit, spawnPoint.position, Quaternion.identity);


    }
}
