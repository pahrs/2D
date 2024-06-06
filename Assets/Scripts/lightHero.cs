using UnityEngine;

public class lightHero : MonoBehaviour
{
    public bool isActive;
    public float tempoDecorrido = 0f;
    public float duracao = 20f;

    public GameObject lightGlobal;
    public GameObject LightHero1;
    public GameObject lightHero2;

    private void Start()
    {
        isActive = false;
        DeactivateLights();
    }

    void Update()
    {
        if (isActive)
        {
            tempoDecorrido += Time.deltaTime;

            if (tempoDecorrido <= duracao)
            {
                ActivateLights();
            }
            else
            {
                isActive = false;
                tempoDecorrido = 0f; // Reset o tempo decorrido
                DeactivateLights();
            }
        }
        else
        {
            DeactivateLights();
        }
    }

    public void Activate()
    {
        isActive = true;
        tempoDecorrido = 0f; // Reset o tempo decorrido ao ativar
    }

    private void ActivateLights()
    {
        lightGlobal.SetActive(true);
        LightHero1.SetActive(true);
        lightHero2.SetActive(true);
    }

    private void DeactivateLights()
    {
        lightGlobal.SetActive(false);
        LightHero1.SetActive(false);
        lightHero2.SetActive(false);
    }
}
