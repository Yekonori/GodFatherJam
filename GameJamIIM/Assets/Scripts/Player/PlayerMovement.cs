using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** TO DO : Add coyote Time
            Particule Effect
            Resolve velocity.x 0 on landing
*/
public class PlayerMovement : MonoBehaviour
{
    #region Script Parameters
    [Header("Physics")]
    [SerializeField] float maxSpeed = 7f;
    [SerializeField] float linearDrag = 10f;
    [SerializeField] float gravity = 1f;
    [SerializeField] float fallMultiplier = 5f;

    [Header("Horizontal Movement")]
    [SerializeField] float moveSpeed = 100f;
    [SerializeField] Vector2 direction;
    private bool facingRight = true;

    [Header("Vertical Movement")]
    [SerializeField] float jumpSpeed = 15f;
    [SerializeField] float jumpDelay = 0.25f;
    private float jumpTimer;

    [Header("Collision")]
    [SerializeField] bool onGround = false;
    [SerializeField] float groundLength = 0.6f;
    [SerializeField] Vector3 colliderOffset;
    #endregion

    #region Fields
    [Header("Components")]
    Rigidbody2D rb;
    CapsuleCollider2D playerCollider;
    [SerializeField] Animator animator;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] GameObject characterHolder;
    #endregion

    #region Unity Methods
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CapsuleCollider2D>();
    }
    // Update is called once per frame
    void Update()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        JumpCheck();
        if (jumpTimer > Time.time && onGround)
        {
            Jump();
        }
        Run(direction.x);

    }

    void FixedUpdate()
    {
        modifyPhysics();
    }

    /* Problem : Jump make Velocity.x go to instant 0 (same problem when landing)
      */
    void Run(float horizontal)
    {
        rb.AddForce(Vector2.right * horizontal * moveSpeed);

        if ((horizontal > 0 && !facingRight) || (horizontal < 0 && facingRight))
        {
            Flip();
        }
        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }

        // Run animation
        if (direction.x != 0)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        //Speed Check
        animator.SetFloat("horizontal", Mathf.Abs(rb.velocity.x));
    }

    #region Jump
    void JumpCheck()
    {
        bool wasOnGround = onGround;
        onGround = Physics2D.Raycast(transform.position + colliderOffset, Vector2.down, groundLength, groundLayer) || Physics2D.Raycast(transform.position - colliderOffset, Vector2.down, groundLength, groundLayer);

        // Land animation
        if (!wasOnGround && onGround)
        {
            StartCoroutine(JumpSqueeze(1.5f, 0.5f, 0.05f));
        }
        

        // Jump Delay after Input
        if (Input.GetButtonDown("Jump"))
        {
            jumpTimer = Time.time + jumpDelay;
        }

        animator.SetBool("onGround", onGround);
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        //rb.velocity =  Vector2.up * jumpSpeed;
        
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + jumpSpeed);
        jumpTimer = 0;
        //Squeeze animation 
        StartCoroutine(JumpSqueeze(0.5f, 0.5f, 0.05f));
    }

    #endregion

    /** Modify GravityScale, linearDrag 
     */
    void modifyPhysics()
    {
        bool changingDirections = (direction.x > 0 && rb.velocity.x < 0) || (direction.x < 0 && rb.velocity.x > 0);

        //No Jump
        if (onGround)
        {
            if (Mathf.Abs(direction.x) < 0.4f || changingDirections)
            {
                rb.drag = linearDrag;
            }
            else
            {
                rb.drag = 0f;
            }
            rb.gravityScale = 0;
        }
        //Jump
        else
        {
            rb.gravityScale = gravity;
            rb.drag = linearDrag * 0.015f;
            if (rb.velocity.y < 0)
            {
                rb.gravityScale = gravity * fallMultiplier;
            }
            //Replace GetButtonDown by GetButton for Higher Jump if press down 
            else if (rb.velocity.y > 0 && !Input.GetButtonDown("Jump"))
            {
                rb.gravityScale = gravity * (fallMultiplier / 2);
            }

        }

    }
    void Flip()
    {
        facingRight = !facingRight;
        transform.rotation = Quaternion.Euler(0, facingRight ? 0 : 180, 0);
    }

    IEnumerator JumpSqueeze(float xSqueeze, float ySqueeze, float seconds)
    {
        Vector3 originalSize = Vector3.one;
        Vector3 newSize = new Vector3(xSqueeze, ySqueeze, originalSize.z);
        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            characterHolder.transform.localScale = Vector3.Lerp(originalSize, newSize, t);
            yield return null;
        }
        t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            characterHolder.transform.localScale = Vector3.Lerp(newSize, originalSize, t);
            yield return null;
        }

    }
    /**  Raycast Drawing
     * */
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + colliderOffset, transform.position + colliderOffset + Vector3.down * groundLength);
        Gizmos.DrawLine(transform.position - colliderOffset, transform.position - colliderOffset + Vector3.down * groundLength);
    }
    
    #endregion
}
