using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class CVSDownloader : MonoBehaviour
{

    //public static CVSDownloader instance;


    //private const string _GOOGLESHEETDOCID = "1PTdPvnHiEjmBzljaf-DDchdQmsrK98eaOehcriwqz9o";
    //private const string _URL = "https://docs.google.com/spreadsheets/d/" + _GOOGLESHEETDOCID + "/export?format=csv";

    //private string _filename;
    //private string _folderPath;

    //private void Awake()
    //{
    //    if (instance != null && instance != this)
    //    {
    //        Destroy(this.gameObject);
    //        return;
    //    }
    //    instance = this;
    //}

    //private void Start()
    //{
    //    _filename = "Dialogue" + ".csv";
    //    _folderPath = Path.Combine(Application.dataPath, _filename);
    //    StartCoroutine(DownloadDialogueData());
    //}

    //public IEnumerator DownloadDialogueData()
    //{
    //    yield return new WaitForEndOfFrame();

    //    string downloadData = null;
    //    using (UnityWebRequest webRequest = UnityWebRequest.Get(_URL))
    //    {

    //        yield return webRequest.SendWebRequest();

    //        if (webRequest.isNetworkError)
    //        {
    //            Debug.Log("Download Error: " + webRequest.error);
    //            downloadData = PlayerPrefs.GetString("LastDataDownloaded", null);
    //        }
    //        else
    //        {
    //            Debug.Log("Download success");
    //            Debug.Log("Data: " + webRequest.downloadHandler.text);
    //            PlayerPrefs.SetString("LastDataDownloaded", webRequest.downloadHandler.text);

    //        }
    //    }

    //    SaveDialogue(UnityWebRequest.Get(_URL).downloadHandler.text);
    //}

    //private void SaveDialogue(string textToSave)
    //{
    //    File.WriteAllText(_folderPath, textToSave);
    //}

    //private void Update()
    //{
    //    if (Input.GetKeyUp(KeyCode.A))
    //    {

    //    }
    //}

}
