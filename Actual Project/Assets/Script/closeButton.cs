using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class closeButton : MonoBehaviour
{
    public Button close;

    void Start()
    {
        // Assign Exit button action
        close.onClick.AddListener(OnCloseButtonClicked);
    }

    void OnCloseButtonClicked()
    {
        if (GameManager.Instance != null && !string.IsNullOrEmpty(GameManager.Instance.chatbotReturnScene))
        {
            SceneManager.LoadScene(GameManager.Instance.chatbotReturnScene);
        }
        else
        {
            Debug.LogWarning("GameManager.Instance or chatbotReturnScene not set. Returning to Start scene.");
            SceneManager.LoadScene("Start");
        }
    }
    
}
