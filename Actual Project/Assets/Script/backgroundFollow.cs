using UnityEngine;

public class backgroundFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = Vector3.zero;
    public float parallaxFactor = 1f;
    private Vector3 lastPlayerPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (player != null)
        {
            lastPlayerPosition = player.position;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (player == null)
            return;

        Vector3 delta = player.position - lastPlayerPosition;

        transform.position -= new Vector3(delta.x * parallaxFactor, delta.y * parallaxFactor, 0f);

        lastPlayerPosition = player.position;
    }
}
