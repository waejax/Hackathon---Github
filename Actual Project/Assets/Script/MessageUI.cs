using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MessageUI : MonoBehaviour
{
    public TMP_Text messageText;
    public RectTransform leftSpacer;
    public RectTransform rightSpacer;
    public LayoutElement bubbleLayout;

    public void Setup(string text, bool isUser, float maxWidth = 400f, float minHeight = 40f)
    {
        messageText.text = text;

        // Width cap (so long text wraps)
        float prefWidth = messageText.preferredWidth + 24f;
        bubbleLayout.preferredWidth = Mathf.Min(prefWidth, maxWidth);
        bubbleLayout.minHeight = minHeight;

        if (isUser)
        {
            // push right
            leftSpacer.gameObject.SetActive(true);
            rightSpacer.gameObject.SetActive(false);
        }
        else
        {
            // push left
            leftSpacer.gameObject.SetActive(false);
            rightSpacer.gameObject.SetActive(true);
        }
    }
}
