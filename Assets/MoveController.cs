using UnityEngine;

public class MoveController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    private float xInput;
    private bool facingRight = true;

    [Header("ground info")]
    [SerializeField] private Transform groundObject;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask groundLayer;
    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimationController();

        CollisionCheck();

        xInput = Input.GetAxisRaw("Horizontal");

        Movement();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

    }

    private void AnimationController()
    {
        animator.SetBool("isGround", isGrounded);
        animator.SetFloat("yVelocity", rb.velocity.y);
        animator.SetFloat("xVelocity", rb.velocity.x);
    }

    private void CollisionCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundObject.position, groundCheckRadius, groundLayer);
    }

    private void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void FixedUpdate()
    {
        if (rb.velocity.x < 0f && facingRight)
        {
            Flip();
        }
        else if (rb.velocity.x > 0f && !facingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    private void Movement()
    {
        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundObject.position, groundCheckRadius);    
    }
}
