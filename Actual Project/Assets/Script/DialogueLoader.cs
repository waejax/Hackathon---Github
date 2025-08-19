using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class DialogueLoader : MonoBehaviour
{
    public string demoURL = "http://localhost/hackathon/demo.php";
    public string infoURL = "http://localhost/hackathon/info.php";
    List<string> allDemo;
    List<string> allInfo;
    public int demoStart = 0;
    public int demoLineCount = 5;
    [SerializeField] private string lastScene;
    private string currentScene;
    // 🔹 Your PHP endpoint
    private string updateLastSceneURL = "http://localhost/hackathon/update_last_scene.php";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
        StartCoroutine(UpdateLastSceneInDB(lastScene));
        
        if (currentScene.Equals("GameDemo", StringComparison.OrdinalIgnoreCase))
        {
            LoadDemo(demoStart, demoLineCount);
        }
    }

    public void LoadDemo(int startIndex, int count, bool isInfoDialogue = false)
    {
        StartCoroutine(GetDemoDialog(startIndex, count, isInfoDialogue));
    }

    public IEnumerator GetDemoDialog(int startIndex, int count, bool isInfoDialogue = false)
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

            StartCoroutine(DialogueManager.Instance.ShowDialog(allDemo, startIndex, count, isInfoDialogue));
        }
        else
        {
            Debug.LogError("Error: " + www.error);
        }
    }

    public IEnumerator GetInfoDialog(GameObject infoIcon, playerMove movementScript)
    {
        if (movementScript != null)
        {
            movementScript.enabled = false;
        }

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
                string fullInfo = dialog.Lines[0];
                List<string> splitSentences = SplitIntoSentences(dialog.Lines[0]);

                yield return StartCoroutine(DialogueManager.Instance.ShowDialog(splitSentences, 0, splitSentences.Count, true, () =>
                {
                    infoIcon.SetActive(false);

                    if (movementScript != null)
                    {
                        movementScript.enabled = true;
                    }

                    if (currentScene.Equals("GameDemo", StringComparison.OrdinalIgnoreCase))
                    {
                        LoadDemo(5, 2, true);
                    }
                }));
            }
        }
        else
        {
            Debug.LogError("Error: " + www.error);

            if (movementScript != null)
            {
                movementScript.enabled = true;
            }
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

    public void onEvidenceIncrease()
    {
        if (currentScene.Equals("GameDemo", StringComparison.OrdinalIgnoreCase))
        {
            LoadDemo(7, 6, true);
        }
    }

    public void TriggerNextDialog(int startIndex, int count)
    {
        if (allDemo != null && startIndex < allDemo.Count)
        {
            DialogueManager.Instance.StartCoroutine(DialogueManager.Instance.ShowDialog(allDemo, startIndex, count));
        }
    }

    public void TriggerInfoDialog(GameObject infoIcon, playerMove movementScript)
    {
        StartCoroutine(GetInfoDialog(infoIcon, movementScript));
    }

    IEnumerator UpdateLastSceneInDB(string sceneName)
    {
        WWWForm form = new WWWForm();
        form.AddField("userID", GameManager.Instance.userID);
        form.AddField("lastScene", sceneName);

        UnityWebRequest www = UnityWebRequest.Post(updateLastSceneURL, form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Last scene updated to: " + sceneName);
        }
        else
        {
            Debug.LogError("Failed to update last scene: " + www.error);
        }
    }
}

