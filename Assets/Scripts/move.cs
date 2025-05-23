using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using Unity.Mathematics;
using System;

public class Move : MonoBehaviour
{
    private static List<Move> allCharacters = new List<Move>();

    public Animator animator;

    private float horizontal;
    private float attackPower = 15f;
    private float speed = 10f;
    private float jumpingPower = 20f;
    private bool isFacingRight = true;

    private bool dashAvailable = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    private bool downDashAvailable = true;
    private bool isDownDashing;
    private float downDashingPower = 24f;
    private float downDashingTime = 0.2f;
    private float downDashingCooldown = 1f;
    private bool debuffed = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private CinemachineImpulseSource impulseSource;

    private PlayerInput playerInput;
    private Vector2 moveInput;

    private Vector2 knockback;
    private float knockbackDuration = 0.5f;
    private float knockbackTimer;
    private Coroutine debuffCoroutine;

    public delegate void DashHandler(Move dasher);
    public static event DashHandler OnDashStarted;
    public static event DashHandler OnDashEnded;

    private void Awake()
    {
        allCharacters.Add(this);
    }

    private void OnDestroy()
    {
        allCharacters.Remove(this);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Jump();
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        Dash();
    }

    public void OnDownDash(InputAction.CallbackContext context)
    {
        DownDash();
    }

    private void Update()
    {
        if (isDashing)
        {
            return;
        }

        horizontal = moveInput.x;

        Flip();
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        if (knockbackTimer > 0)
        {
            rb.velocity = knockback;
            knockbackTimer -= Time.fixedDeltaTime;
        }
        else
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void Jump()
    {
        if (IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
    }

    private void Dash()
    {
        if (dashAvailable)
        {
            StartCoroutine(DashCoroutine());
        }
    }

    private IEnumerator DashCoroutine()
    {
        dashAvailable = false;
        isDashing = true;
        
        OnDashStarted?.Invoke(this);

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        
        impulseSource.GenerateImpulse();

        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;

        OnDashEnded?.Invoke(this);

        yield return new WaitForSeconds(dashingCooldown);
        dashAvailable = true;
    }

    private void DownDash()
    {
        if (!IsGrounded())
        {
            StartCoroutine(DownDashCoroutine());
        }
    }

    private IEnumerator DownDashCoroutine()
    {
        downDashAvailable = false;
        isDownDashing = true;
        rb.velocity = new Vector2(0f, -downDashingPower);
        tr.emitting = true;
        while (!IsGrounded())
        {
            yield return null;
        }
        tr.emitting = false;
        isDownDashing = false;
        downDashAvailable = true;
    }

    public void ApplyKnockback(Move attacker)
    {
        Vector2 forceDirection = (transform.position - attacker.transform.position).normalized;
        forceDirection.x = forceDirection.x > 0 ? 1 : -1; 
        forceDirection.y = Mathf.Abs(forceDirection.y);
        knockback = forceDirection * attackPower;
        knockbackTimer = knockbackDuration;

        if (attacker.debuffed)
        {
            ApplyDebuff();
        }
    }

    private void ApplyDebuff()
    {
        if (debuffCoroutine != null)
        {
            StopCoroutine(debuffCoroutine);
        }
        debuffed = true;
        debuffCoroutine = StartCoroutine(DebuffCoroutine());
    }

    private IEnumerator DebuffCoroutine()
    {
        yield return new WaitForSeconds(4f);
        if (debuffed)
        {
            rb.velocity = new Vector2(-50f, 0); // Example of falling off the map
        }
        debuffed = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Move otherCharacter = collision.gameObject.GetComponent<Move>();
        if (otherCharacter != null && isDashing)
        {
            otherCharacter.ApplyKnockback(this);
        }
    }
}