using UnityEngine;

public class movePlayer : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;
    public float jumpForce = 12f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.15f;
    public LayerMask groundLayer;

    public Animator animator;

    private Rigidbody2D rb;
    private float moveInput;
    private bool isGrounded;
    private bool jumpRequest;
    private Vector3 originalScale;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        originalScale = transform.localScale;  // save original scale
    }

    void Update()
    {
        // Get input and store in class-level variable
        moveInput = Input.GetAxisRaw("Horizontal");

        // Flip sprite based on direction
        if (moveInput > 0)
        // moving right but sprite faces left by default, so flip left
        transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
    else if (moveInput < 0)
        // moving left, so flip right
        transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);

        // Update animator Speed parameter for walking animation
        animator.SetFloat("Speed", Mathf.Abs(moveInput));

        // Check if player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Update animator for jumping/landing
        animator.SetBool("IsGrounded", isGrounded);

        // Check jump input, set jumpRequest flag
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpRequest = true;
            animator.SetTrigger("Jump"); // Optional: if you have a jump trigger animation
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void FixedUpdate()
    {
        // Apply horizontal movement
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Handle jump in FixedUpdate for physics
        if (jumpRequest)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpRequest = false;
        }
        // Apply horizontal movement using physics
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
