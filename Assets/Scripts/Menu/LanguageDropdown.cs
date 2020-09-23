using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageDropdown : MonoBehaviour
{

    private Dropdown drop;

    // Start is called before the first frame update
    void Start()
    {
        drop = GetComponent<Dropdown>();
        drop.value = ValueHolder.instance.gameLanguageId;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
