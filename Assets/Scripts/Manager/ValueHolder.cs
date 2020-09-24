using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueHolder : MonoBehaviour
{

    public static ValueHolder instance;

    public string gameLanguage = "ENG";
    public int gameLanguageId = 0;
    public Dropdown languageDropdown;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ChangeGameLanguage(int newOption)
    {
        print("change");
        gameLanguageId = newOption;
        gameLanguage = languageDropdown.options[gameLanguageId].text;
    }
}
