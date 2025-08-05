using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public int startLineIndex;
    public int lineCount = 1;

    private bool trigger = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!trigger && collision.CompareTag("Player"))
        {
            trigger = true;
            FindObjectOfType<DialogueLoader>().TriggerNextDialog(startLineIndex, lineCount);
        }
    }
}
