using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class leaderboardManager : MonoBehaviour
{
    public string leaderboardURL = "http://localhost/hackathon/leaderboard.php";
    public Text leaderboardText;

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

            string table = FormatAsTable(leaderList.leaders);
            leaderboardText.text = table;
        }
        else
        {
            Debug.LogError("Error: " + www.error);
        }
    }

    string FormatAsTable(List<Leader> leaders)
    {
        string table = "<b>Email</b>\t\t\t\t<b>Score<b>\n";
        table += "----------------------------------------\n";

        foreach (Leader leader in leaders)
        {
            table += $"{leader.email,-30}\t{leader.score,3}\n";
        }

        return table;
    }
}
