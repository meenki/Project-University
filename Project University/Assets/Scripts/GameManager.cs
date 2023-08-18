using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Schema;
using TMPro;
using UnityEngine;

public enum DiceType
{
    Fire = 0,
    Wind,
    Volt,
    Ice,
    Boss,
    End
}

public enum EnemyType
{
    Normal,
    Speed,
    Boss
}

[System.Serializable]
public class DiceInfo
{
    public DiceType Type;
    public float damage;
    public float attackSpeed;
}

public class GameManager : MonoBehaviour
{
    public Generator generator;

    public TextMeshProUGUI waveText;
    public TextMeshProUGUI lifeText;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI summonCostText;

    public Transform[] diceBoard;
    public DiceInfo[] diceInfo = new DiceInfo[(int)DiceType.End];
    public int startSummonCost = 150;
    public int increaseSummonCost = 10;

    public int[] upgradeCost = { 50, 100, 200, 400 };
    public float[] upgradeDamage = { 1.0f, 1.2f, 1.4f, 1.6f, 1.8f };
    public float[] levelDamage = { 1.0f, 1.5f, 2.0f, 2.5f, 3.0f };

    public int startLife = 10;
    public int startMoney = 450;
    public int killMoney = 10;

    [HideInInspector]
    public int[] diceLevel = new int[(int)DiceType.End];

    int wave = 0;
    int money = 0;
    int life = 0;
    int summonCount = 0;
    bool[] isInDice;

    // 쥐 개수 10 + 2 * ( Wave - 1 )
    // 쥐 체력 50 + 25 * ( Wave - 1 )
    // 보스 체력 = 50 * ( wave - 1 ) ^ 2 + 250 * ( wave - 1 ) + 200
    // 해당 웨이브 쥐 잡을 시 골드 = 이전 웨이브 쥐 마리 당 골드 + 웨이브

    private static GameManager instance = null;
    public static GameManager Instance
    {
        get
        {
            return instance; 
        }
        private set { instance = value; }
    }

    public void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }

        wave = 1;
        money = startMoney;
        life = startLife;
        summonCount = startSummonCost;
        isInDice = new bool[diceBoard.Length];

        for(int i = 0; i < diceLevel.Length; ++i)
        {
            diceLevel[i] = 1;
        }

        waveText.text = wave.ToString();
        lifeText.text = life.ToString();
        moneyText.text = money.ToString();
        summonCostText.text = summonCount.ToString();

        WaveStart();
    }

    public void NewDice()
    {
        if (money >= startSummonCost + summonCount * increaseSummonCost)
        {
            while (true)
            {
                int rand = Random.Range(0, isInDice.Length);
                if (isInDice[rand])
                {
                    continue;
                }
                else
                {
                    isInDice[rand] = true;

                    break;
                }
            }

            money -= startSummonCost + summonCount * increaseSummonCost;
            summonCount++;
        }
    }

    public bool UpgradeDice(DiceType type)
    {
        if (diceLevel[(int)type] >= 5) return false;

        if (upgradeCost[diceLevel[(int)type] - 1] <= money)
        {
            diceLevel[(int)type]++;
            money -= upgradeCost[diceLevel[(int)type]];
            moneyText.text = money.ToString();
            return true;
        }
        return false;
    }

    public void WaveStart()
    {

    }

    public void DecreaseLife()
    {
        --life;
        if(life == 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {

    }
}
