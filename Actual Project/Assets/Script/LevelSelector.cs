using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public Button primaryButton;
    public Button secondaryButton;
    public Button uniButton;
    public Button workButton;

    void Start()
    {
        primaryButton.onClick.AddListener(() => LoadLevel("PrimaryLevel"));
        secondaryButton.onClick.AddListener(() => LoadLevel("SecondaryLevel"));
        uniButton.onClick.AddListener(() => LoadLevel("UniLevel"));
        workButton.onClick.AddListener(() => LoadLevel("WorkLevel"));
    }

    void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}