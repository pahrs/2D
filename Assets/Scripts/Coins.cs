using UnityEngine;

public class Coins : MonoBehaviour
{
    public int playerIndex; 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            Destroy(collision.gameObject); 
            CoinManager.instance.AddCoins(playerIndex, 10); 
        }
    }
}