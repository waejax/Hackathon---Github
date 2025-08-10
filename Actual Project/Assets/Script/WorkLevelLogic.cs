using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WorkLevelLogic : MonoBehaviour
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

    void Start()
    {
        truthOriginalScale = truthCollider.transform.localScale;
        lieOriginalScale = lieCollider.transform.localScale;

        // Typewriter effect for scenario
        StartCoroutine(TypeSentence("You've been job hunting for months. Your uncle is a director at a big company and offers you a position — without going through an interview."));

	truthChoiceText.text = "Go through the proper application process";
	lieChoiceText.text = "Accept the job through family connection";

        // Set consequences for truth
        GameManager.Instance.truthConsequence = new ConsequenceData
        {
            previewText = "Potential Consequences:\n- You might get rejected\nOR\n- You earn respect",
            previewText1 = "You might not get the job",
            previewText2 = "You gain respect and prove fairness",
            resultOption1 = "You don’t get the job right away, but you gain the respect of others.",
            resultOption2 = "You prove your worth through fairness and eventually succeed.",
            scoreOption1 = 5,
            scoreOption2 = 10,
            nextScene = "Start"
        };

        // Set consequences for lie
        GameManager.Instance.lieConsequence = new ConsequenceData
        {
            previewText = "Potential Consequences:\n- You get the job\nOR\n- Face distrust",
            previewText1 = "You get the job easily",
            previewText2 = "Coworkers question your skills",
            resultOption1 = "You get the job, but coworkers question your abilities.",
            resultOption2 = "You get the job, but you’re excluded from team projects.",
            scoreOption1 = -5,
            scoreOption2 = -10,
            nextScene = "Start"
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
