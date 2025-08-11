using UnityEngine;
using UnityEngine.UI;
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
    public Collider2D truthCollider; // Also used as backCollider in ConsequenceScene
    public Collider2D lieCollider;   // Also used as proceedCollider in ConsequenceScene

    public Text truthText;
    public Text lieText;

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

        if (!hasChosen)
        {
            string currentScene = SceneManager.GetActiveScene().name;

            if (truthCollider != null && playerCollider.bounds.Intersects(truthCollider.bounds))
            {
                hasChosen = true;

                if (currentScene == "ConsequenceScene")
                {
                    // truthCollider acts as backCollider: load previous scene
                    if (!string.IsNullOrEmpty(GameManager.Instance.previousScene))
                    {
                        SceneManager.LoadScene(GameManager.Instance.previousScene);
                    }
                }
                else
                {
                    Debug.Log("Player chose TRUTH!");

                    GameManager.Instance.previousScene = currentScene;
                    GameManager.Instance.currentChoice = ChoiceType.Truth;
                    GameManager.Instance.selectedChoiceText = truthText != null ? truthText.text : "Tell the truth";

                    SceneManager.LoadScene("ConsequenceScene");
                }
            }
            else if (lieCollider != null && playerCollider.bounds.Intersects(lieCollider.bounds))
            {
                hasChosen = true;

                if (currentScene == "ConsequenceScene")
                {
                    // lieCollider acts as proceedCollider: trigger popup in ConsequencesLogic
                    var logic = FindObjectOfType<ConsequencesLogic>();
                    if (logic != null)
                    {
                        logic.ShowFinalConsequence();
                    }
                }
                else
                {
                    Debug.Log("Player chose LIE!");

                    GameManager.Instance.previousScene = currentScene;
                    GameManager.Instance.currentChoice = ChoiceType.Lie;
                    GameManager.Instance.selectedChoiceText = lieText != null ? lieText.text : "Make up an excuse";

                    SceneManager.LoadScene("ConsequenceScene");
                }
            }
        }
    }

    // Trigger when player sprite collides with choice collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasChosen) return;

        string currentScene = SceneManager.GetActiveScene().name;

        if (other == truthCollider)
        {
            hasChosen = true;

            if (currentScene == "ConsequenceScene")
            {
                if (!string.IsNullOrEmpty(GameManager.Instance.previousScene))
                {
                    SceneManager.LoadScene(GameManager.Instance.previousScene);
                }
            }
            else
            {
                Debug.Log("Triggered TRUTH collider!");
                GameManager.Instance.previousScene = currentScene;
                GameManager.Instance.currentChoice = ChoiceType.Truth;
                GameManager.Instance.selectedChoiceText = truthText != null ? truthText.text : "Tell the truth";
                SceneManager.LoadScene("ConsequenceScene");
            }
        }
        else if (other == lieCollider)
        {
            hasChosen = true;

            if (currentScene == "ConsequenceScene")
            {
                var logic = FindObjectOfType<ConsequencesLogic>();
                if (logic != null)
                {
                    logic.ShowFinalConsequence();
                }
            }
            else
            {
                Debug.Log("Triggered LIE collider!");
                GameManager.Instance.previousScene = currentScene;
                GameManager.Instance.currentChoice = ChoiceType.Lie;
                GameManager.Instance.selectedChoiceText = lieText != null ? lieText.text : "Make up an excuse";

                SceneManager.LoadScene("ConsequenceScene");
            }
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
