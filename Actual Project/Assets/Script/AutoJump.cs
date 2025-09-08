using UnityEngine;

public class AutoJump : MonoBehaviour
{
    public float jumpHeight = 2f;     // how high it jumps
    public float jumpDuration = 0.6f; // time to reach peak and come down
    public float groundPause = 0.3f;  // wait time before next jump
    private float timer = 0f;
    private float groundY;

    void Start()
    {
        groundY = transform.position.y; // starting point = ground
    }

    void Update()
    {
        timer += Time.deltaTime;
        float cycleTime = jumpDuration + groundPause;
        float t = timer % cycleTime;

        float newY = groundY;

        if (t < jumpDuration) // jumping phase
        {
            float progress = t / jumpDuration; // 0 → 1
            // parabola curve (up fast, down faster)
            float arc = 4 * progress * (1 - progress);
            newY = groundY + arc * jumpHeight;
        }
        // else → groundPause, stays at groundY

        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
