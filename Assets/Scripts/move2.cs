using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class move2 : MonoBehaviour
{
    private float horizontal;
    private float speed = 10f;
    private float jumpingPower = 20f;
    private bool isFacingRight = true;

    private bool dashAvailable = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    [SerializeField] private UnityEngine.Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundlayer;
    [SerializeField] private TrailRenderer tr;
    void Update() 
    {
        if (isDashing)
        {
            return;
        }
        horizontal = Input.GetAxisRaw("Horizontal2");
        
        if (Input.GetKeyDown(KeyCode.Joystick2Button2) && IsGrounded())
        {
            rb.velocity = new UnityEngine.Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetKeyUp(KeyCode.Joystick2Button2) && rb.velocity.y > 0f)
        {
            rb.velocity = new UnityEngine.Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.Joystick2Button5) && dashAvailable)
        {
            StartCoroutine(Dash());
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
        dashAvailable = true;
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
        rb.velocity = new UnityEngine.Vector2 (transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
    }
}

