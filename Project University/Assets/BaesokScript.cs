using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BaesokScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject TextObj;
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(AddText);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddText()
    {
        if (TextObj.GetComponent<TextMeshProUGUI>().text == "x1")
            TextObj.GetComponent<TextMeshProUGUI>().text = "x2";
        else if (TextObj.GetComponent<TextMeshProUGUI>().text == "x2")
            TextObj.GetComponent<TextMeshProUGUI>().text = "x5";
        else if (TextObj.GetComponent<TextMeshProUGUI>().text == "x5")
            TextObj.GetComponent<TextMeshProUGUI>().text = "x1";
        // 조건문 안에 원하는 기능 추가

    }
}
