using UnityEngine;

public class backgroundFollow : MonoBehaviour
{
    public Transform player;
    //public Vector3 offset = Vector3.zero;
    public float parallaxFactor = 1f;
    private Vector3 lastPlayerPosition;

    public float backgroundWidth = 50f;

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

        float deltaX = player.position.x - lastPlayerPosition.x;

        transform.position -= new Vector3(deltaX * parallaxFactor, 0f, 0f);

        lastPlayerPosition.x = player.position.x;

        // float distanceFromPlayer = player.position.x - transform.position.x;

        // if (Mathf.Abs(distanceFromPlayer) >= backgroundWidth)
        // {
        //     float offset = (distanceFromPlayer > 0 ? 1 : -1) * backgroundWidth;
        //     transform.position += new Vector3(offset, 0f, 0f);
        // }
    }
}
