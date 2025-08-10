using UnityEngine;
using UnityEngine.SceneManagement;

public class movePlayer : MonoBehaviour
{
    float horizontalInput;
    public float moveSpeed = 5f;
    bool isFacingRight = false;
    public float jumpPower = 8f;
    bool isGrounded = false;

    Rigidbody2D rb;
    Animator animator;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.15f;
    public LayerMask groundLayer;

    [Header("Choice Colliders")]
    public Collider2D truthCollider;
    public Collider2D lieCollider;

    private Collider2D playerCollider;
    private bool hasChosen = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        FlipSprite();

        // Jump input
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
            isGrounded = false;
            animator.SetBool("isJumping", true);
        }

        // Check if grounded using OverlapCircle
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (isGrounded)
        {
            animator.SetBool("isJumping", false);
        }

        // Check for overlap with truth/lie colliders only once
        if (!hasChosen)
        {
            if (truthCollider != null && playerCollider.bounds.Intersects(truthCollider.bounds))
            {
                hasChosen = true;
                Debug.Log("Player chose TRUTH!");

                // Set GameManager values for truth choice
                GameManager.Instance.previousScene = SceneManager.GetActiveScene().name;
                GameManager.Instance.currentChoice = ChoiceType.Truth;
                GameManager.Instance.selectedChoiceText = truthCollider.GetComponentInChildren<UnityEngine.UI.Text>()?.text ?? "Tell the truth";

                SceneManager.LoadScene("ConsequenceScene");
            }
            else if (lieCollider != null && playerCollider.bounds.Intersects(lieCollider.bounds))
            {
                hasChosen = true;
                Debug.Log("Player chose LIE!");

                // Set GameManager values for lie choice
                GameManager.Instance.previousScene = SceneManager.GetActiveScene().name;
                GameManager.Instance.currentChoice = ChoiceType.Lie;
                GameManager.Instance.selectedChoiceText = lieCollider.GetComponentInChildren<UnityEngine.UI.Text>()?.text ?? "Make up an excuse";

                SceneManager.LoadScene("ConsequenceScene");
            }
        }
    }

    // Trigger when player sprite collides with choice collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasChosen) return;

        if (other == truthCollider)
        {
            Debug.Log("Triggered TRUTH collider!");
            hasChosen = true;
            GameManager.Instance.previousScene = SceneManager.GetActiveScene().name;
            GameManager.Instance.currentChoice = ChoiceType.Truth;
            GameManager.Instance.selectedChoiceText = truthCollider.GetComponentInChildren<UnityEngine.UI.Text>()?.text ?? "Tell the truth";
            SceneManager.LoadScene("ConsequenceScene");
        }
        else if (other == lieCollider)
        {
            Debug.Log("Triggered LIE collider!");
            hasChosen = true;
            GameManager.Instance.previousScene = SceneManager.GetActiveScene().name;
            GameManager.Instance.currentChoice = ChoiceType.Lie;
            GameManager.Instance.selectedChoiceText = lieCollider.GetComponentInChildren<UnityEngine.UI.Text>()?.text ?? "Make up an excuse";
            SceneManager.LoadScene("ConsequenceScene");
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);

        animator.SetFloat("xVelocity", Mathf.Abs(rb.linearVelocity.x));
        animator.SetFloat("yVelocity", rb.linearVelocity.y);
    }

    void FlipSprite()
    {
        if (isFacingRight && horizontalInput < 0f || !isFacingRight && horizontalInput > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
