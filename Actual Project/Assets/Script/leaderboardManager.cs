using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class leaderboardManager : MonoBehaviour, IPointerClickHandler
{
    public string leaderboardURL = "http://localhost/hackathon/leaderboard.php";
    public Text emailText;
    public Text scoreText;
    private List<Leader> currentLeaders;


    [System.Serializable]
    public class Leader
    {
        public string email;
        public string score;
    }

    [System.Serializable]
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

            DisplayLeaderboard(leaderList.leaders);
        }
        else
        {
            Debug.LogError("Error: " + www.error);
            emailText.text = "error loading leaderboard";
        }
    }

    void DisplayLeaderboard(List<Leader> leaders)
    {
        currentLeaders = leaders;
        emailText.text = "";
        scoreText.text = "";

        int rank = 1;
        foreach (Leader leader in leaders)
        {
            emailText.text += rank + ". " + leader.email + "\n";
            scoreText.text += leader.score + "\n";
            rank++;
        }
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        Vector2 localMousePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            emailText.rectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out localMousePos
        );

        float pivotOffsetY = emailText.rectTransform.rect.height * emailText.rectTransform.pivot.y;

        float yFromTop = pivotOffsetY - localMousePos.y;

        float lineHeight = emailText.fontSize * emailText.lineSpacing;
        
        int clickedIndex = Mathf.FloorToInt(yFromTop / lineHeight);

        Debug.Log($"Clicked line: {clickedIndex}");

        if (clickedIndex >= 0 && clickedIndex < currentLeaders.Count)
        {
            Leader selectedLeader = currentLeaders[clickedIndex];
            PlayerPrefs.SetString("SelectedPlayerEmail", selectedLeader.email);
            PlayerPrefs.SetString("SelectedPlayerScore", selectedLeader.score);
            SceneManager.LoadScene("ad-player");
        }
    }
}
