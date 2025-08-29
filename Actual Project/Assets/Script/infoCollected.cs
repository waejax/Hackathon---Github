using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class infoCollected : MonoBehaviour
{
    public Text infoText;
    private DialogueLoader dialogueLoader;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogueLoader = FindObjectOfType<DialogueLoader>();

        DisplayCollectedInfo();
    }

    public void DisplayCollectedInfo()
    {
        List<string> collectedInfo = dialogueLoader != null ? dialogueLoader.getCollectedInfo() : null;

        if (collectedInfo == null || collectedInfo.Count == 0)
        {
            infoText.text = "No info collected";
        }
        else
        {
            string displayText = "";
            foreach (string line in collectedInfo)
            {
                displayText += "- " + line + "\n";
            }

            infoText.text = displayText;
        }
    }
}
