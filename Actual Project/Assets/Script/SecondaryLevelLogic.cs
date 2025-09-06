using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections;

public class SecondaryLevelLogic : MonoBehaviour
{
    [Header("Choice Colliders (UI or World Objects)")]
    public Collider2D truthCollider;
    public Collider2D lieCollider;

    [Header("UI Elements")]
    public Text scenarioText;
    public Text truthChoiceText;
    public Text lieChoiceText;    
    public Button ChatbotButton;

    [Header("Animation Settings")]
    public float pulseSpeed = 2f;      // Pulsing speed
    public float pulseAmount = 0.05f;  // Pulsing size change

    private Vector3 truthOriginalScale;
    private Vector3 lieOriginalScale;

    // 🔹 Your PHP endpoint
    private string updateLastSceneURL = "http://localhost/hackathon/update_last_scene.php";

    void Start()
    {
        StartCoroutine(UpdateLastSceneInDB("SecondaryLevel"));

        truthOriginalScale = truthCollider.transform.localScale;
        lieOriginalScale = lieCollider.transform.localScale;

        // Typewriter effect for scenario
        StartCoroutine(TypeSentence("You failed your exam by 2 marks. Your parent is the teacher. What do you do?"));

	truthChoiceText.text = "Retake the exam";
	lieChoiceText.text = "Ask for a fake passing mark";

        // Set consequences for truth
        GameManager.Instance.truthConsequence = new ConsequenceData
        {
            previewText = "Potential Consequences:\n- You may be admired for fairness\nOR\n- Retake the test later",
            previewText1 = "You may be admired for fairness",
            previewText2 = "You may need to retake the test later",
            resultOption1 = "You retake the exam next year, but you're respected for fairness.",
            resultOption2 = "You retake the exam quietly without help, and no one notices.",
            scoreOption1 = 10,
            scoreOption2 = 5,
            nextScene = "transition2"
        };

        // Set consequences for lie
        GameManager.Instance.lieConsequence = new ConsequenceData
        {
            previewText = "Potential Consequences:\n- You might pass\nOR\n- Get caught",
            previewText1 = "You might pass",
            previewText2 = "You might get caught",
            resultOption1 = "The cheating is discovered. Your parent gets in trouble.",
            resultOption2 = "You pass quietly, but it weighs on your conscience.",
            scoreOption1 = -10,
            scoreOption2 = -5,
            nextScene = "transition2"
        };

        // Keep only Chatbot button as clickable
        ChatbotButton.onClick.AddListener(() =>
        {
            GameManager.Instance.chatbotReturnScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene("Chatbot");
        });
    }

    void Update()
    {
        // Pulse animation for the choice objects
        float pulse = 1 + Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
        truthCollider.transform.localScale = truthOriginalScale * pulse;
        lieCollider.transform.localScale = lieOriginalScale * pulse;
    }

    IEnumerator UpdateLastSceneInDB(string sceneName)
    {
        WWWForm form = new WWWForm();
        form.AddField("userID", GameManager.Instance.userID);
        form.AddField("lastScene", sceneName);

        UnityWebRequest www = UnityWebRequest.Post(updateLastSceneURL, form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Last scene updated to: " + sceneName);
        }
        else
        {
            Debug.LogError("Failed to update last scene: " + www.error);
        }
    }

    // Typewriter effect
    System.Collections.IEnumerator TypeSentence(string sentence)
    {
        scenarioText.text = "";
        foreach (char letter in sentence)
        {
            scenarioText.text += letter;
            yield return new WaitForSeconds(0.05f); // Typing speed
        }
    }
}
