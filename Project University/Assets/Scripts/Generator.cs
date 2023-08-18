using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public Transform[] enemyWay;
    public GameObject enemyPrefab;
    [HideInInspector]
    public GameObject[] enemyBox;
    public Transform enemyParent;

    public int boxNum = 100;
    public float spawnTime = 1;

    int curEnemy;
    int spawnEnemy;
    int preSpawnCount = 0;

    public void Awake()
    {
        enemyBox = new GameObject[boxNum];
        for(int i = 0; i < boxNum; i++)
        {
            enemyBox[i] = Instantiate(enemyPrefab, enemyParent);
            enemyBox[i].GetComponent<Enemy>().enemyWay = enemyWay;
            enemyBox[i].SetActive(false);
        }
    }

    public void StartWave(int wave)
    {
        if (preSpawnCount == 0)
        {
            spawnEnemy = 10;
            preSpawnCount = 10;
        }
        else
        {
            spawnEnemy = preSpawnCount += wave;
        }
    }

    public void SpawnEnemy()
    {

    }

    public void Update()
    {
        
    }
}
