using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class Move : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private CinemachineImpulseSource impulseSource;
    [SerializeField] private AudioSource audioSource; 
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip dashSound;
    private static List<Move> allCharacters = new List<Move>();
    public Animator animator;
    private float horizontal;
    private float attackPower = 15f;
    public float speed = 10f;
    public float jumpingPower = 20f;
    private bool isFacingRight = true;
    public bool dashAvailable = true;
    private bool isDashing;
    public float dashingPower = 24f;
    public float dashingTime = 0.2f;
    private float dashingCooldown = 1f;
    private Vector2 moveInput;
    private Vector2 knockback;
    private float knockbackDuration = 0.3f;
    private float knockbackTimer;
    public delegate void DashHandler(Move dasher);
    public static event DashHandler OnDashStarted;
    public static event DashHandler OnDashEnded;

    private void Update()
    {
        if (isDashing)
        {
            return;
        }
        if (IsGrounded())
        {
            animator.SetBool("IsJumping", false);
        }
        else
        {
            animator.SetBool("IsJumping", true);
        }

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

    private void Awake()
    {
        allCharacters.Add(this);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        horizontal = Mathf.Abs(moveInput.x) > 0.1f ? Mathf.Sign(moveInput.x) : 0;
        Debug.Log(horizontal);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Jump();
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (knockbackTimer<=0)
        {
           Dash(); 
        }
    }

    private bool IsGrounded()
    {
        bool grounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if (!grounded)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.2f);
            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject != gameObject && collider.GetComponent<Move>() != null)
                {
                    grounded = true;
                    break;
                }
            }
        }
        return grounded;
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
            PlaySound(jumpSound); 
        } 
    }

    private void Dash()
    {
        if (dashAvailable)
        {
            StartCoroutine(PlayerDash());
        }
    }

    private IEnumerator PlayerDash()
    {
        dashAvailable = false;
        isDashing = true;
        animator.SetBool("IsDashing", true);
        OnDashStarted?.Invoke(this);

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;

        impulseSource.GenerateImpulse();
        PlaySound(dashSound); 

        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        animator.SetBool("IsDashing", false);
        OnDashEnded?.Invoke(this);

        yield return new WaitForSeconds(dashingCooldown);
        dashAvailable = true;
    }

    public void Knockback(Move attacker)
    {
        Vector2 forceDirection = (transform.position - attacker.transform.position).normalized;
        forceDirection.x = forceDirection.x > 0 ? 1 : -1; 
        forceDirection.y = Mathf.Abs(forceDirection.y);
        knockback = forceDirection * attackPower;
        knockbackTimer = knockbackDuration;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Move otherCharacter = collision.gameObject.GetComponent<Move>();
        if (otherCharacter != null && isDashing)
        {
            otherCharacter.Knockback(this);
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}