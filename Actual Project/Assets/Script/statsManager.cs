using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
public class statsManager : MonoBehaviour
{
    public string statsURL = "http://localhost/hackathon/leaderboard.php";
    public Text totalText;
    public Text avgScoreText;

    [System.Serializable]
    public class Stats
    {
        public int total;
        public float avgScore;
    }

    [System.Serializable]
    public class ResponseData
    {
        public Stats stats;
    }

    void Start()
    {
        StartCoroutine(LoadStats());
    }

    IEnumerator LoadStats()
    {
        UnityWebRequest www = UnityWebRequest.Get(statsURL);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string json = www.downloadHandler.text;
            Debug.Log("Json from php: " + json);

            ResponseData response = JsonUtility.FromJson<ResponseData>(json);

            if (response != null && response.stats != null)
            {
                totalText.text = response.stats.total.ToString();
                avgScoreText.text = response.stats.avgScore.ToString();
            }
            else
            {
                totalText.text = "No stats data found";
            }
        }
        else
        {
            Debug.LogError("Error: " + www.error);
            totalText.text = "error loading stats";
        }
    }
}
