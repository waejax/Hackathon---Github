using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ScenarioData
{
    public string scenarioText;
    public string truthChoiceText;
    public string lieChoiceText;
    public ConsequenceData truthConsequence;
    public ConsequenceData lieConsequence;
}

public class PrimaryLevelLogic : MonoBehaviour
{
    [Header("Scenario Configuration")]
    public List<ScenarioData> scenarios = new List<ScenarioData>();
    public int currentScenarioIndex = 0;

    [Header("Choice Colliders (UI or World Objects)")]
    public Collider2D truthCollider;
    public Collider2D lieCollider;
    public Text truthChoiceText;
    public Text lieChoiceText;
    
    [Header("UI Elements")]
    public Text scenarioText;
    public Button ChatbotButton;

    [Header("Animation Settings")]
    public float pulseSpeed = 2f;
    public float pulseAmount = 0.05f;

    private Vector3 truthOriginalScale;
    private Vector3 lieOriginalScale;
    private int totalScenarios = 0;
    private bool allScenariosCompleted = false;

    // 🔹 Your PHP endpoint
    private string updateLastSceneURL = "http://localhost/hackathon/update_last_scene.php";

    void Start()
    {
        StartCoroutine(UpdateLastSceneInDB("PrimaryLevel"));

        truthOriginalScale = truthCollider.transform.localScale;
        lieOriginalScale = lieCollider.transform.localScale;

        InitializeDefaultScenarios();

        totalScenarios = scenarios.Count;

        currentScenarioIndex = PlayerPrefs.GetInt("PrimaryLevelScenarioIndex", 0);
        LoadScenario(currentScenarioIndex);

        ChatbotButton.onClick.AddListener(() =>
        {
            GameManager.Instance.chatbotReturnScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene("Chatbot");
        });
    }

    void InitializeDefaultScenarios()
    {
        ScenarioData scenario1 = new ScenarioData
        {
            scenarioText = "You came into class and noticed that everyone was\nalready seated. The teacher ask why you are late.",
            truthChoiceText = "Tell the truth",
            lieChoiceText = "Make up an excuse",

            truthConsequence = new ConsequenceData
            {
                previewText = "Potential Consequences:\n- You might be praised\nOR\n- Still get a warning",
                previewText1 = "You might be praised",
                previewText2 = "You get a warning",
                resultOption1 = "Your teacher praises your honesty and gives you a light warning.",
                resultOption2 = "Your teacher notes your honesty and kindly reminds you to be early next time.",
                scoreOption1 = 10,
                scoreOption2 = 5,
                //nextScene = "transition" // for when they collect the keris
                nextScene = "PrimaryEvidence"
            },

            lieConsequence = new ConsequenceData
            {
                previewText = "Potential Consequences:\n- You might get away with it\nOR\n- Get caught and punished",
                previewText1 = "You might get away with it",
                previewText2 = "You might get caught and punished",
                resultOption1 = "You lied and your teacher didn't notice. You get away with it.",
                resultOption2 = "The teacher checks the attendance log and catches your lie. You get detention.",
                scoreOption1 = -5,
                scoreOption2 = -10,
                //nextScene = "transition"
                nextScene = "PrimaryEvidence"
            }
        };

        ScenarioData scenario2 = new ScenarioData
        {
            scenarioText = "You found a wallet on the ground with money inside.\nWhat do you do?",
            truthChoiceText = "Return it to lost and found",
            lieChoiceText = "Keep it for yourself",

            truthConsequence = new ConsequenceData
            {
                previewText = "Potential Consequences:\n- Owner is grateful\nOR\n- No one claims it",
                previewText1 = "Owner is grateful and rewards you",
                previewText2 = "No one claims it, you feel good anyway",
                resultOption1 = "The owner is extremely grateful and rewards you for your honesty.",
                resultOption2 = "No one claims the wallet, but you feel good about doing the right thing.",
                scoreOption1 = 15,
                scoreOption2 = 10,
                // nextScene = "transition"
                nextScene = "PrimaryEvidence1"
            },

            lieConsequence = new ConsequenceData
            {
                previewText = "Potential Consequences:\n- Get away with it\nOR\n- Get caught on camera",
                previewText1 = "No one finds out",
                previewText2 = "Security camera catches you",
                resultOption1 = "You keep the money and no one finds out.",
                resultOption2 = "Security footage shows you taking the wallet. You face consequences.",
                scoreOption1 = -10,
                scoreOption2 = -20,
                // nextScene = "transition"
                nextScene = "PrimaryEvidence1"
            }
        };

        ScenarioData scenario3 = new ScenarioData
        {
            scenarioText = "You forgot to do your homework and the teacher is\ncollecting it now. What do you do?",
            truthChoiceText = "Admit you forgot",
            lieChoiceText = "Say you left it at home",

            truthConsequence = new ConsequenceData
            {
                previewText = "Potential Consequences:\n- Get extension\nOR\n- Get zero marks",
                previewText1 = "Teacher gives you extension",
                previewText2 = "You get zero but chance to improve",
                resultOption1 = "Your teacher appreciates your honesty and gives you an extra day.",
                resultOption2 = "You get zero but teacher notes your honesty for future assignments.",
                scoreOption1 = 8,
                scoreOption2 = 3,
                nextScene = "PrimaryEvidence2"
            },

            lieConsequence = new ConsequenceData
            {
                previewText = "Potential Consequences:\n- Get away with it\nOR\n- Get caught lying",
                previewText1 = "Teacher believes you",
                previewText2 = "Teacher checks with parents",
                resultOption1 = "The teacher believes you and gives you until tomorrow.",
                resultOption2 = "The teacher calls your parents and finds out the truth. Double punishment.",
                scoreOption1 = -8,
                scoreOption2 = -15,
                nextScene = "PrimaryEvidence2"
            }
        };

        scenarios.Add(scenario1);
        scenarios.Add(scenario2);
        scenarios.Add(scenario3);
    }

    void LoadScenario(int index)
    {
        if (index >= 0 && index < scenarios.Count)
        {
            ScenarioData currentScenario = scenarios[index];
            
            // Update scenario text with typing effect
            StartCoroutine(TypeSentence(currentScenario.scenarioText));
            
            // Update choice texts
            if (truthChoiceText != null)
                truthChoiceText.text = currentScenario.truthChoiceText;
            if (lieChoiceText != null)
                lieChoiceText.text = currentScenario.lieChoiceText;
            
            GameManager.Instance.truthConsequence = currentScenario.truthConsequence;
            GameManager.Instance.lieConsequence = currentScenario.lieConsequence;

            // Save current progress
            PlayerPrefs.SetInt("PrimaryLevelScenarioIndex", index);
            
            // 🔹 ADD THIS ONE LINE:
            PlayerPrefs.SetInt("PrimaryLevelScenarioIndex", index + 1); // Save NEXT scenario
        }
        else
        {
            // All scenarios completed
            allScenariosCompleted = true;
            PlayerPrefs.DeleteKey("PrimaryLevelScenarioIndex");
            SceneManager.LoadScene("PrimaryLevelEvidence");
        }
    }

    public void LoadNextScenario()
    {
        currentScenarioIndex++;
        LoadScenario(currentScenarioIndex);
    }

    void Update()
    {
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

    IEnumerator TypeSentence(string sentence)
    {
        scenarioText.text = "";
        foreach (char letter in sentence)
        {
            scenarioText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
