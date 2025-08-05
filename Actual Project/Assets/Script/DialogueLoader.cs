using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class DialogueLoader : MonoBehaviour
{
    public string demoURL = "http://localhost/hackathon/demo.php";
    public string infoURL = "http://localhost/hackathon/info.php";
    List<string> allDemo;
    List<string> allInfo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(GetDemoDialog());
    }

    public IEnumerator GetDemoDialog()
    {
        UnityWebRequest www = UnityWebRequest.Get(demoURL);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string json = www.downloadHandler.text;
            Debug.Log("Json from php: " + json);

            json = "{\"Lines\":" + json + "}";
            Debug.Log("wrraped json: " + json);

            Dialog dialog = JsonUtility.FromJson<Dialog>(json);
            allDemo = dialog.Lines;

            StartCoroutine(DialogueManager.Instance.ShowDialog(allDemo, 0, 7));
        }
        else
        {
            Debug.LogError("Error: " + www.error);
        }
    }

    public IEnumerator GetInfoDialog()
    {
        UnityWebRequest www = UnityWebRequest.Get(infoURL);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string json = www.downloadHandler.text;
            Debug.Log("Json from php: " + json);

            json = "{\"Lines\":" + json + "}";
            Debug.Log("wrraped json: " + json);

            Dialog dialog = JsonUtility.FromJson<Dialog>(json);

            if (dialog.Lines != null && dialog.Lines.Count > 0)
            {
                List<string> splitSentences = SplitIntoSentences(dialog.Lines[0]);

                StartCoroutine(DialogueManager.Instance.ShowDialog(splitSentences, 0, splitSentences.Count));
            }
        }
        else
        {
            Debug.LogError("Error: " + www.error);
        }
    }

    private List<string> SplitIntoSentences(string block)
    {
        List<string> sentences = new List<string>();
        string[] split = block.Split(new[] { '.', '!' });

        foreach (string s in split)
        {
            string trim = s.Trim();
            if (!string.IsNullOrEmpty(trim))
            {
                sentences.Add(trim + ".");
            }
        }

        return sentences;
    }

    public void TriggerNextDialog(int startIndex, int count)
    {
        if (allDemo != null && startIndex < allDemo.Count)
        {
            DialogueManager.Instance.StartCoroutine(DialogueManager.Instance.ShowDialog(allDemo, startIndex, count));
        }
    }

    public void TriggerInfoDialog()
    {
        StartCoroutine(GetInfoDialog());
    }
}
