using UnityEngine;

public class MoveController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    private float xInput;

    [Header("ground info")]
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private Transform groundObject;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] bool isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CollisionCheck();

        xInput = Input.GetAxisRaw("Horizontal");

        Movement();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

    }
    private void CollisionCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundObject.position, groundCheckRadius);
    }

    private void Jump()
    {
        if (isGrounded == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
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
