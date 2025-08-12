using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class leaderboardManager : MonoBehaviour
{
    public string leaderboardURL = "http://localhost/hackathon/leaderboard.php";

    public Transform tableContainer;
    public GameObject rowPrefab;

    public class Leader
    {
        public string email;
        public string score;
    }

    public class LeaderList
    {
        public List<Leader> leaders;
    }

    void Start()
    {
        StartCoroutine(LoadLeaderboard());
    }

    IEnumerator LoadLeaderboard()
    {
        UnityWebRequest www = UnityWebRequest.Get(leaderboardURL);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string json = www.downloadHandler.text;
            Debug.Log("Json from php: " + json);

            json = "{\"leaders\":" + json + "}";
            Debug.Log("wrraped json: " + json);

            LeaderList leaderList = JsonUtility.FromJson<LeaderList>(json);

            foreach (var leader in leaderList.leaders)
            {
                GameObject row = Instantiate(rowPrefab, tableContainer);
                Text[] texts = row.GetComponentsInChildren<Text>();
                if (texts.Length >= 2)
                {
                    texts[0].text = leader.email;
                    texts[1].text = leader.score;
                }
            }
        }
        else
        {
            Debug.LogError("Error: " + www.error);
        }
    }
}
