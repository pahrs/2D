using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class move : MonoBehaviour
{
    private float horizontal;
    private float attackPower = 10f;
    private float speed = 10f;
    private float jumpingPower = 30f;
    private bool isFacingRight = true;

    private bool dashAvailable = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    private bool downDashAvailable = true;
    private bool isDownDashing;
    private float DownDashingPower = 24f;
    private float DownDashingTime = 0.2f;
    private float DownDashingCooldown = 1f;




    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundlayer;
    [SerializeField] private TrailRenderer tr;

    void Update() 
    {
        if (isDashing)
        {
            return;
        }
        float verticalInput = Input.GetAxis("Vertical1");
        horizontal = Input.GetAxisRaw("Horizontal1");
        
        
        if (Input.GetKeyDown(KeyCode.Joystick1Button2) && IsGrounded())
        {
            rb.velocity = new UnityEngine.Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetKeyUp(KeyCode.Joystick1Button2) && rb.velocity.y > 0f)
        {
            rb.velocity = new UnityEngine.Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button5) && dashAvailable)
        {
            StartCoroutine(Dash());
        }
         if (Input.GetKeyDown(KeyCode.Joystick1Button4) && !IsGrounded())
        {
            StartCoroutine (downDash());
        }
        

        Flip();
    }   

    private void FixedUpdate()
    {  
        if (isDashing)
        {
            return;
        }
        
        rb.velocity = new UnityEngine.Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundlayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            UnityEngine.Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private IEnumerator Dash()
    {
        dashAvailable = false;
        isDashing = true; 
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new UnityEngine.Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        dashAvailable = true;
    }
    private IEnumerator downDash()
    {
    downDashAvailable = false;
    isDownDashing = true; 
    rb.velocity = new UnityEngine.Vector2(0f, -DownDashingPower);
    tr.emitting = true;
    while (!IsGrounded())
    {
    yield return null;   
    }
    tr.emitting = false;
    isDownDashing = false;
    downDashAvailable = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDashing)
        {
            if (collision.gameObject.tag == "Hero")
            {
                Rigidbody2D heroRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();

                if (heroRigidbody != null)
                {
                    UnityEngine.Vector2 forceDirection = (collision.transform.position - transform.position).normalized;
                    forceDirection.x = -1;
                    forceDirection.y = Mathf.Abs(forceDirection.y); 
                    Debug.Log("Force Direction" + forceDirection);
                    heroRigidbody.AddForce(forceDirection * attackPower, ForceMode2D.Impulse);
                }
            }
        }
    }
}

