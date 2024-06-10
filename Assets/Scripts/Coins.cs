using UnityEngine;

public class Coins : MonoBehaviour
{
    public int playerIndex; 
    public WallRes wallRes;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            if (!wallRes.onBubble)
            {
            Destroy(collision.gameObject); 
            CoinCount.instance.AddCoins(playerIndex, 10); 
            }
        }
    }
}