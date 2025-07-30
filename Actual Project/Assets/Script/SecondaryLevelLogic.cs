using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SecondaryLevelLogic : MonoBehaviour
{
    public Button truthButton;
    public Button lieButton;
    public Text scenarioText;

    void Start()
    {
        scenarioText.text = "You failed your exam by 2 marks. Your parent is the teacher. Do you ask them to pass you?";

        truthButton.GetComponentInChildren<Text>().text = "Accept the grade and retake the test";
        lieButton.GetComponentInChildren<Text>().text = "Ask for a fake passing mark";

        // Configure consequences for this level
        GameManager.Instance.truthConsequence = new ConsequenceData
        {
            previewText = "Potential Consequences:\n- You may be admired for fairness\nOR\n- Retake the test later",
            previewText1 = "You may be admired for fairness",
            previewText2 = "You may need to retake the test later",
            resultOption1 = "You retake the exam next year, but you're respected for fairness.",
            resultOption2 = "You retake the exam quietly without help, and no one notices.",
            scoreOption1 = 10,
            scoreOption2 = 5,
            nextScene = "UniLevel"
        };

        GameManager.Instance.lieConsequence = new ConsequenceData
        {
            previewText = "Potential Consequences:\n- You might pass\nOR\n- Get caught",
            previewText1 = "You might pass",
            previewText2 = "You might get caught",
            resultOption1 = "The cheating is discovered. Your parent gets in trouble.",
            resultOption2 = "You pass quietly, but it weighs on your conscience.",
            scoreOption1 = -10,
            scoreOption2 = -5,
            nextScene = "UniLevel"
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
    }

}
