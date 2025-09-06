using UnityEngine;
using UnityEngine.SceneManagement;

public class SwingRotation : MonoBehaviour
{
    [Header("Pulse Settings")]
    public float pulseAmount = 0.05f;   // scale change (5% by default)
    public float pulseSpeed = 2f;       // how fast it pulses

    private Vector3 initialScale;

    void Start()
    {
        if (SceneManager.GetActiveScene().name != "FinalSummary")
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 45f);
        }
        initialScale = transform.localScale;
    }

    void Update()
    {
        // Calculate pulsing offset
        float scaleOffset = Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
        transform.localScale = initialScale + Vector3.one * scaleOffset;
    }
}
