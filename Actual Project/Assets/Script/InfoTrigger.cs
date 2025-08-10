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
                playerMove movement = collision.GetComponent<playerMove>();
                if (movement != null)
                {
                    movement.enabled = false;
                }

                loader.TriggerInfoDialog(this.gameObject, movement);
            }
        }
    }
}
