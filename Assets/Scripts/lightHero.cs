using UnityEngine;
using UnityEngine.UI;

public class ligthHero : MonoBehaviour
{
    public GameObject[] players;
    public Image imagem;
    public float opacidadeInicial = 0f;
    public float opacidadeFinal = 1f;

    public float tempoDecorrido = 0f;


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
        tempoDecorrido += Time.deltaTime;

        if(tempoDecorrido >= 5f) 
        {
            Color cor = imagem.color;

            cor.a = opacidadeFinal;

            imagem.color = cor;
        }
    }
}


