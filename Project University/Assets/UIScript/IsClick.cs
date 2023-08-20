using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IsClick : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject TargetObj;
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(ButtonClickReaction);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ButtonClickReaction()
    {
        if (TargetObj != null)
        {
            if (TargetObj.activeSelf)
            {
                TargetObj.SetActive(false);
            }
            else
            {
                TargetObj.SetActive(true);
            }
            // 해당하는 버튼에 맞춰 끄기
        }
    }

    void PauseButton()
    {

    }
}
