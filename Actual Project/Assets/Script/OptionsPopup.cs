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
    public Button infoButton;
    public Button contactButton;
    public Button closeButton; 

    void Start()
    {
        // Ensure popup is hidden at start
        popupPanel.SetActive(false);

        // Assign button listeners
        openPopupButton.onClick.AddListener(OpenPopup);
        ReportButton.onClick.AddListener(() => LoadScene("IncidentReport"));
        ChatbotButton.onClick.AddListener(() => LoadScene("Chatbot"));
        infoButton.onClick.AddListener(() => LoadScene("infoCollected"));
        contactButton.onClick.AddListener(() => LoadScene("Contact"));
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
