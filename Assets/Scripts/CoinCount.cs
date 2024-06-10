using UnityEngine;
using System;

public class CoinCount : MonoBehaviour
{
    public static CoinCount instance;

    public int[] coinCount = new int[4];
    public static event Action OnCoinsChanged; 

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            
            for (int i = 0; i < coinCount.Length; i++)
            {
                coinCount[i] = 0;
            }
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
            OnCoinsChanged?.Invoke(); 
        }
        else
        {
            Debug.LogError("Invalid player index: " + playerIndex);
        }
    }

    public void RemoveHalfCoins(int playerIndex)
    {
        if (playerIndex >= 0 && playerIndex < coinCount.Length)
        {
            coinCount[playerIndex] = Mathf.Max(coinCount[playerIndex] / 2, 0);
            Debug.Log("Player " + playerIndex + " Lost Half Coins: " + coinCount[playerIndex]);
            OnCoinsChanged?.Invoke(); 
        }
        else
        {
            Debug.LogError("Invalid player index: " + playerIndex);
        }
    }
}