
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawn : MonoBehaviour
{
    public string tagDoPrefab = "Ground"; 
    public float alturaMaxima = 6f; 
    public float intervaloDeGeracao = 2.5f; 
    public float rangeXMin = -8f;
    public float rangeXMax = 8f;

    public float timer; 

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= intervaloDeGeracao)
        {
            
            GameObject[] prefabs = GameObject.FindGameObjectsWithTag(tagDoPrefab);

            
            if (prefabs.Length > 0)
            {
                
                GameObject prefabSelecionado = prefabs[Random.Range(0, prefabs.Length)];

                
                float posX = Random.Range(rangeXMin, rangeXMax);

                
                GameObject novaPlataforma = Instantiate(prefabSelecionado, new Vector3(posX, alturaMaxima, 0f), Quaternion.identity);

                
                timer = 0f;
            }
            
        }
    }
}