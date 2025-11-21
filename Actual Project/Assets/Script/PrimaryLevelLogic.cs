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
            scenarioText = "You found a wallet on the ground with money \ninside. What do you do?",
            truthChoiceText = "Return it to lost and found",
            lieChoiceText = "Keep it for yourself",

            truthConsequence = new ConsequenceData
            {
                previewText = "Potential Consequences:\n- Owner is grateful\nOR\n- No one claims it",
                previewText1 = "Owner is grateful and rewards you",
                previewText2 = "No one claims it, you feel good anyway",
                resultOption1 = "The owner is extremely grateful and rewards you for your honesty.",
                resultOption2 = "No one claims the wallet, but you feel good for \ndoing the right thing.",
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
            scenarioText = "You accidentally broke school equipment while playing in \nthe classroom. What do you do?",
            truthChoiceText = "Confess immediately",
            lieChoiceText = "Don't say anything",

            truthConsequence = new ConsequenceData
            {
                previewText = "Potential Consequences:\n- Get praised\nOR\n- Replace it",
                previewText1 = "Your teacher praise your honesty",
                previewText2 = "You have to replace it",
                resultOption1 = "Your teacher is mad but praise you for taking responsibility.",
                resultOption2 = "Your teacher forgive you but scold you for breaking it.",
                scoreOption1 = 10,
                scoreOption2 = 5,
                nextScene = "PrimaryEvidence2"
            },

            lieConsequence = new ConsequenceData
            {
                previewText = "Potential Consequences:\n- Others get blamed\nOR\n- Get caught lying",
                previewText1 = "Someone else gets blamed",
                previewText2 = "You get caught lying",
                resultOption1 = "You avoid responsibility and gets other people punished.",
                resultOption2 = "Another student told the teacher you broke it and you are punished.",
                scoreOption1 = -15,
                scoreOption2 = -10,
                nextScene = "PrimaryEvidence2"
            }
        };

        ScenarioData scenario4 = new ScenarioData
        {
            scenarioText = "You found a toy that you wanted at a playground.",
            truthChoiceText = "Hand it to lost and found",
            lieChoiceText = "Keep it for yourself",

            truthConsequence = new ConsequenceData
            {
                previewText = "Potential Consequences:\n- Make someone happy\nOR\n- Get praised",
                previewText1 = "Owner get their toy back",
                previewText2 = "You get praised for doing the right thing",
                resultOption1 = "Owner get their toy and they are happy.",
                resultOption2 = "Your teacher appreciate your honesty.",
                scoreOption1 = 10,
                scoreOption2 = 15,
                nextScene = "transition"
            },

            lieConsequence = new ConsequenceData
            {
                previewText = "Potential Consequences:\n- Get to keep it\nOR\n- Owner saw you take it",
                previewText1 = "You get to keep the toy",
                previewText2 = "Owner say you take it",
                resultOption1 = "You kept someone's toy for yourself.",
                resultOption2 = "The toy owner confronts you and ask for it back.",
                scoreOption1 = -10,
                scoreOption2 = -15,
                nextScene = "transition"
            }
        };

        scenarios.Add(scenario1);
        scenarios.Add(scenario2);
        scenarios.Add(scenario3);
        scenarios.Add(scenario4);
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
        }
        else
        {
            // All scenarios completed
            allScenariosCompleted = true;
            PlayerPrefs.DeleteKey("PrimaryLevelScenarioIndex");
            SceneManager.LoadScene("transition");
        }
    }

    public void LoadNextScenario()
    {
        currentScenarioIndex++;
        LoadScenario(currentScenarioIndex);
    }
    public void CompleteCurrentScenario()
    {
        currentScenarioIndex++;
        PlayerPrefs.SetInt("PrimaryLevelScenarioIndex", currentScenarioIndex);
        PlayerPrefs.Save();
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