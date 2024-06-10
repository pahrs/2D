using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class WallRes : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float cornerSpeedMultiplier = 0.5f; 
    public int playerIndex; 
    private PlayerInput playerInput;
    private Rigidbody2D rb; 
    private new Collider2D collider;
    private bool isMovingToCenter = false; 
    private bool isMovingToRandomDirection = false;
    private Vector2 targetPosition; 
    public GameObject bubble;
    private SpriteRenderer bubbleSpriteRenderer;
    private float timeRemaining = 120f; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        collider = GetComponent<Collider2D>(); 
        bubbleSpriteRenderer = bubble.GetComponent<SpriteRenderer>(); 
        StartCoroutine(TimeCountdown()); 
    }

    public void OnBubble(InputAction.CallbackContext context)
    {
        quitBubble();
    }

    void Update()
    {
        if (timeRemaining <= 0)
        {
            return; 
        }

        if (isMovingToCenter)
        {
            MoveToCenter();
            moveSpeed = 10f;
            bubbleSpriteRenderer.enabled = true;
        }
        else if (isMovingToRandomDirection)
        {
            MoveToRandomDirection();
            moveSpeed = 5f;
            bubbleSpriteRenderer.enabled = true;
        }

        CheckIfOutOfCameraBounds();
    }

    void MoveToCenter()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        rb.gravityScale = 0f; 

        if ((Vector2)transform.position == targetPosition)
        {
            isMovingToCenter = false;
            ChooseRandomDirection();
        }
    }

    void MoveToRandomDirection()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        rb.gravityScale = 0f; 

        if ((Vector2)transform.position == targetPosition)
        {
            isMovingToCenter = true;
            isMovingToRandomDirection = false; 
        }
    }

    void ChooseRandomDirection()
    {
        float x = Random.Range(0f, Screen.width);
        float y = Random.Range(0f, Screen.height);
        targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(x, y, 10f));
        isMovingToRandomDirection = true;
        collider.enabled = false; 
    }

    bool IsVisible()
    {
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        return viewportPosition.x >= 0 && viewportPosition.x <= 1 && viewportPosition.y >= 0 && viewportPosition.y <= 1;
    }

    void CheckIfOutOfCameraBounds()
    {
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        if (viewportPosition.x < 0 || viewportPosition.x > 1 || viewportPosition.y < 0)
        {
            CoinCount.instance.RemoveHalfCoins(playerIndex);

            targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 10f));
            isMovingToCenter = true;
        }
    }

    void quitBubble()
    {
        isMovingToRandomDirection = false;
        rb.gravityScale = 4f; 
        collider.enabled = true; 
        bubbleSpriteRenderer.enabled = false;
    }

    IEnumerator TimeCountdown()
    {
        while (timeRemaining > 0)
        {
            yield return new WaitForSeconds(1f);
            timeRemaining--;
        }

        this.enabled = false;
    }
}