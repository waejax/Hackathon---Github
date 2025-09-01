using UnityEngine;

public class PowerUpWalker : MonoBehaviour
{
    public float moveSpeed = 5f;             // How fast it moves
    public Transform leftBoundary;           // Left edge
    public Transform rightBoundary;          // Right edge

    private bool movingRight = false;

    void Update()
    {
        if (movingRight)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);

            if (transform.position.x >= rightBoundary.position.x)
                movingRight = false;
        }
        else
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

            if (transform.position.x <= leftBoundary.position.x)
                movingRight = true;
        }
    }
}
