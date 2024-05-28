using UnityEngine;
using UnityEngine.UI;

public class LightController : MonoBehaviour
{
    public Light2D light2D; // Referência ao componente Light2D
    public Image blackScreen; // Referência ao objeto de tela preta
    public float eventStartTime = 60f; // Tempo em segundos para iniciar o evento
    public float eventDuration = 10f; // Duração do evento em segundos
    public float lightRadius = 5f; // Tamanho da luz
    private float timer;
    private bool eventActive = false;

    void Start()
    {
        timer = 0f;
        light2D.enabled = false; // Iniciar com a luz desativada
        blackScreen.color = new Color(0, 0, 0, 0); // Transparente
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (!eventActive && timer >= eventStartTime)
        {
            StartEvent();
        }
        else if (eventActive && timer >= eventStartTime + eventDuration)
        {
            EndEvent();
        }
    }

    void StartEvent()
    {
        eventActive = true;
        light2D.enabled = true;
        light2D.pointLightOuterRadius = lightRadius;
        StartCoroutine(FadeBlackScreen(1f, 1f)); // Escurecer a tela em 1 segundo
    }

    void EndEvent()
    {
        eventActive = false;
        light2D.enabled = false;
        StartCoroutine(FadeBlackScreen(0f, 1f)); // Clarear a tela em 1 segundo
    }

    System.Collections.IEnumerator FadeBlackScreen(float targetAlpha, float duration)
    {
        float startAlpha = blackScreen.color.a;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            blackScreen.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        blackScreen.color = new Color(0, 0, 0, targetAlpha);
    }
}