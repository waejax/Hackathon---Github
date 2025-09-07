using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] private float bounceForce = 15f; // how strong the bounce is

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D rb = collision.attachedRigidbody;
        if (rb != null)
        {
            // Reset vertical velocity so bounce is consistent
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            // Apply upward force
            rb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
        }
    }
}
