using UnityEngine;
using UnityEngine.SceneManagement;

public class doorTrigger : MonoBehaviour
{
    
    [Header("Scene Transition")]
    public string nextSceneName = "PrimaryLevel";
    
    [Header("Requirements")]
    public bool requireItem = false;
    public string requiredItemKey = "KerisCollected";
    public int requiredItemAmount = 1;
    
    [Header("Scenario Progress")]
    public bool completeScenario = true;
    public string scenarioKey = "PrimaryLevelScenarioIndex";

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Check if item is required
            if (requireItem)
            {
                int hasItem = PlayerPrefs.GetInt(requiredItemKey, 0);
                if (hasItem < requiredItemAmount)
                {
                    Debug.Log($"Need to collect {requiredItemKey} first!");
                    return;
                }
            }
            
            // Mark scenario complete
            if (completeScenario && !string.IsNullOrEmpty(scenarioKey))
            {
                MarkScenarioComplete();
            }
            
            // Load next scene
            SceneManager.LoadScene(nextSceneName);
        }
    }

    void MarkScenarioComplete()
    {
        // Try to find PrimaryLevelLogic in the scene
        PrimaryLevelLogic logic = FindObjectOfType<PrimaryLevelLogic>();
        
        if (logic != null)
        {
            // Use the logic's method (which handles guest/registered players)
            logic.CompleteCurrentScenario();
            return;
        }
        else
        {
            // Fallback: Direct PlayerPrefs update
            bool isRegistered = IsPlayerRegistered();
            string actualKey = isRegistered ? scenarioKey : "Guest_" + scenarioKey;
            
            int currentIndex = PlayerPrefs.GetInt(actualKey, 0);
            PlayerPrefs.SetInt(actualKey, currentIndex + 1);
            PlayerPrefs.Save();
            
            Debug.Log($"Marked scenario complete. Key: {actualKey}, New index: {currentIndex + 1}");
        }
    }

    bool IsPlayerRegistered()
    {
        return GameManager.Instance != null && GameManager.Instance.userID > 0;
    }
}

// using UnityEngine;
// using UnityEngine.SceneManagement;
// using System.Collections.Generic;

// public class doorTrigger : MonoBehaviour
// {

//     public string nextSceneName;

//     public static bool hasShard = false;
//     public static int infoIconCount = 0;

//     public void OnTriggerEnter2D(Collider2D collision)
//     {
//         if (collision.gameObject.name.Contains("Keris"))
//         {
//             hasShard = true;
//             Destroy(collision.gameObject);
//             return;
//         }

//         if (collision.gameObject.name.StartsWith("Info_"))
//         {
//             infoIconCount++;
//             Destroy(collision.gameObject);
//             return;
//         }

//         if (collision.CompareTag("Player"))
//         {
//             string currentScene = SceneManager.GetActiveScene().name;

//             if (currentScene.ToLower().Contains("evidence"))
//             {
//                 if (hasShard && infoIconCount >= 2)
//                 {
//                     SceneManager.LoadScene(nextSceneName);
//                 }
//                 else
//                 {
//                     showRequirementDialog();
//                 }
//             }
//             else
//             {
//                 SceneManager.LoadScene(nextSceneName);
//             }
//         }
//     }

//     private void showRequirementDialog()
//     {
//         List<string> message = new List<string>
//         {
//             "You need to collect the shard and at least 2 info icons to proceed."
//         };

//         DialogueManager.Instance.StartCoroutine(
//             DialogueManager.Instance.ShowDialog(
//                 message,
//                 0,
//                 message.Count,
//                 false,
//                 null
//             )
//         );
//     }
// }
