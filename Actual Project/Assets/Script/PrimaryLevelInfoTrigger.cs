using UnityEngine;

public class PrimaryLevelInfoTrigger : MonoBehaviour
{
    private bool trigger = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!trigger && collision.CompareTag("Player"))
        {
            trigger = true;
            PrimaryLevelDialogueLoader loader = FindObjectOfType<PrimaryLevelDialogueLoader>();

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
