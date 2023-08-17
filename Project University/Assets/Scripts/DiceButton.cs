using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiceButton : MonoBehaviour
{
    public DiceType type;
    public TextMeshProUGUI cost;
    public TextMeshProUGUI level;

    void Awake()
    {
        cost.text = GameManager.Instance.upgradeCost.ToString();
        level.text = GameManager.Instance.diceLevel[(int)type].ToString();
    }
}
