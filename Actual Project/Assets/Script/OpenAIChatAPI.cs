using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class OpenAIChatAPI : MonoBehaviour
{
    [Header("DeepSeek Settings")]
    public string apiKey = "your_deepseek_api_key_here";

    [Header("References")]
    public ChatBotManager chatManager;

    private string endpoint = "https://api.deepseek.com/v1/chat/completions";

    public void SendMessageToOpenAI(string userMessage)
    {
        StartCoroutine(SendChatRequest(userMessage));
    }

    private IEnumerator SendChatRequest(string userMessage)
    {
        string jsonBody = JsonUtility.ToJson(new ChatRequestBody
        {
            model = "deepseek-chat",
            messages = new Message[] {
                new Message { role = "system", content = "You are an AI chatbot acting as an anti-corruption advisor or Brunei's Anti-Corruption Bureau officer. You provide information and answer questions about corruption by referring to general knowledge, Brunei's corruption laws, and notable corruption cases in Brunei. You may occasionally refer to corruption-related facts or cases from other countries, but only if they help explain or support your point. Always prioritize Brunei’s context and information when responding." },
                new Message { role = "user", content = userMessage }
            }
        });

        byte[] postData = System.Text.Encoding.UTF8.GetBytes(jsonBody);

        using (UnityWebRequest request = new UnityWebRequest(endpoint, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(postData);
            request.downloadHandler = new DownloadHandlerBuffer();

            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + apiKey);

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = request.downloadHandler.text;
                OpenAIResponse response = JsonUtility.FromJson<OpenAIResponse>(jsonResponse);
                string aiReply = response.choices[0].message.content.Trim();
                chatManager.AddMessage(aiReply, false);
            }
            else
            {
                chatManager.AddMessage("Error: " + request.error + "\n" + request.downloadHandler.text, false);
            }
        }
    }

    [System.Serializable]
    public class ChatRequestBody
    {
        public string model;
        public Message[] messages;
    }

    [System.Serializable]
    public class Message
    {
        public string role;
        public string content;
    }

    [System.Serializable]
    public class OpenAIResponse
    {
        public Choice[] choices;
    }

    [System.Serializable]
    public class Choice
    {
        public Message message;
    }
}
