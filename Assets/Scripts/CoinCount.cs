using UnityEngine;
using System;

public class CoinCount : MonoBehaviour
{
    public static CoinCount instance;

    public int[] coinCount = new int[4];
    public static event Action OnCoinsChanged; // Evento para notificar mudanças nas moedas

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
        if (playerIndex >= 0 && playerIndex < coinCount.Length)
        {
            coinCount[playerIndex] += amount;
            Debug.Log("Player " + playerIndex + " Total Coins: " + coinCount[playerIndex]);
            OnCoinsChanged?.Invoke(); // Dispara o evento
        }
        else
        {
            Debug.LogError("Invalid player index: " + playerIndex);
        }
    }
}
