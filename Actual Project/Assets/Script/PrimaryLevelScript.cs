using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System;

public class PrimaryLevelScript : MonoBehaviour
{
    public string primaryURL = "http://localhost/hackathon/primarylevel.php";
    List<string> allDemo;
    public int primaryStart = 0;
    public int primaryLineCount = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadDemo(primaryStart, primaryLineCount);
    }

    public void LoadDemo(int startIndex, int count, bool isInfoDialogue = false)
    {
        StartCoroutine(GetPrimaryDialog(startIndex, count, isInfoDialogue));
    }

    public IEnumerator GetPrimaryDialog(int startIndex, int count, bool isInfoDialogue = false)
    {
        UnityWebRequest www = UnityWebRequest.Get(primaryURL);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string json = www.downloadHandler.text;
            Debug.Log("Json from php: " + json);

            json = "{\"Lines\":" + json + "}";
            Debug.Log("wrraped json: " + json);

            Dialog dialog = JsonUtility.FromJson<Dialog>(json);
            allDemo = dialog.Lines;

            StartCoroutine(DialogueManager.Instance.ShowDialog(allDemo, startIndex, count, isInfoDialogue));
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

}