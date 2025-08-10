using UnityEngine;

public class EvidenceTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Evidence evidenceManager = FindObjectOfType<Evidence>();
            if (evidenceManager != null)
            {
                evidenceManager.CollectEvidence(this.gameObject);
            }
        }
    }
}
