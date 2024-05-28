using UnityEngine;

public class ligthHero : MonoBehaviour
{
    public GameObject[] players;
    public GameObject blackScreen;
    public float visibilityStartTime = 130f; // Tempo para começar a tela preta a ficar visível (2:10 minutos)
    public float visibilityDuration = 20f; // Duração da visibilidade da tela preta (20 segundos)
    public float effectDuration = 10f; // Duração do efeito

    private bool eventStarted = false;
    private float currentTime;
    private bool blackScreenVisible = false;

    void Start()
    {
        currentTime = visibilityStartTime + visibilityDuration; // Começa invisível
        SetBlackScreenVisibility(false);
    }

    void Update()
    {
        currentTime -= Time.deltaTime;

        if (!eventStarted && currentTime <= visibilityStartTime)
        {
            SetBlackScreenVisibility(true); // Torna a tela preta visível
            eventStarted = true;
        }

        if (eventStarted && currentTime <= 0)
        {
            EndEvent();
        }
    }

    void EndEvent()
    {
        // Desativar efeito de luz nos jogadores
        foreach (GameObject player in players)
        {
            Light playerLight = player.GetComponent<Light>();
            if (playerLight != null)
            {
                playerLight.enabled = false;
            }
        }

        eventStarted = false;
        SetBlackScreenVisibility(false); // Torna a tela preta invisível
    }

    void SetBlackScreenVisibility(bool visible)
    {
        blackScreen.SetActive(visible);
        blackScreenVisible = visible;
    }
}
