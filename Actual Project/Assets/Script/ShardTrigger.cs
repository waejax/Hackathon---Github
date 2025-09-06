using UnityEngine;

public class ShardTrigger : MonoBehaviour
{
    private bool triggered = false;
    public string level;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!triggered && collision.CompareTag("Player"))
        {
            triggered = true;

            // Make the icon disappear
            // Option 1: Disable the entire GameObject
            gameObject.SetActive(false);

            // Option 2: Destroy the icon GameObject
            // Destroy(gameObject);

            DialogueLoader loader = FindObjectOfType<DialogueLoader>();

            if (loader != null)
            {
                playerMove movement = collision.GetComponent<playerMove>();
                if (movement != null)
                {
                    movement.enabled = false;
                }
                if (movement != null)
                {
                    movement.enabled = true;
                }

                loader.TriggerShardDialog(this.gameObject, movement, level);
            }
        }
    }
}