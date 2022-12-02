using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Info")]
    public float speed = 10f;
    public float jumpForce = 5f;
    public LayerMask groundMask;
    public float extraHeightTest = 0.01f;
    public float groundDrag = 2f;

    [Header("Step Up References and Info")]
    public Transform stepUpPosition;
    public Transform stepUpLimit;
    public float stepUpDistance = 1f;
    public float stepUpHeight = 0.3f;
    public float stepOverLength = 0.15f;

    private bool grounded = true;
    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    new private BoxCollider2D collider;
    private float horizontal;


    public Transform pickUpSpot;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        grounded = isGrounded();
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded)
        {
            Jump();
        }

        if (grounded) {
            rb.drag = groundDrag;
        } else
        {
            rb.drag = 0;
        }
    }

    private void FixedUpdate()
    {
        Walk();
        StepUp();
    }

    private void Walk()
    {


        anim.SetBool("walking", false);

        if (horizontal < 0)
        {
            //spriteRenderer.flipX = true;
            //transform.localScale = new Vector3(-1, 1, 1);
            transform.rotation = Quaternion.Euler(0, 180, 0);
            stepOverLength = -Mathf.Abs(stepOverLength);
        }
        else if (horizontal > 0)
        {
            //spriteRenderer.flipX = false;
            //transform.localScale = new Vector3(1, 1, 1);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            stepOverLength = Mathf.Abs(stepOverLength);
        }

        if (grounded && horizontal != 0)
        {
            anim.SetBool("walking", true);
        }

        if (horizontal != 0)
            rb.velocity = new Vector2(horizontal * speed * Time.deltaTime, rb.velocity.y);
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private bool isGrounded()
    {
        RaycastHit2D ray = Physics2D.Raycast(collider.bounds.center, Vector2.down, collider.bounds.extents.y + extraHeightTest, groundMask);
        Color rayColor;
        if (ray.collider != null)
        {
            rayColor = Color.green;
        } 
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(collider.bounds.center, Vector2.down * (collider.bounds.extents.y + extraHeightTest), rayColor);
        return ray.collider != null;
    }

    private void StepUp()
    {
        RaycastHit2D block = Physics2D.Raycast(stepUpPosition.transform.position, transform.right, stepUpDistance, groundMask);

        RaycastHit2D limit = Physics2D.Raycast(stepUpLimit.transform.position, transform.right, stepUpDistance + 0.01f, groundMask);



        Color rayColor;
        if (block.collider != null && limit == false && grounded)
        {
            rayColor = Color.green;
            transform.position = new Vector2(transform.position.x + stepOverLength, transform.position.y + stepUpHeight);
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(stepUpPosition.transform.position, transform.right * stepUpDistance, rayColor);
        Debug.DrawRay(stepUpLimit.transform.position, transform.right * stepUpDistance, Color.gray);

    }
}
