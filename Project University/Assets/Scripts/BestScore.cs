using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; //UI���� ���̺귯�� 

public class BestScore : MonoBehaviour
{
    public TextMeshProUGUI bestScoreText;

    // Start is called before the first frame update
    void Start()
    {
        int bestScore = PlayerPrefs.GetInt("BestScore");
        bestScoreText.text = bestScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
