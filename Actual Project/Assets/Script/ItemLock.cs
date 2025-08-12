using UnityEngine;

public class CollectiblePositionLock : MonoBehaviour
{
    private Vector3 initialPosition;

    void Start()
    {
        // Store the collectible's starting position in world space
        initialPosition = transform.position;
    }

    void LateUpdate()
    {
        // Always reset position in case something (like camera scaling) moves it
        transform.position = initialPosition;
    }
}
