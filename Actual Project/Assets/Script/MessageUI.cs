using TMPro;
using UnityEngine;

public class MessageUI : MonoBehaviour
{
    public TMP_Text messageText;

    public void Setup(string text)
    {
        messageText.text = text;
    }
}
