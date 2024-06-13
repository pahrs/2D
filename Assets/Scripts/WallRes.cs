using UnityEngine;
using UnityEngine.InputSystem;

public class WallRes : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float cornerSpeedMultiplier = 0.5f; 
    public int playerIndex; 
    private Rigidbody2D rb; 
    public Collider2D colliderP;
    private bool isMovingToCenter = false; 
    private bool isMovingToRandomDirection = false;
    private Vector2 targetPosition; 
    public GameObject bubble;
    private SpriteRenderer bubbleSpriteRenderer;
    private float timeRemaining = 120f; 
    public bool onBubble;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        colliderP = GetComponent<Collider2D>(); 
        bubbleSpriteRenderer = bubble.GetComponent<SpriteRenderer>(); 
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
        colliderP.enabled = false; 
    }

    void CheckIfOutOfCameraBounds()
    {
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        if (viewportPosition.x < 0 || viewportPosition.x > 1 || viewportPosition.y < 0)
        {
            targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 10f));
            isMovingToCenter = true;
            if (onBubble == false)
            {
                CoinCount.instance.RemoveHalfCoins(playerIndex);
            }
            onBubble = true;
        }
    }

    void quitBubble()
    {
        isMovingToRandomDirection = false;
        rb.gravityScale = 4f; 
        colliderP.enabled = true; 
        bubbleSpriteRenderer.enabled = false;
        onBubble = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            if ((collision.gameObject != null)&&!onBubble)
            {
            Destroy(collision.gameObject); 
            CoinCount.instance.AddCoins(playerIndex, 10); 
            }
        }
    }
}