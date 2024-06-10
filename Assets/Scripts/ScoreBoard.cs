using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    public Text[] playerCoinsTexts; // Referências aos componentes de texto para cada jogador

    void Start()
    {
        // Inicializa os textos com as moedas iniciais
        UpdateScoreboard();
    }

    void OnEnable()
    {
        // Inscreve-se para ouvir mudanças nas moedas
        CoinCount.OnCoinsChanged += UpdateScoreboard;
    }

    void OnDisable()
    {
        // Desinscreve-se para evitar erros quando o objeto for destruído
        CoinCount.OnCoinsChanged -= UpdateScoreboard;
    }

    void UpdateScoreboard()
    {
        for (int i = 0; i < playerCoinsTexts.Length; i++)
        {
            playerCoinsTexts[i].text = "Player " + (i + 1) + ": " + CoinCount.instance.coinCount[i] + " Coins";
        }
    }
}
