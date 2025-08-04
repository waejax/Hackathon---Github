using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChatManager : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject userMessage;
    public GameObject botMessage;

    [Header("References")]
    public Transform contentTransform; // Content object under ScrollView
    public InputField userInput; // Regular Unity UI InputField
    public ScrollRect scrollRect; // for auto-scrolling

    public OpenAIChatAPI openAIChatAPI;

    public void OnSendButtonClicked()
    {
        string message = userInput.text.Trim();

        if (!string.IsNullOrEmpty(message))
        {
            CreateMessage(message, true);
            userInput.text = "";

            openAIChatAPI.SendMessageToOpenAI(message);
        }
    }

    public void CreateMessage(string message, bool isUser)
    {
        GameObject prefab = isUser ? userMessage : botMessage;
        GameObject newMessage = Instantiate(prefab, contentTransform);

        TMP_Text messageTMP = newMessage.GetComponentInChildren<TMP_Text>();
        messageTMP.text = message;

        // Force UI to update and scroll to bottom
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
    }

    System.Collections.IEnumerator RespondWithBot(string userInput)
    {
        yield return new WaitForSeconds(1f); // Simulate delay

        string mockResponse = "This is a fake AI reply to: " + userInput;

        CreateMessage(mockResponse, isUser: false);
    }
}
