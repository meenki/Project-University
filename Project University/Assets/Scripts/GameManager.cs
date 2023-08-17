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
    public int[] diceLevel = new int[(int)DiceType.End];
    public int startSummonCost = 150;
    public int increaseSummonCost = 10;

    public int upgradeCost = 50;
    public int startLife = 10;
    public int startMoney = 450;

    int wave = 0;
    int money = 0;
    int life = 0;
    int summonCount = 0;
    bool[] isInDice;

    // Áã °³¼ö 10 + 2 * (Wave-1)
    // Áã Ã¼·Â 50 + 25 * (Wave-1)
    // º¸½º Ã¼·Â = 50 * ( wave ? 1 ) ^2 + 250 * ( wave ? 1 ) + 200

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

        waveText.text = wave.ToString();
        lifeText.text = life.ToString();
        moneyText.text = money.ToString();
        summonCostText.text = summonCount.ToString();

        WaveStart();
    }

    public void NewDice()
    {

    }

    public void WaveStart()
    {

    }

    public void GameOver()
    {

    }
}
