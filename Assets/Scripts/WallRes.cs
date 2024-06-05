using UnityEngine;

public class WallRes : MonoBehaviour
{
    public float moveSpeed = 50f; // Velocidade de movimento
    public float cornerSpeedMultiplier = 0.5f; // Multiplicador de velocidade ao mover para os cantos

    private Rigidbody2D rb; // Referência ao Rigidbody2D
    private new Collider2D collider; // Referência ao Collider2D
    private bool isMovingToCenter = false; // Indicador de movimento para o centro
    private bool isMovingToRandomDirection = false; // Indicador de movimento para uma direção aleatória
    private Vector2 targetPosition; // Posição alvo
    public GameObject bubble;
    private SpriteRenderer bubbleSpriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Obtém o Rigidbody2D
        collider = GetComponent<Collider2D>(); // Obtém o Collider2D
        bubbleSpriteRenderer = bubble.GetComponent<SpriteRenderer>(); // Obtém o SpriteRenderer
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isMovingToRandomDirection = false;
            rb.gravityScale = 1f; // Reativa a gravidade
            collider.enabled = true; // Reativa o collider
            bubbleSpriteRenderer.enabled = false;
            
        }

        if (isMovingToCenter)
        {
            MoveToCenter();
            moveSpeed = 50f;
            bubbleSpriteRenderer.enabled = true;
        }
        else if (isMovingToRandomDirection)
        {
            MoveToRandomDirection();
            moveSpeed = 25f;
            bubbleSpriteRenderer.enabled = true;
        }
    }

    void MoveToCenter()
    {
        // Movendo o herói para o centro da tela
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        rb.gravityScale = 0f; // Desativa a gravidade

        // Verificar se o herói já está no centro
        if ((Vector2)transform.position == targetPosition)
        {
            isMovingToCenter = false;
            ChooseRandomDirection();
        }
    }

    void MoveToRandomDirection()
    {
        // Movendo o herói para uma direção aleatória
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        rb.gravityScale = 0f; // Desativa a gravidade

        // Verificar se o herói já chegou à posição aleatória
        if ((Vector2)transform.position == targetPosition)
        {
            isMovingToCenter = true;
            isMovingToRandomDirection = false;
            rb.gravityScale = 1f; // Reativa a gravidade
            collider.enabled = true; // Reativa o collider
        }
    }

    void ChooseRandomDirection()
    {
        // Escolhe uma direção aleatória para mover
        float x = Random.Range(0f, Screen.width);
        float y = Random.Range(0f, Screen.height);
        targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(x, y, 10f));
        isMovingToRandomDirection = true;
        collider.enabled = false; // Desativa o collider temporariamente
    }

    bool IsVisible()
    {
        // Verifica se o herói está visível na tela
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        return viewportPosition.x >= 0 && viewportPosition.x <= 1 && viewportPosition.y >= 0 && viewportPosition.y <= 1;
    }

    void OnBecameInvisible()
    {
        // Quando o herói sai da tela, move para o centro
        targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 10f));
        isMovingToCenter = true;
    }
}
