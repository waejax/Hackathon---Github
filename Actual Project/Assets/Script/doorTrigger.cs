using UnityEngine;
using UnityEngine.SceneManagement;

public class doorTrigger : MonoBehaviour
{
    
    public string nextSceneName;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
