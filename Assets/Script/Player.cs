using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 1f;
    public float jumpForce = 3f;
    public Animator animator;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Collider2D collider;
    private bool isGrounded;
    private bool doubleJump;
    private bool isFalling;

    private void TriggerJump()
    {
        animator.SetTrigger("Jump"); 
    }

    private void TriggerDoubleJump()
    {
        animator.SetTrigger("DoubleJump"); 
        animator.ResetTrigger("Jump");     
    }

    private void PlayFall()
    {
        if (!isGrounded && !animator.GetCurrentAnimatorStateInfo(0).IsName("DoubleJump"))
        {
            animator.SetBool("isFalling", true); 
        }
    }

    private void StopFall()
    {
        animator.SetBool("isFalling", false); 
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void SpriteFlip(float horizontalInput)
    {
        if (horizontalInput < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        animator.SetFloat("speed", Mathf.Abs(horizontalInput));        
        
        
        transform.Translate(new Vector3(horizontalInput * speed * Time.deltaTime, 0f, 0f));
        SpriteFlip(horizontalInput);
    }

    void Update()
    {
        
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            doubleJump = true; 
            TriggerJump(); 
        }
        else if (doubleJump && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f); 
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            doubleJump = false; 
            TriggerDoubleJump(); 
        }

        
        if (!isGrounded && rb.velocity.y < 0 && !animator.GetCurrentAnimatorStateInfo(0).IsName("DoubleJump"))
        {
            PlayFall(); 
        }

       
        if (isGrounded)
        {
            StopFall(); 
        }
    }

    public void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            doubleJump = false; 
            StopFall(); 
        }
    }

    public void OnCollisionExit2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
