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
    public string primaryURL = "http://localhost/hackathon/primarylevel.php";
    List<string> allDemo;
    List<string> allInfo;
    public int demoStart = 0;
    public int demoLineCount = 4;
    [SerializeField] private string lastScene;
    private string currentScene;
    public GameObject demoPlayer;
    public GameObject player;
    private List<string> collectedInfo = new List<string>();
    private static DialogueLoader instance;
    public GameObject shardIcon;
    public GameObject infoIcon;

    // 🔹 Your PHP endpoint
    private string updateLastSceneURL = "http://localhost/hackathon/update_last_scene.php";


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void onSceneLoad(Scene scene, LoadSceneMode mode)
    {
        currentScene = scene.name;
        StartCoroutine(UpdateLastSceneInDB(lastScene));

        if (currentScene.Equals("GameDemo", StringComparison.OrdinalIgnoreCase))
        {
            StartCoroutine(GameDemoDialog());

        }
        else if (currentScene.Equals("instruction", StringComparison.OrdinalIgnoreCase))
        {
            StartCoroutine(instructionDialog());
        }

        else if (currentScene.Equals("PrimaryLevelEvidence", StringComparison.OrdinalIgnoreCase))
        {
            LoadDemo(12, 3, false);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += onSceneLoad;
    }

    private IEnumerator GameDemoDialog()
    {
        yield return StartCoroutine(GetDemoDialog(0, 3, false));
        yield return new WaitForSeconds(5f);
        yield return StartCoroutine(GetDemoDialog(3, 2, false));
        yield return new WaitForSeconds(5f);
        yield return StartCoroutine(GetDemoDialog(5, 1, false));
    }

    private IEnumerator instructionDialog()
    {
        yield return StartCoroutine(GetDemoDialog(6, 6, false));
        SceneManager.LoadScene("PrimaryLevelEvidence");
    }

      void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
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

            if (demoPlayer != null)
            {
                demoPlayer.SetActive(true);
                player.SetActive(false);
            }

            yield return DialogueManager.Instance.ShowDialog(allDemo, startIndex, count, isInfoDialogue, () =>
            {
                if (demoPlayer != null)
                {
                    demoPlayer.SetActive(false);
                    player.SetActive(true);
                    infoIcon.SetActive(false);
                }
            });
        }
        else
        {
            Debug.LogError("Error: " + www.error);
        }
    }

    public IEnumerator GetInfoDialog(GameObject infoIcon, playerMove movementScript,string URL)
    {
        if (movementScript != null)
        {
            movementScript.enabled = false;
        }

        UnityWebRequest www = UnityWebRequest.Get(URL);
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
                List<string> splitSentences = SplitIntoSentences(fullInfo);

                if (!collectedInfo.Contains(fullInfo))
                {
                    collectedInfo.Add(fullInfo);
                }

                yield return StartCoroutine(DialogueManager.Instance.ShowDialog(splitSentences, 0, splitSentences.Count, true, () =>
                {
                    infoIcon.SetActive(false);

                    if (movementScript != null)
                    {
                        movementScript.enabled = true;
                    }

                    if (currentScene.Equals("GameDemo", StringComparison.OrdinalIgnoreCase))
                    {
                        LoadDemo(7, 2, false);
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
    public IEnumerator GetShardDialog(int startIndex, int count, bool isInfoDialogue = false)
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

            if (demoPlayer != null)
            {
                demoPlayer.SetActive(true);
                player.SetActive(false);
            }

            yield return DialogueManager.Instance.ShowDialog(allDemo, startIndex, count, isInfoDialogue, () =>
            {
                if (demoPlayer != null)
                {
                    demoPlayer.SetActive(false);
                    player.SetActive(true);
                    infoIcon.SetActive(false);
                }
            });
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

    public void onEvidenceIncrease()
    {
        if (currentScene.Equals("GameDemo", StringComparison.OrdinalIgnoreCase))
        {
            LoadDemo(9, 6, false);
        }

    }

    public void TriggerNextDialog(int startIndex, int count)
    {
        if (allDemo != null && startIndex < allDemo.Count)
        {
            DialogueManager.Instance.StartCoroutine(DialogueManager.Instance.ShowDialog(allDemo, startIndex, count));
        }
    }

    public void TriggerInfoDialog(GameObject infoIcon, playerMove movementScript, string level)
    {
        string gameLevel;

        if (currentScene.Equals("PrimaryLevelEvidence", StringComparison.OrdinalIgnoreCase))
        {
            StartCoroutine(GetInfoDialog(infoIcon, movementScript, primaryURL));
        }
        else
        {
            gameLevel = infoURL + "?level=" + Uri.EscapeDataString(level);
            StartCoroutine(GetInfoDialog(infoIcon, movementScript, gameLevel));
        }

    }

    public void TriggerShardDialog(GameObject infoIcon, playerMove movementScript, string level)
    {
        string gameLevel;

        gameLevel = infoURL + "?level=" + Uri.EscapeDataString(level);
        if (currentScene.Equals("PrimaryLevelEvidence", StringComparison.OrdinalIgnoreCase))
        {
            StartCoroutine(GetShardDialog(6, 2, false));
        }
        if (currentScene.Equals("SecondaryLevelEvidence", StringComparison.OrdinalIgnoreCase))
        {
            StartCoroutine(GetShardDialog(8, 2, false));
        }
        if (currentScene.Equals("UniLevelEvidence", StringComparison.OrdinalIgnoreCase))
        {
            StartCoroutine(GetShardDialog(10, 1, false));
        }
        
    }

    public List<string> getCollectedInfo()
    {
        return collectedInfo;
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

