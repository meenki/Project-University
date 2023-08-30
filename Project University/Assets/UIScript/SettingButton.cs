using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingButton : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject settingwindow;
    void Start()
    {
        Button b = GetComponent<Button>();
        settingwindow.SetActive(false);
        b.onClick.AddListener(ActiveSetting);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ActiveSetting()
    {
        if (settingwindow.activeSelf)
        {
            Time.timeScale = 1.0f;
            settingwindow.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            settingwindow.SetActive(true);
        }
    }
}
