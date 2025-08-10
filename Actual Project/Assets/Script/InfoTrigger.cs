using UnityEngine;

public class InfoTrigger : MonoBehaviour
{
    private bool trigger = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!trigger && collision.CompareTag("Player"))
        {
            trigger = true;
            DialogueLoader loader = FindObjectOfType<DialogueLoader>();

            if (loader != null)
            {
                loader.TriggerInfoDialog(this.gameObject);
            }
        }
    }
}
