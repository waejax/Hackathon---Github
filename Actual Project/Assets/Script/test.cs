using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class test : MonoBehaviour
{
    float horizontalInput;
    public float moveSpeed = 5f;
    bool isFacingRight = true; // Start facing right
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

    public Text truthText;
    public Text lieText;

    private Collider2D playerCollider;
    private bool hasChosen = false;

    [Header("PowerUp Settings")]
    public GameObject powerUpEffectPrefab;
    private Vector3 originalScale;
    private float originalJumpPower;
    private int maxJumps = 2;
    private int jumpsUsed = 0;

    // Slight shrink factor (not half)
    [Range(0.6f, 1.0f)]
    public float smallScaleFactor = 0.8f;

    [Header("Timer Settings")]
    public float countdownTime = 60f;
    private float timer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerCollider = GetComponent<Collider2D>();

        originalScale = transform.localScale;
        originalJumpPower = jumpPower;

        // Start facing right, even if sprite artwork faces left
        isFacingRight = true;
        transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);

        timer = countdownTime;
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        FlipSprite();

        // Timer countdown
        if (!hasChosen)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                AutoRandomChoice();
            }
        }

        // Jump input
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
                isGrounded = false;
                jumpsUsed = 1;
                animator.SetBool("isJumping", true);
            }
            else if (jumpsUsed < maxJumps)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
                jumpsUsed++;
            }
        }

        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (isGrounded)
        {
            animator.SetBool("isJumping", false);
            jumpsUsed = 0;
        }

        // Choice handling
        if (!hasChosen)
        {
            string currentScene = SceneManager.GetActiveScene().name;

            if (truthCollider != null && playerCollider.bounds.Intersects(truthCollider.bounds))
            {
                MakeChoice(ChoiceType.Truth, truthText != null ? truthText.text : "Tell the truth", currentScene);
            }
            else if (lieCollider != null && playerCollider.bounds.Intersects(lieCollider.bounds))
            {
                MakeChoice(ChoiceType.Lie, lieText != null ? lieText.text : "Make up an excuse", currentScene);
            }
        }
    }

    private void MakeChoice(ChoiceType type, string choiceText, string currentScene)
    {
        hasChosen = true;

        if (currentScene == "ConsequenceScene")
        {
            if (type == ChoiceType.Truth)
            {
                if (!string.IsNullOrEmpty(GameManager.Instance.previousScene))
                {
                    SceneManager.LoadScene(GameManager.Instance.previousScene);
                }
            }
            else
            {
                var logic = FindObjectOfType<ConsequencesLogic>();
                if (logic != null) logic.ShowFinalConsequence();
            }
        }
        else
        {
            Debug.Log("Player chose " + type);
            GameManager.Instance.previousScene = currentScene;
            GameManager.Instance.currentChoice = type;
            GameManager.Instance.selectedChoiceText = choiceText;
            SceneManager.LoadScene("ConsequenceScene");
        }
    }

    private void AutoRandomChoice()
    {
        hasChosen = true;
        string currentScene = SceneManager.GetActiveScene().name;

        ChoiceType randomChoice = (Random.value < 0.5f) ? ChoiceType.Truth : ChoiceType.Lie;
        string choiceText = (randomChoice == ChoiceType.Truth) ?
            (truthText != null ? truthText.text : "Tell the truth") :
            (lieText != null ? lieText.text : "Make up an excuse");

        MakeChoice(randomChoice, choiceText, currentScene);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PowerUp"))
        {
            // Preserve facing direction
            float facingSign = Mathf.Sign(transform.localScale.x);

            // Slight shrink while keeping facing direction
            float baseAbsX = Mathf.Abs(originalScale.x);
            transform.localScale = new Vector3(baseAbsX * smallScaleFactor * facingSign,
                                               originalScale.y * smallScaleFactor,
                                               originalScale.z);

            // Small form = triple jump
            maxJumps = 3;

            // Adjust jump so 3 small jumps ≈ 2 normal jumps
            jumpPower = originalJumpPower * Mathf.Sqrt(2f / 3f);

            if (powerUpEffectPrefab != null)
            {
                Instantiate(powerUpEffectPrefab, other.transform.position, Quaternion.identity);
            }

            Destroy(other.gameObject);
            return;
        }

        if (hasChosen) return;

        string currentScene = SceneManager.GetActiveScene().name;

        if (other == truthCollider)
        {
            MakeChoice(ChoiceType.Truth, truthText != null ? truthText.text : "Tell the truth", currentScene);
        }
        else if (other == lieCollider)
        {
            MakeChoice(ChoiceType.Lie, lieText != null ? lieText.text : "Make up an excuse", currentScene);
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
        if ((isFacingRight && horizontalInput < 0f) || (!isFacingRight && horizontalInput > 0f))
        {
            isFacingRight = !isFacingRight;

            float sizeX = Mathf.Abs(transform.localScale.x);
            float sizeY = transform.localScale.y;

            // Flip only by changing X sign
            transform.localScale = new Vector3(sizeX * (isFacingRight ? 1f : -1f),
                                               sizeY,
                                               transform.localScale.z);
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
