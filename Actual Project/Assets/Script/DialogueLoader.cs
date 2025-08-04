using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class DialogueLoader : MonoBehaviour
{
    public Text bandar;
    public Text kb;

    public string dbURL = "http://localhost/hackathon/demo.php";

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

            StartCoroutine(DialogueManager.Instance.ShowDialog(dialog));
        }
        else
        {
            Debug.LogError("Error: " + www.error);
        }
    }
}
