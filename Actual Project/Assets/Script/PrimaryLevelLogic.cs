using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PrimaryLevelLogic : MonoBehaviour
{
    [Header("Choice Colliders (UI or World Objects)")]
    public Collider2D truthCollider;
    public Collider2D lieCollider;

    [Header("UI Elements")]
    public Text scenarioText;
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
        StartCoroutine(TypeSentence("You came to school late. The teacher asks why."));

        // Set consequences for truth
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

        // Set consequences for lie
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
