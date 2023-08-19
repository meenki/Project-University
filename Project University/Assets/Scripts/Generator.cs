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

    public int enemyDefaultHP = 50;
    public int enemyMultipleHP = 25;
    public int bossBouseHP = 200;
    public int bossMultipleHP = 250;
    public float enemyDefaultSpeed = 1;
    public int enemyDefaultSpawnCount = 10;

    public int boxNum = 100;
    public float spawnTime = 1;

    int curWave = 0;
    [HideInInspector]
    public int curEnemy = 0;
    [HideInInspector]
    public int spawnEnemy = 0;
    int preSpawnCount = 0;
    float elapsed = 0;

    public void Awake()
    {
        enemyBox = new GameObject[boxNum];
        for (int i = 0; i < boxNum; i++)
        {
            enemyBox[i] = Instantiate(enemyPrefab, enemyParent);
            enemyBox[i].GetComponent<Enemy>().enemyWay = enemyWay;
            enemyBox[i].SetActive(false);
        }
    }

    // Áã °³¼ö 10 + 2 * ( Wave - 1 )
    // Áã Ã¼·Â 50 + 25 * ( Wave - 1 )
    // º¸½º Ã¼·Â = 50 * ( wave - 1 ) ^ 2 + 250 * ( wave - 1 ) + 200

    public void WaveStart(int wave)
    {
        curWave = wave;
        if (preSpawnCount == 0)
        {
            spawnEnemy = 10;
            preSpawnCount = 10;
        }
        else
        {
            if (curWave % 5 == 0)
            {
                spawnEnemy = 1;
            }
            else
            {
                spawnEnemy = preSpawnCount += 2 * (curWave - 1);
            }
        }
    }

    public void SpawnEnemy()
    {
        for (int i = 0; i < enemyBox.Length; i++)
        {
            if (!enemyBox[i].activeSelf)
            {
                if (curWave % 5 == 0)
                {
                    enemyBox[i].GetComponent<Enemy>().ActiveEnemy
                        (
                        EnemyType.Boss,
                        enemyDefaultHP * (curWave - 1) ^ 2 + bossMultipleHP * (curWave - 1) + bossBouseHP,
                        enemyDefaultSpeed
                        );
                }
                else
                {
                    var type = Random.Range((int)EnemyType.Normal, (int)EnemyType.Boss);
                    enemyBox[i].GetComponent<Enemy>().ActiveEnemy
                        (
                        (EnemyType)type,
                        enemyDefaultHP + enemyMultipleHP * (curWave - 1),
                        enemyDefaultSpeed
                        );
                }

                spawnEnemy--;
                curEnemy++;
                break;
            }
        }
    }

    public void Update()
    {
        elapsed += Time.deltaTime;
        if (spawnEnemy > 0)
        {
            if (elapsed > spawnTime)
            {
                SpawnEnemy();
                elapsed = 0;
            }
        }
    }
}
