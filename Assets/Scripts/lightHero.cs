using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class ligthHero : MonoBehaviour
{
    public GameObject[] players;
    public Image imagem;
    public float opacidadeInicial = 0f;
    public float opacidadeFinal = 1f;
    public bool isActive;

    public float tempoDecorrido = 0f;
    public float duracao = 20f;


    private void Start()
    {

        if (imagem != null)
        {
            Color cor = imagem.color;

            cor.a = opacidadeInicial;

            imagem.color = cor;
        }
    }
    void Update()
    {

        if(isActive) 
        {
            tempoDecorrido += Time.deltaTime;

            if (tempoDecorrido <= duracao)
            {
                Color cor = imagem.color;
                cor.a = Mathf.Lerp(opacidadeInicial, opacidadeFinal, tempoDecorrido / duracao);
                imagem.color = cor;
            }
            else
            {
                isActive = false; // Desativa isActive após a duração
            }
        }
        else
        {
            Color cor = imagem.color;
            cor.a = opacidadeInicial;
            imagem.color = cor;
        }
    }
}

        
    

    

