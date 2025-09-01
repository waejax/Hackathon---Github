using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections;

public class test1 : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject userMessagePrefab;
    public GameObject botMessagePrefab;

    [Header("References")]
    public Transform contentTransform; // Content under ScrollView
    public TMP_InputField userInput;
    public ScrollRect scrollRect;
    public Button sendButton;

    void Start()
    {
        sendButton.onClick.AddListener(OnSendButtonClicked);

        // TMP_InputField settings
        userInput.lineType = TMP_InputField.LineType.MultiLineNewline;
    }

    public void OnSendButtonClicked()
    {
        string message = userInput.text.Trim();

        if (!string.IsNullOrEmpty(message))
        {
            CreateMessage(message, true);
            userInput.text = "";

            // Simulate bot response
            StartCoroutine(RespondWithBot(message));
        }
    }

    public void CreateMessage(string message, bool isUser)
    {
        GameObject prefab = isUser ? userMessagePrefab : botMessagePrefab;
        GameObject newMessage = Instantiate(prefab, contentTransform);

        TMP_Text messageTMP = newMessage.GetComponentInChildren<TMP_Text>();
        messageTMP.text = message;

        // Force layout rebuild
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentTransform.GetComponent<RectTransform>());

        // Scroll to bottom
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
    }

    IEnumerator RespondWithBot(string userInput)
    {
        yield return new WaitForSeconds(1f);
        string response = "Bot: I heard you say → " + userInput;
        CreateMessage(response, false);
    }
}
