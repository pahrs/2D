using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;

    public int[] coinCounts = new int[4];

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddCoins(int playerIndex, int amount)
    {
        if (playerIndex >= 0 && playerIndex < coinCounts.Length)
        {
            coinCounts[playerIndex] += amount;
            Debug.Log("Player " + playerIndex + " Total Coins: " + coinCounts[playerIndex]);
        }
        else
        {
            Debug.LogError("Invalid player index: " + playerIndex);
        }
    }
}