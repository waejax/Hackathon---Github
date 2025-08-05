using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneByName : MonoBehaviour
{
    public Button loadButton;        // Assign your UI Button in Inspector
    public string sceneToLoadName;   // Type the exact scene name in Inspector

    void Start()
    {
        if (loadButton != null && !string.IsNullOrEmpty(sceneToLoadName))
        {
            loadButton.onClick.AddListener(() => SceneManager.LoadScene(sceneToLoadName));
        }
        else
        {
            Debug.LogWarning("Button or Scene Name not assigned!");
        }
    }
}
