using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Linq;

public class leaderboardManager : MonoBehaviour, IPointerClickHandler
{
    public string leaderboardURL = "http://localhost/hackathon/leaderboard.php";
    public Text emailText;
    public Text scoreText;
    public InputField searchInput;
    private List<Leader> currentLeaders;
    private List<Leader> allLeaders;


    [System.Serializable]
    public class Leader
    {
        public string userID;
        public string email;
        public string moralityScore;
        public string points;
    }

    [System.Serializable]
    public class LeaderList
    {
        public List<Leader> leaders;
        public Stats stats;
    }

    [System.Serializable]
    public class Stats
    {
        public int total;
        public float avgScore;
    }

    void Start()
    {
        StartCoroutine(LoadLeaderboard());
    }

    public void OnSearch()
    {
        string query = searchInput.text.Trim().ToLower();

        if (string.IsNullOrEmpty(query))
        {
            DisplayLeaderboard(allLeaders);
            return;
        }

        List<Leader> filtered = allLeaders
            .Where(leader =>
                (!string.IsNullOrEmpty(leader.email) && leader.email.ToLower().Contains(query)) ||
                (!string.IsNullOrEmpty(leader.moralityScore) && leader.moralityScore.ToLower().Contains(query))
            )
            .ToList();

        DisplayLeaderboard(filtered);
    }

    IEnumerator LoadLeaderboard()
    {
        UnityWebRequest www = UnityWebRequest.Get(leaderboardURL);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string json = www.downloadHandler.text;
            Debug.Log("Json from php: " + json);

            LeaderList leaderList = JsonUtility.FromJson<LeaderList>(json);

            allLeaders = leaderList.leaders;
            DisplayLeaderboard(allLeaders);
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
            scoreText.text += leader.moralityScore + "\n";
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
            PlayerPrefs.SetString("SelectedPlayerUserID", selectedLeader.userID);
            PlayerPrefs.SetString("SelectedPlayerPoints", selectedLeader.points);
            PlayerPrefs.SetString("SelectedPlayerScore", selectedLeader.moralityScore);

            SceneManager.LoadScene("ad-player");
        }
    }
}
