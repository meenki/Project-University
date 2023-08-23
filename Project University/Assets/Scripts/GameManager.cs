using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Schema;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

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
    Boss,
    End
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

    public GameObject DoneObj;
    public TextMeshProUGUI ResultText;
    public TextMeshProUGUI PauseText;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI lifeText;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI summonCostText;

    public GameObject bulletPrefab;
    public Transform bulletParent;
    public GameObject[] bulletBox;
    public int bulletNum;

    public Sprite[] fireDice;
    public Sprite[] windDice;
    public Sprite[] voltDice;
    public Sprite[] iceDice;
    public Sprite[] bossDice;

    public Dice[] diceBoard;
    public DiceInfo[] diceInfo = new DiceInfo[(int)DiceType.End];
    public int startSummonCost = 150;
    public int increaseSummonCost = 10;

    public int[] upgradeCost = { 50, 100, 200, 400 };
    public float[] upgradeDamage = { 1.0f, 1.2f, 1.4f, 1.6f, 1.8f };
    public float[] levelDamage = { 1.0f, 1.5f, 2.0f, 2.5f, 3.0f };

    public int startLife = 10;
    public int startMoney = 450;
    public int startKillMoney = 10;

    [HideInInspector]
    public int curKillMoney = 10;

    [HideInInspector]
    public int[] diceLevel = new int[(int)DiceType.End];

    public GameObject killPrefab;


    int wave = 0;
    int money = 0;
    int life = 0;
    int summonCount = 0;
    bool[] isInDice;

    GameObject hitObject = null;
    Vector3 objPrePos = Vector3.zero;

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
        isInDice = new bool[diceBoard.Length];

        for (int i = 0; i < diceLevel.Length; ++i)
        {
            diceLevel[i] = 1;
        }

        bulletBox = new GameObject[bulletNum];
        for (int i = 0; i < bulletNum; i++)
        {
            bulletBox[i] = Instantiate(bulletPrefab, bulletParent);
            bulletBox[i].SetActive(false);
        }

        waveText.text = wave.ToString();
        PauseText.text = wave.ToString();
        ResultText.text = wave.ToString();
        lifeText.text = life.ToString();
        moneyText.text = money.ToString();
        summonCostText.text = startSummonCost.ToString();

        WaveStart();
    }

    public void NewDice()
    {
        if (money >= startSummonCost + summonCount * increaseSummonCost)
        {
            bool isEmpty = false;
            for (int i = 0; i < isInDice.Length; ++i)
            {
                if (isInDice[i] == false)
                {
                    isEmpty = true;
                    break;
                }
            }
            if (!isEmpty)
            {
                return;
            }

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
                    var type = Random.Range(0, (int)DiceType.End);
                    diceBoard[rand].InitDice(diceInfo[type]);

                    break;
                }
            }

            money -= startSummonCost + summonCount * increaseSummonCost;
            summonCount++;
            moneyText.text = money.ToString();
            summonCostText.text = (startSummonCost + summonCount * increaseSummonCost).ToString();
        }
    }

    public void Attack(Dice dice, Enemy target)
    {
        if (dice.type == DiceType.Volt)
        {
            int count = 2;
            for (int i = 0; i < bulletBox.Length; ++i)
            {
                if (!bulletBox[i].activeSelf)
                {
                    for (int j = 0; j < generator.enemyBox.Length; ++j)
                    {
                        if (generator.enemyBox[j].activeSelf)
                        {
                            if (generator.enemyBox[j] == target) continue;
                            bulletBox[i].GetComponent<Bullet>().Shot(dice, generator.enemyBox[i].GetComponent<Enemy>());

                            count--;
                            if (count == 0) break;
                        }
                    }
                }
            }
        }
        for (int i = 0; i < bulletBox.Length; ++i)
        {
            if (!bulletBox[i].activeSelf)
            {
                bulletBox[i].GetComponent<Bullet>().Shot(dice, target);
                break;
            }
        }
    }

    public Enemy GetTarget()
    {
        Enemy target = null;
        for (int i = 0; i < generator.enemyBox.Length; ++i)
        {
            if (generator.enemyBox[i].activeSelf)
            {
                if (target == null)
                    target = generator.enemyBox[i].GetComponent<Enemy>();
                else
                {
                    if (target.distance < generator.enemyBox[i].GetComponent<Enemy>().distance)
                    {
                        target = generator.enemyBox[i].GetComponent<Enemy>();
                    }
                }

            }
        }

        return target;
    }

    public bool UpgradeDice(DiceType type)
    {
        if (diceLevel[(int)type] >= 5) return false;

        if (upgradeCost[diceLevel[(int)type] - 1] <= money)
        {
            money -= upgradeCost[diceLevel[(int)type] - 1];
            diceLevel[(int)type]++;
            moneyText.text = money.ToString();
            return true;
        }
        return false;
    }

    public void WaveStart()
    {
        generator.WaveStart(wave);
        waveText.text = wave.ToString();
        PauseText.text = wave.ToString();
        ResultText.text = wave.ToString();
    }

    // 해당 웨이브 쥐 잡을 시 골드 = 이전 웨이브 쥐 마리 당 골드 + 웨이브
    public void KillEnemy()
    {
        generator.curEnemy--;
        money += curKillMoney;
        moneyText.text = money.ToString();

        if (isWaveEnd())
        {
            WaveStart();
        }
    }

    public void DecreaseLife()
    {
        life--;
        generator.curEnemy--;
        lifeText.text = life.ToString();

        if (life == 0)
        {
            GameOver();
        }

        if (isWaveEnd())
        {
            WaveStart();
        }
    }

    public bool isWaveEnd()
    {
        if (generator.curEnemy > 0 || generator.spawnEnemy > 0)
        {
            return false;
        }

        ++wave;

        return true;
    }

    public void Update()
    {
        if(hitObject != null)
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hitObject.transform.position = worldPoint;
        }

        if(Input.GetMouseButtonDown(0))
        {
            if (hitObject == null)
            {
                RayCast(Input.mousePosition);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if(hitObject != null)
            {
             

                hitObject.GetComponent<Dice>().DropDice();
                hitObject.transform.position = objPrePos;
                Destroy(hitObject.GetComponent<Rigidbody2D>());
                hitObject.GetComponent<Dice>().isSelected = false;
            }
            
            hitObject = null;
        }
    }

    void RayCast(Vector3 mousePos)
    {
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(mousePos);
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

        if (hit.collider != null)
        {
            if (hit.collider.GetComponent<Dice>() != null)
            {
                objPrePos = hit.collider.transform.position;
                hitObject = hit.collider.gameObject;
                hitObject.GetComponent<Dice>().isSelected = true;
                hitObject.AddComponent<Rigidbody2D>();
            }
        }
    }

    public void GameOver()
    {
        // 결과창
        DoneObj.SetActive(true);
    }
}
