using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConsequencesLogic : MonoBehaviour
{
    public Text choiceMadeText;
    public Text firstPreviewText;
    public Text secPreviewText;
    public Text finalResultText;
    public Button yesButton;
    public Button noButton;
    public Button nextButton;
    public Slider moralitySlider;
    public Text moralityValueText;
    public Button ChatbotButton;

    private bool resultShown = false;
    private ConsequenceData currentData;

    void Start()
    {
        finalResultText.text = "";
        nextButton.gameObject.SetActive(false);
        UpdateMoralityUI();

        // Use data based on choice
        if (GameManager.Instance.currentChoice == ChoiceType.Truth)
        {
            currentData = GameManager.Instance.truthConsequence;
        }
        else
        {
            currentData = GameManager.Instance.lieConsequence;
        }

        choiceMadeText.text = "You chose: " + GameManager.Instance.selectedChoiceText;
        firstPreviewText.text = currentData.previewText1;
        secPreviewText.text = currentData.previewText2;


        yesButton.onClick.AddListener(ShowFinalConsequence);

        // return to previous level
        noButton.onClick.AddListener(() =>
        {
            if (!string.IsNullOrEmpty(GameManager.Instance.previousScene))
            {
                SceneManager.LoadScene(GameManager.Instance.previousScene);
            }
            else
            {
                Debug.LogWarning("No previous scene set!");
            }
        });

        nextButton.interactable = true;
        nextButton.onClick.AddListener(() => SceneManager.LoadScene(currentData.nextScene));
        
        ChatbotButton.onClick.AddListener(() =>
        {
            GameManager.Instance.chatbotReturnScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene("Chatbot");
        });
    }

    void ShowFinalConsequence()
    {
        if (resultShown) return;
        resultShown = true;

        yesButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);

        int rng = Random.Range(0, 100);

        if (rng < 50)
        {
            finalResultText.text = currentData.resultOption1;
            GameManager.Instance.moralityScore += currentData.scoreOption1;
        }
        else
        {
            finalResultText.text = currentData.resultOption2;
            GameManager.Instance.moralityScore += currentData.scoreOption2;
        }

        UpdateMoralityUI();
        nextButton.gameObject.SetActive(true);
    }

    void UpdateMoralityUI()
    {
        if (moralitySlider != null)
        {
            moralitySlider.value = GameManager.Instance.moralityScore;
        }

        if (moralityValueText != null)
        {
            moralityValueText.text = "Morality: " + GameManager.Instance.moralityScore;
        }
    }
}
