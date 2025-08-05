using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WorkLevelLogic : MonoBehaviour
{
    public Button truthButton;
    public Button lieButton;
    public Text scenarioText;
    public Button ChatbotButton;

    void Start()
    {
        scenarioText.text = "You've been job hunting for months. Your uncle is a director at a big company and offers you a position — without going through an interview.";

        truthButton.GetComponentInChildren<Text>().text = "Go through the proper application process";
        lieButton.GetComponentInChildren<Text>().text = "Accept the job through family connection";

        GameManager.Instance.truthConsequence = new ConsequenceData
        {
            previewText = "Potential Consequences:\n- You might get rejected\nOR\n- You earn respect",
            previewText1 = "You might not get the job",
            previewText2 = "You gain respect and prove fairness",
            resultOption1 = "You don’t get the job right away, but you gain the respect of others.",
            resultOption2 = "You prove your worth through fairness and eventually succeed.",
            scoreOption1 = 5,
            scoreOption2 = 10,
            nextScene = "FinalResult" // replace with your actual final scene
        };

        GameManager.Instance.lieConsequence = new ConsequenceData
        {
            previewText = "Potential Consequences:\n- You get the job\nOR\n- Face distrust",
            previewText1 = "You get the job easily",
            previewText2 = "Coworkers question your skills",
            resultOption1 = "You get the job, but coworkers question your abilities.",
            resultOption2 = "You get the job, but you’re excluded from team projects.",
            scoreOption1 = -5,
            scoreOption2 = -10,
            nextScene = "FinalResult" // replace with your actual final scene
        };

        truthButton.onClick.AddListener(() =>
        {
            GameManager.Instance.previousScene = SceneManager.GetActiveScene().name;
            GameManager.Instance.currentChoice = ChoiceType.Truth;
            GameManager.Instance.selectedChoiceText = truthButton.GetComponentInChildren<Text>().text;
            SceneManager.LoadScene("ConsequenceScene");
        });

        lieButton.onClick.AddListener(() =>
        {
            GameManager.Instance.previousScene = SceneManager.GetActiveScene().name;
            GameManager.Instance.currentChoice = ChoiceType.Lie;
            GameManager.Instance.selectedChoiceText = lieButton.GetComponentInChildren<Text>().text;
            SceneManager.LoadScene("ConsequenceScene");
        });

        ChatbotButton.onClick.AddListener(() =>
        {
            GameManager.Instance.chatbotReturnScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene("Chatbot");
        });
    }
}
