using UnityEngine;

public enum ChoiceType { None, Truth, Lie }

[System.Serializable]
public class ConsequenceData
{
    public string previewText;
    public string previewText1;
    public string previewText2;
    public string resultOption1;
    public string resultOption2;
    public int scoreOption1;
    public int scoreOption2;
    public string nextScene;
    
}


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public ChoiceType currentChoice = ChoiceType.None;
    public string selectedChoiceText = "";           // button text
    public ConsequenceData truthConsequence;
    public ConsequenceData lieConsequence;
    public int moralityScore = 0;
    public string previousScene = "";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
