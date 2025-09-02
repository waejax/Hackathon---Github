using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class DeepSeekAPI : MonoBehaviour
{
    [Header("DeepSeek Settings")]
    public string apiKey = "your_deepseek_api_key_here";

    [Header("References")]
    public ChatManager chatManager;

    // The API endpoint for DeepSeek chat completions
    private string endpoint = "https://api.deepseek.com/v1/chat/completions";

    /// <summary>
    /// Public method to send a user message to the DeepSeek API.
    /// It starts the coroutine to handle the web request.
    /// </summary>
    /// <param name="userMessage">The message text from the user input field.</param>
    public void SendMessageToDeepSeek(string userMessage)
    {
        StartCoroutine(SendChatRequest(userMessage));
    }

    /// <summary>
    /// Coroutine to handle the API request and response.
    /// </summary>
    /// <param name="userMessage">The message text to be sent in the request body.</param>
    private IEnumerator SendChatRequest(string userMessage)
    {
        // Create the JSON request body with the system and user messages.
        // The system message defines the chatbot's persona.
        string jsonBody = JsonUtility.ToJson(new ChatRequestBody
        {
            model = "deepseek-chat",
            messages = new Message[] {
                new Message { role = "system", content = "You are an AI chatbot acting as an anti-corruption advisor or Brunei's Anti-Corruption Bureau officer. You provide information and answer questions about corruption by referring to general knowledge, Brunei's corruption laws, and notable corruption cases in Brunei. You may occasionally refer to corruption-related facts or cases from other countries, but only if they help explain or support your point. Always prioritize Brunei’s context and information when responding." },
                new Message { role = "user", content = userMessage }
            }
        });

        // Convert the JSON string to a byte array.
        byte[] postData = System.Text.Encoding.UTF8.GetBytes(jsonBody);

        // Create a UnityWebRequest to send the POST request.
        using (UnityWebRequest request = new UnityWebRequest(endpoint, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(postData);
            request.downloadHandler = new DownloadHandlerBuffer();

            // Set the required headers for the API request.
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + apiKey);

            // Wait for the request to complete.
            yield return request.SendWebRequest();

            // Check if the request was successful.
            if (request.result == UnityWebRequest.Result.Success)
            {
                // Parse the JSON response from the server.
                string jsonResponse = request.downloadHandler.text;
                DeepSeekResponse response = JsonUtility.FromJson<DeepSeekResponse>(jsonResponse);

                // Extract the AI's reply and create a new chat message.
                string aiReply = response.choices[0].message.content.Trim();
                chatManager.CreateMessage(aiReply, false);
            }
            else
            {
                // Handle API errors and display a message in the chat.
                chatManager.CreateMessage("Error: " + request.error + "\n" + request.downloadHandler.text, false);
            }
        }
    }

    // Serializable classes for JSON serialization and deserialization.
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
    public class DeepSeekResponse
    {
        public Choice[] choices;
    }

    [System.Serializable]
    public class Choice
    {
        public Message message;
    }
}
