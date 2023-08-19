using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiceButton : MonoBehaviour
{
    public void NewDice()
    {
        GameManager.Instance.NewDice();
    }
}
