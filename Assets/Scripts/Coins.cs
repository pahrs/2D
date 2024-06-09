using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerIndex; // O índice deste jogador (0, 1, 2, 3)

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            Destroy(collision.gameObject); // Destrói a moeda
            CoinManager.instance.AddCoins(playerIndex, 10); // Adiciona 10 moedas para este jogador
        }
    }
}