using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PrimaryLevelLogic : MonoBehaviour
{
    public Button truthButton;
    public Button lieButton;
    public Text scenarioText;

    void Start()
    {
        scenarioText.text = "You came to school late. The teacher asks why.";

        // Change button texts
        truthButton.GetComponentInChildren<Text>().text = "Tell the truth";
        lieButton.GetComponentInChildren<Text>().text = "Make up an excuse";

        // Set consequences for this level
        GameManager.Instance.truthConsequence = new ConsequenceData
        {
            previewText = "Potential Consequences:\n- You might be praised\nOR\n- Still get a warning",
            previewText1 = "You might be praised",
            previewText2 = "You still get a warning",
            resultOption1 = "Your teacher praises your honesty and gives you a light warning.",
            resultOption2 = "Your teacher notes your honesty but still reports it to the principal.",
            scoreOption1 = 10,
            scoreOption2 = 5,
            nextScene = "SecondaryLevel"
        };

        GameManager.Instance.lieConsequence = new ConsequenceData
        {
            previewText = "Potential Consequences:\n- You might get away with it\nOR\n- Get caught and punished",
            previewText1 = "You might get away with it",
            previewText2 = "You might get caught and punished",
            resultOption1 = "You lied and your teacher didn’t notice. You get away with it.",
            resultOption2 = "The teacher checks the attendance log and catches your lie. You get detention.",
            scoreOption1 = -5,
            scoreOption2 = -10,
            nextScene = "SecondaryLevel"
        };

        // Setup button logic
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
