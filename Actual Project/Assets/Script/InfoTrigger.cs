using UnityEngine;

public class InfoTrigger : MonoBehaviour
{
    private bool trigger = false;

    void OTriggerEnter2D(Collider2D collision)
    {
        if (!trigger && collision.CompareTag("Player"))
        {
            trigger = true;
            DialogueLoader loader = FindObjectOfType<DialogueLoader>();
        }
    }
}
