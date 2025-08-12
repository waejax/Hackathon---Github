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
    public Button mainMenuButton;

    private string homeScene = "Start";

    void Start()
    {
        // Ensure popup is hidden at start
        popupPanel.SetActive(false);

        // Assign button listeners
        openPopupButton.onClick.AddListener(OpenPopup);
        closeButton.onClick.AddListener(ClosePopup);

        ReportButton.onClick.AddListener(() => LoadScene("IncidentReport"));
        ChatbotButton.onClick.AddListener(() => LoadScene("Chatbot"));
        infoButton.onClick.AddListener(() => LoadScene("infoCollected"));
        contactButton.onClick.AddListener(() => LoadScene("Contact"));

        mainMenuButton.onClick.AddListener(() => LoadScene(homeScene));
    }

    public void OpenPopup()
    {
        popupPanel.SetActive(true);
    }

    void ClosePopup() {
        popupPanel.SetActive(false);
    }

    void LoadScene(string sceneName)
    {
        GameManager.Instance.chatbotReturnScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }

    void AssignSceneButton(Button button, string targetScene)
    {
        button.onClick.AddListener(() =>
        {
            string currentScene = SceneManager.GetActiveScene().name;

            if (currentScene == homeScene)
            {
                SceneManager.LoadScene(targetScene);
            }
            else if (currentScene == targetScene)
            {
                SceneManager.LoadScene(homeScene);
            }
        });
    }
}
