using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConsequencesLogic : MonoBehaviour
{
    [Header("UI References")]
    public Text choiceMadeText;
    public Text firstPreviewText;
    public Text secPreviewText;
    public GameObject finalResultPopup; // Popup panel
    public Text finalResultText;
    public Text scoreChangeText;        // e.g. "+5" or "-10"
    public Text totalScoreText;         // e.g. "Total Morality: 15"
    public Button nextButton;
    public Button ChatbotButton;

    [Header("Colliders (World or UI)")]
    public Collider2D proceedCollider; // The "Lie" choice collider
    public Collider2D backCollider;    // The "Truth" choice collider

    [Header("Animation Settings")]
    public float pulseSpeed = 2f;      // Pulsing speed
    public float pulseAmount = 0.05f;  // Pulsing size change

    private Vector3 proceedOriginalScale;
    private Vector3 backOriginalScale;

    private bool resultShown = false;
    private ConsequenceData currentData;

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

        // Save original scales for animation
        if (proceedCollider != null)
            proceedOriginalScale = proceedCollider.transform.localScale;
        if (backCollider != null)
            backOriginalScale = backCollider.transform.localScale;
    }

    void Update()
    {
        // Animate pulse on proceed and back colliders
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
    }
}
