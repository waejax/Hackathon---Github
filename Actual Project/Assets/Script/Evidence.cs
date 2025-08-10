using UnityEngine;
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
            Destroy(door);
        }
    }

    public void CollectEvidence(GameObject evidenceObject)
    {
        evidenceCount++;
        UpdateEvidenceText();
        Destroy(evidenceObject);
    }

    private void UpdateEvidenceText()
    {
        evidenceText.text = "Evidence: " + evidenceCount;
    }
}
