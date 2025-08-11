using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Evidence : MonoBehaviour
{

    public int evidenceCount = 0;
    public Text evidenceText;
    public GameObject door;
    private bool doorDestroyed = false;

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

            if (door != null)
            {
                SpriteRenderer sr = door.GetComponent<SpriteRenderer>();
                if (sr != null)
                    sr.enabled = false;

                foreach (Transform child in door.transform)
                {
                    child.gameObject.SetActive(false);
                }
            }
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
}
