using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    public DiceType type;
    public TextMeshProUGUI cost;
    public TextMeshProUGUI levelText;

    int level;
    bool isMax = false;
    void Start()
    {
        level = GameManager.Instance.diceLevel[(int)type];
        cost.text = GameManager.Instance.upgradeCost[level - 1].ToString();
        levelText.text = level.ToString();
    }

    public void UpgradeDice()
    {
        if (isMax)
            return;

        if (GameManager.Instance.UpgradeDice(type))
        {
            level = GameManager.Instance.diceLevel[(int)type];

            Debug.Log(level);
            cost.text = GameManager.Instance.upgradeCost[level - 1].ToString();
            levelText.text = level.ToString();

            if (GameManager.Instance.upgradeCost[level - 1] == 0)
            {
                isMax = true;
                cost.text = "Max";
                levelText.text = "5";
                return;
            }
        }
    }
}
