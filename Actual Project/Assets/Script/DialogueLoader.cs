using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class DialogueLoader : MonoBehaviour
{
    public string dbURL = "http://localhost/hackathon/demo.php";
    List<string> allDialog;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(GetDialogData());
    }

    public IEnumerator GetDialogData()
    {
        UnityWebRequest www = UnityWebRequest.Get(dbURL);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string json = www.downloadHandler.text;
            Debug.Log("Json from php: " + json);

            json = "{\"Lines\":" + json + "}";
            Debug.Log("wrraped json: " + json);

            Dialog dialog = JsonUtility.FromJson<Dialog>(json);
            allDialog = dialog.Lines;

            StartCoroutine(DialogueManager.Instance.ShowDialog(allDialog, 0, 7));
        }
        else
        {
            Debug.LogError("Error: " + www.error);
        }
    }

    public void TriggerNextDialog(int startIndex, int count)
    {
        if (allDialog != null && startIndex < allDialog.Count)
        {
            DialogueManager.Instance.StartCoroutine(DialogueManager.Instance.ShowDialog(allDialog, startIndex, count));
        }
    }
}
