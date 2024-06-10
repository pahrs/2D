using UnityEngine;

public class Coins : MonoBehaviour
{
    public int playerIndex; 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            if (collision.gameObject != null)
            {
            Destroy(collision.gameObject); 
            CoinCount.instance.AddCoins(playerIndex, 10); 
            }
        }
    }
}