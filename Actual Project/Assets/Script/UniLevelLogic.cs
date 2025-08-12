using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections;

public class UniLevelLogic : MonoBehaviour
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
        StartCoroutine(UpdateLastSceneInDB("UniLevel"));

        truthOriginalScale = truthCollider.transform.localScale;
        lieOriginalScale = lieCollider.transform.localScale;

        // Typewriter effect for scenario
        StartCoroutine(TypeSentence("You live near campus, but only students who live far away are eligible for a travel allowance. You’re thinking of using your cousin’s address to get the money."));

	truthChoiceText.text = "Use your real address and skip the allowance";
	lieChoiceText.text = "Use your cousin’s address to claim the allowance";

        // Set consequences for truth
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

        // Set consequences for lie
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
