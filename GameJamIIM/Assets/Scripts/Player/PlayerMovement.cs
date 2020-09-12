using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** TO DO :
            Particule Effect
*/
public class PlayerMovement : MonoBehaviour
{
    #region Script Parameters
    [Header("Physics")]
    [SerializeField] float maxSpeed = 7f;
    [SerializeField] float linearDrag = 10f;
    [SerializeField] float gravity = 1f;
    [SerializeField] float fallMultiplier = 5f;
    [SerializeField] float glissValue = 0.1f;
    [SerializeField] PlayerChangeForm pcf;

    [Header("Horizontal Movement")]
    [SerializeField] float moveSpeed = 100f;
    [SerializeField] Vector2 direction;
    private bool facingRight = true;
    private bool isMoving = false;

    [Header("Vertical Movement")]
    [SerializeField] float jumpSpeed = 15f;
    [SerializeField] float jumpDelay = 0.25f;
    [SerializeField] float hangTime = 0.1f;
    private float hangCounter;
    private float jumpTimer;
    private bool isJump = false;

    [Header("Collision")]
    [SerializeField] public bool onGround = false;
    [SerializeField] float groundLength = 1f;
    [SerializeField] Vector3 colliderOffset;
    #endregion

    #region Fields
    [Header("Components")]
    Rigidbody2D rb;
    CapsuleCollider2D playerCollider;
    [SerializeField] List<Animator> animator;
    //public Animator Animator
    //{
    //    get { return animator; }
    //    set { animator = value; }
    //}
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

        //for modify physics
        if (direction.x != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        JumpCheck();
        if ((jumpTimer > Time.time && onGround) && (hangCounter > 0 && Input.GetButtonDown("Jump")))
        {
            Jump();
        }
        Run(direction.x);

    }

    void FixedUpdate()
    {
        modifyPhysics();
    }

    void Run(float horizontal)
    {
        rb.AddForce(Vector2.right * horizontal * moveSpeed);

        if ((horizontal > 0 && !facingRight) || (horizontal < 0 && facingRight))
        {
            Flip();
        }

        //limit speed run
        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }

        // Run animation + velocity controle
        if (direction.x != 0)
        {
            foreach (Animator anim in animator)
            {
                anim.SetBool("isRunning", true);
            }
        }
        else
        {
            foreach (Animator anim in animator)
            {
                anim.SetBool("isRunning", false);
            }
            if(pcf.CurrentForm != eForms.BALLOON)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }

        //Speed Check
        foreach (Animator anim in animator)
        {
            anim.SetFloat("horizontal", Mathf.Abs(rb.velocity.x));
        }
    }

    #region Jump
    void JumpCheck()
    {
        bool wasOnGround = onGround;
        onGround = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z) + colliderOffset, Vector2.down, groundLength, groundLayer) || Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z) - colliderOffset, Vector2.down, groundLength, groundLayer);

        // Land animation
        //if (!wasOnGround && onGround)
        //{
        //    StartCoroutine(JumpSqueeze(1.5f, 0.5f, 0.03f));
        //}

        //Jumping Animation
        if (rb.velocity.y != 0)
        {
            foreach (Animator anim in animator)
            {
                anim.SetBool("isJumping", true);
            }
            isJump = true;
        }
        else
        {
            foreach (Animator anim in animator)
            {
                anim.SetBool("isJumping", false);
            }
            isJump = false;
        }

        //Coyote Time
        if (onGround)
        {
            hangCounter = hangTime;
            foreach (Animator anim in animator)
            {
                anim.SetBool("isJumping", false);
            }
            isJump = false;
        }
        else
        {
            hangCounter -= Time.deltaTime;
        }

        // Jump Delay after Input
        if (Input.GetButtonDown("Jump"))
        {
            jumpTimer = Time.time + jumpDelay;
        }

        foreach (Animator anim in animator)
        {
            anim.SetBool("onGround", onGround);
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0 + jumpSpeed);
        jumpTimer = 0;
        hangCounter = 0;
        //Squeeze animation 
        //StartCoroutine(JumpSqueeze(0.5f, 0.5f, 0.03f));
    }

    #endregion

    /** Modify GravityScale, linearDrag 
     */
    void modifyPhysics()
    {
        bool changingDirections = (direction.x > 0 && rb.velocity.x < 0) || (direction.x < 0 && rb.velocity.x > 0);

        //no slide between change direction exept Balloon
        if ((Mathf.Abs(direction.x) < 0.4f || changingDirections) && pcf.CurrentForm == eForms.BALLOON)
        {
             if (Mathf.Abs(rb.velocity.x) >= 1f)
             {
                    if(rb.velocity.x > 1)
                    {
                        rb.velocity = new Vector2(rb.velocity.x - glissValue, rb.velocity.y);
                        //Debug.Log("rb.velocity.x > 1 = " + rb.velocity.x);
                    }
                    else if(rb.velocity.x < -1)
                    {
                        rb.velocity = new Vector2(rb.velocity.x + glissValue, rb.velocity.y);
                        //Debug.Log("rb.velocity.x < -1 = " + rb.velocity.x);
                    }

                    if (Mathf.Round(rb.velocity.x) >= -1 && Mathf.Round(rb.velocity.x) <= 1 && pcf.CurrentForm == eForms.BALLOON)
                    {
                        rb.velocity = new Vector2(0, rb.velocity.y);
                    }
                    //Debug.Log(Mathf.Round(rb.velocity.x));
            }
        }
        else if (changingDirections)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }


        //No Jump
        if (onGround)
        {
            if (!isMoving)
            {
                //pour le stopper net
                if (!isJump)
                {
                    rb.drag = 0;
                }
                else if(pcf.CurrentForm == eForms.BALLOON)
                {
                    rb.drag = 0;
                }
                else
                {
                    rb.drag = linearDrag;
                    //rb.velocity = new Vector2(0, rb.velocity.y);
                }
            }

            else
            {
                rb.drag = 0f;
            }
            //if (Mathf.Abs(direction.x) < 0.4f || changingDirections)
            //{
            //    rb.drag = linearDrag;
            //}
            //else
            //{
            //    rb.drag = 0f;
            //}
            //rb.gravityScale = 0;

        }
        //Jump or falling
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

    /** Coroutine 
     */
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
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z) + colliderOffset, transform.TransformDirection(Vector3.down) * groundLength, Color.red);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z) - colliderOffset, transform.TransformDirection(Vector3.down) * groundLength, Color.red);
    }

    #endregion

    public void CopyCatPlayerMovement(PlayerMovement copycatPM)
    {
        maxSpeed = copycatPM.maxSpeed;
        linearDrag = copycatPM.linearDrag;
        gravity = copycatPM.gravity;
        fallMultiplier = copycatPM.fallMultiplier;
        glissValue = copycatPM.glissValue;

        moveSpeed = copycatPM.moveSpeed;

        jumpSpeed = copycatPM.jumpSpeed;
        jumpDelay = copycatPM.jumpDelay;
        hangTime = copycatPM.hangTime;
    }
}
