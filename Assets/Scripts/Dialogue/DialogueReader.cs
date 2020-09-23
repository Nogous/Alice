using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueReader : MonoBehaviour
{

    List<Dialogue> allDialogues = new List<Dialogue>();
    Dictionary<string, Dialogue> dialogueDictionnary = new Dictionary<string, Dialogue>();

    [SerializeField] private GameObject _dialoguePanel;
    [SerializeField] private Text _dialogueText;
    [SerializeField] private Text _nameText;
    private int _dialogueIndex = 1;
    private string _dialogueContext = "";
    [SerializeField] private GameObject[] _characterTalking;
    private int _previousCharacterTalking;
    [SerializeField] private Transform _characterTalkingPlace;
    [SerializeField] private string _language;

    // Start is called before the first frame update
    void Start()
    {
        TextAsset dialogueData = Resources.Load<TextAsset>("DialogueInfo");

        string[] data = dialogueData.text.Split(new char[] { '\n' });

        for(int i = 1; i < data.Length; i++)
        {
            string[] row = data[i].Split(new char[] { ',' });
            Dialogue dial = new Dialogue();
            dial.key = row[0];
            int.TryParse(row[0], out dial.id);
            dial.name = row[1];
            dial.dialogue = row[2];
            int.TryParse(row[3], out dial.characterModel);

            allDialogues.Add(dial);
            dialogueDictionnary.Add(dial.key, dial);
        }

        if(ValueHolder.instance)
            _language = ValueHolder.instance.gameLanguage;

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.onDebugMode && Input.GetKeyUp(KeyCode.O))
        {
            CheckDialogue("BLA", 4);
        }
        else if (GameManager.instance.onDebugMode && Input.GetKeyUp(KeyCode.L))
        {
            CheckDialogue(_dialogueContext, _dialogueIndex);
        }
    }

    private void CheckDialogue(string context, int id)
    {
        string temp = id.ToString("D2");
        string dialogueKey = _language + "_" + context + "_" + temp;
        if (dialogueDictionnary[dialogueKey].dialogue != "end")
        {
            if (dialogueDictionnary[dialogueKey].characterModel != _previousCharacterTalking || !_characterTalkingPlace.gameObject.activeSelf)
            {
                for (int i = 0; i < _characterTalkingPlace.childCount; i++)
                {
                    Destroy(_characterTalkingPlace.GetChild(i).gameObject);
                }
                Instantiate(_characterTalking[dialogueDictionnary[dialogueKey].characterModel], _characterTalkingPlace);
            }
            _dialogueIndex = id;
            _dialogueContext = context;
            _dialogueText.text = dialogueDictionnary[dialogueKey].dialogue;
            _nameText.text = dialogueDictionnary[dialogueKey].name;
            _dialoguePanel.SetActive(true);
            _characterTalkingPlace.gameObject.SetActive(true);
            _previousCharacterTalking = dialogueDictionnary[dialogueKey].characterModel;
            _dialogueIndex += 1;
        }
        else
        {
            _dialoguePanel.SetActive(false);
            _characterTalkingPlace.gameObject.SetActive(false);
            _dialogueIndex = 1;
        }
    }
}
