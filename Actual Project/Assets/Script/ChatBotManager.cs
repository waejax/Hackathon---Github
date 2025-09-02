using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class ChatBotManager : MonoBehaviour
{
    [Header("UI References")]
    public RectTransform messagesContent;
    public ScrollRect scrollRect;
    public TMP_InputField inputField;
    public Button sendButton;
    public Button closeButton;

    [Header("Prefabs")]
    public GameObject userMessagePrefab;
    public GameObject botMessagePrefab;

    [Header("DeepSeek API")]
    public OpenAIChatAPI openAIChatAPI; // reference to your existing script

    void Start()
    {
        sendButton.onClick.AddListener(OnSend);

        // Assign Exit button action
        closeButton.onClick.AddListener(OnCloseButtonClicked);
    }

    void OnSend()
    {
        string text = inputField.text.Trim();
        if (string.IsNullOrEmpty(text)) return;

        inputField.text = "";

        // Add user message immediately
        AddMessage(text, true);

        // Send message to your OpenAIChatAPI
        openAIChatAPI.SendMessageToOpenAI(text);
    }

    public void AddMessage(string text, bool isUser)
    {
        var prefab = isUser ? userMessagePrefab : botMessagePrefab;
        GameObject go = Instantiate(prefab, messagesContent);
        go.GetComponent<MessageUI>().Setup(text);

        // Force layout to update immediately so the next message stacks properly
        LayoutRebuilder.ForceRebuildLayoutImmediate(go.GetComponent<RectTransform>());
        LayoutRebuilder.ForceRebuildLayoutImmediate(messagesContent);

        // Scroll to bottom
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
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
