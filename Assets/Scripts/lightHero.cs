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
            tempoDecorrido = 0f;
            Debug.Log(tempoDecorrido);
            tempoDecorrido += Time.deltaTime;

            Color cor = imagem.color;
            cor.a = opacidadeFinal;
            imagem.color = cor;

            if(tempoDecorrido >= duracao)
            {

            }
        }


        if(!isActive)
        {
            Color cor = imagem.color;
            cor.a = opacidadeInicial;
            imagem.color = cor;
        }  
    }
}


