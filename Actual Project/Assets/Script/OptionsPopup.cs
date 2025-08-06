using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PopupSceneManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject popupPanel;
    public Button openPopupButton;
    public Button ReportButton;
    public Button ChatbotButton;
    public Button closeButton; 

    void Start()
    {
        // Ensure popup is hidden at start
        popupPanel.SetActive(false);

        // Assign button listeners
        openPopupButton.onClick.AddListener(OpenPopup);
        ReportButton.onClick.AddListener(() => LoadScene("IncidentReport"));
        ChatbotButton.onClick.AddListener(() => LoadScene("Chatbot"));
        closeButton.onClick.AddListener(() => LoadScene("Start"));
    }

    void OpenPopup()
    {
        popupPanel.SetActive(true);
    }

    void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
