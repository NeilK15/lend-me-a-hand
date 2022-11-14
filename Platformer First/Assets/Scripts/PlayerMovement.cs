using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{

    public float speed = 10f;
    public float maxSpeed = 1.5f;
    public float jumpForce = 5f;
    public LayerMask groundMask;
    public float extraHeightTest = 0.01f;

    public Transform stepUpPosition;
    public Transform stepUpLimit;
    public float stepUpDistance = 1f;
    public float stepUpHeight = 0.3f;
    public float limitErrorCorrection = 0.05f;

    private bool grounded = true;
    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    new private BoxCollider2D collider;
    private float horizontal;

    public bool blockStatus;
    public bool limitStatus;

    RaycastHit2D block;
    RaycastHit2D limit;

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
        grounded = IsGrounded();
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded)
        {
            Jump();
        }

        block = Physics2D.Raycast(stepUpPosition.transform.position, transform.right, stepUpDistance, groundMask);
        limit = Physics2D.Raycast(stepUpLimit.transform.position, transform.right, stepUpDistance + limitErrorCorrection, groundMask);

        blockStatus = !block;
        limitStatus = !limit;
    }

    private void FixedUpdate()
    {
        Walk();
        Friction();

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
        }
        else if (horizontal > 0)
        {
            //spriteRenderer.flipX = false;
            //transform.localScale = new Vector3(1, 1, 1);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (grounded && horizontal != 0 && !block)
        {
            anim.SetBool("walking", true);
        }

        if (horizontal != 0 && !block)
        {
            rb.velocity += (new Vector2(horizontal * speed * Time.deltaTime, 0));
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), rb.velocity.y);

            //rb.velocity = new Vector2(horizontal * speed * Time.deltaTime, rb.velocity.y + 0.003f);
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private bool IsGrounded()
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
        



        Color rayColor;
        if (block.collider != null && limit == false && grounded)
        {
            rayColor = Color.green;
            transform.position = new Vector2(transform.position.x, transform.position.y + stepUpHeight);
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(stepUpPosition.transform.position, transform.right * stepUpDistance, rayColor);
        Debug.DrawRay(stepUpLimit.transform.position, transform.right * (stepUpDistance + limitErrorCorrection), rayColor);

    }

    public void Friction()
    {

        if (horizontal == 0)
        {
            if (rb.velocity.x > 0.0000001f)
                Debug.Log(rb.velocity);
            //rb.velocity = new Vector2 (0, rb.velocity.y);
        }




    }
}
