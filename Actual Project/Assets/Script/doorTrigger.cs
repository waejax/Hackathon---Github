using UnityEngine;
using UnityEngine.SceneManagement;

public class doorTrigger : MonoBehaviour
{
    
    public string nextSceneName;


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            SceneManager.LoadScene(nextSceneName);
        }
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
