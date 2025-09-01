using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ChatBotManager : MonoBehaviour
{
    public RectTransform messagesContent;
    public ScrollRect scrollRect;
    public TMP_InputField inputField;
    public Button sendButton;
    public GameObject messagePrefab;

    void Start()
    {
        sendButton.onClick.AddListener(OnSend);
    }

    void OnSend()
    {
        string text = inputField.text.Trim();
        if (string.IsNullOrEmpty(text)) return;

        AddMessage(text, true);
        inputField.text = "";

        // Simulate bot reply
        StartCoroutine(FakeBotReply(text));
    }

    void AddMessage(string text, bool isUser)
    {
        var go = Instantiate(messagePrefab, messagesContent);
        var ui = go.GetComponent<MessageUI>();
        ui.Setup(text, isUser);

        Canvas.ForceUpdateCanvases();
        StartCoroutine(ScrollToBottom());
    }

    IEnumerator ScrollToBottom()
    {
        yield return null;
        scrollRect.verticalNormalizedPosition = 0f;
    }

    IEnumerator FakeBotReply(string userText)
    {
        yield return new WaitForSeconds(1f);
        AddMessage("Echo: " + userText, false);
    }
}
