using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Evidence : MonoBehaviour
{

    public int evidenceCount = 0;
    public Text evidenceText;
    public GameObject door;
    private bool doorDestroyed = false;
    public string nextSceneName;
    public Collider2D sceneTrigger;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateEvidenceText();
    }

    // Update is called once per frame
    void Update()
    {

        if (evidenceCount >= 2 && !doorDestroyed)
        {
            doorDestroyed = true;
            Destroy(door);
        }
    }

    public void CollectEvidence(GameObject evidenceObject)
    {
        evidenceCount++;
        UpdateEvidenceText();
        Destroy(evidenceObject);

        if (evidenceCount == 1)
        {
            DialogueLoader dialogueLoader = FindObjectOfType<DialogueLoader>();
            if (dialogueLoader != null)
            {
                dialogueLoader.onEvidenceIncrease();
            }
        }
    }

    private void UpdateEvidenceText()
    {
        evidenceText.text = "Evidence: " + evidenceCount;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (doorDestroyed && collision.CompareTag("Player") && sceneTrigger != null && collision == sceneTrigger)
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
