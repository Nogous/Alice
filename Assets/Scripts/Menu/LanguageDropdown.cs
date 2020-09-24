using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageDropdown : MonoBehaviour
{

    private Dropdown drop;
    //public static LanguageDropdown instance;

    private void Awake()
    {
        //instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        drop = GetComponent<Dropdown>();
        ValueHolder.instance.languageDropdown = drop;
        drop.value = ValueHolder.instance.gameLanguageId;
    }

    public void ChangeGameLanguage(int newOption)
    {
        ValueHolder.instance.ChangeGameLanguage(newOption);
    }
}
