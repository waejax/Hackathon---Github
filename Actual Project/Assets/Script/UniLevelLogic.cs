using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UniLevelLogic : MonoBehaviour
{
    public Button truthButton;
    public Button lieButton;
    public Text scenarioText;
    public Button ChatbotButton;

    void Start()
    {
        scenarioText.text = "You live near campus, but only students who live far away are eligible for a travel allowance. You’re thinking of using your cousin’s address to get the money.";

        truthButton.GetComponentInChildren<Text>().text = "Use your real address and skip the allowance";
        lieButton.GetComponentInChildren<Text>().text = "Use your cousin’s address to claim the allowance";

        GameManager.Instance.truthConsequence = new ConsequenceData
        {
            previewText = "Potential Consequences:\n- You may lose money\nOR\n- Be proud of your honesty",
            previewText1 = "You may lose the allowance",
            previewText2 = "You may feel proud of your honesty",
            resultOption1 = "You don’t get the money, but you feel proud knowing you did the right thing.",
            resultOption2 = "You stay honest and gain your professor’s respect.",
            scoreOption1 = 10,
            scoreOption2 = 5,
            nextScene = "WorkLevel"
        };

        GameManager.Instance.lieConsequence = new ConsequenceData
        {
            previewText = "Potential Consequences:\n- You might get the money\nOR\n- Get caught",
            previewText1 = "You might get the money",
            previewText2 = "You might get caught",
            resultOption1 = "You receive the money, but months later, you’re caught during a verification check.",
            resultOption2 = "You get the money, but someone finds out and you’re suspended.",
            scoreOption1 = -5,
            scoreOption2 = -10,
            nextScene = "WorkLevel"
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
