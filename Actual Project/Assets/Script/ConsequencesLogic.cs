using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections;

public class ConsequencesLogic : MonoBehaviour
{
    [Header("UI References")]
    public Text choiceMadeText;
    public Text firstPreviewText;
    public Text secPreviewText;
    public GameObject finalResultPopup; 
    public Text finalResultText;
    public Text scoreChangeText;        
    public Text totalScoreText;         
    public Button nextButton;
    public Button ChatbotButton;

    [Header("Colliders (World or UI)")]
    public Collider2D proceedCollider;
    public Collider2D backCollider;

    [Header("Animation Settings")]
    public float pulseSpeed = 2f;
    public float pulseAmount = 0.05f;

    private Vector3 proceedOriginalScale;
    private Vector3 backOriginalScale;
    private bool resultShown = false;
    private ConsequenceData currentData;

    string getMoralityURL = "http://localhost/hackathon/get_morality.php";
    string updateMoralityURL = "http://localhost/hackathon/update_morality.php";

    void Start()
    {
        if (finalResultPopup != null)
            finalResultPopup.SetActive(false);

        if (GameManager.Instance.currentChoice == ChoiceType.Truth)
            currentData = GameManager.Instance.truthConsequence;
        else
            currentData = GameManager.Instance.lieConsequence;

        choiceMadeText.text = "You choose to: " + GameManager.Instance.selectedChoiceText;
        firstPreviewText.text = currentData.previewText1;
        secPreviewText.text = currentData.previewText2;

        nextButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(currentData.nextScene);
        });

        ChatbotButton.onClick.AddListener(() =>
        {
            GameManager.Instance.chatbotReturnScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene("Chatbot");
        });

        if (proceedCollider != null)
            proceedOriginalScale = proceedCollider.transform.localScale;
        if (backCollider != null)
            backOriginalScale = backCollider.transform.localScale;

        // Load morality score from DB
        StartCoroutine(LoadMoralityScore());
    }

    void Update()
    {
        float pulse = 1 + Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
        if (proceedCollider != null)
            proceedCollider.transform.localScale = proceedOriginalScale * pulse;
        if (backCollider != null)
            backCollider.transform.localScale = backOriginalScale * pulse;
    }

    public void ShowFinalConsequence()
    {
        if (resultShown) return;
        resultShown = true;

        int scoreChange = 0;
        int rng = Random.Range(0, 100);

        if (rng < 50)
        {
            finalResultText.text = currentData.resultOption1;
            scoreChange = currentData.scoreOption1;
        }
        else
        {
            finalResultText.text = currentData.resultOption2;
            scoreChange = currentData.scoreOption2;
        }

        // Update local score
        GameManager.Instance.moralityScore += scoreChange;

        if (scoreChangeText != null)
        {
            scoreChangeText.text = (scoreChange >= 0 ? "+" : "") + scoreChange.ToString();
            scoreChangeText.color = scoreChange >= 0 ? Color.green : Color.red;
        }

        if (totalScoreText != null)
            totalScoreText.text = "" + GameManager.Instance.moralityScore;

        if (finalResultPopup != null)
            finalResultPopup.SetActive(true);

        // Save updated score to DB
        StartCoroutine(UpdateMoralityScore(GameManager.Instance.moralityScore));
    }

    IEnumerator LoadMoralityScore()
    {
        WWWForm form = new WWWForm();
        form.AddField("userID", GameManager.Instance.userID);

        UnityWebRequest www = UnityWebRequest.Post(getMoralityURL, form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            int dbScore;
            if (int.TryParse(www.downloadHandler.text.Trim(), out dbScore))
            {
                GameManager.Instance.moralityScore = dbScore;
                if (totalScoreText != null)
                    totalScoreText.text = "" + GameManager.Instance.moralityScore;
            }
        }
        else
        {
            Debug.LogError("Error loading morality score: " + www.error);
        }
    }

    IEnumerator UpdateMoralityScore(int newScore)
    {
        WWWForm form = new WWWForm();
        form.AddField("userID", GameManager.Instance.userID);
        form.AddField("newScore", newScore);

        UnityWebRequest www = UnityWebRequest.Post(updateMoralityURL, form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error updating morality score: " + www.error);
        }
    }
}
