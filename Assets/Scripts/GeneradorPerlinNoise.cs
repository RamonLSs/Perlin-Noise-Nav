using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class GeneradorPerlinNoise : MonoBehaviour
{
    public int x, z;
    public GameObject pieza, personaje;
    public NavMeshSurface superficie;
    
    void Start()
    {
        Generar();
        Invoke("GenerarNavMesh", 10);
    }

    private void GenerarNavMesh()
    {
        superficie.BuildNavMesh();
    }

    private void Generar()
    {
        float semilla = Random.Range(0.0f, 1000.0f);
        float escala = 10.0f;
        int proporcion = 50;
        bool existePersonaje = false;

        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < z; j++)
            {
                float corX = semilla + i / escala;
                float corY = semilla + j / escala;

                int r = (int)(Mathf.PerlinNoise(corX, corY) * proporcion);
                if (r > 20)
                {
                    Instantiate(pieza, new Vector3(i * 5, 0, j * 5), Quaternion.identity);

                    int genP = Random.Range(0, 10);

                    if(genP == 5 && existePersonaje == false)
                    {
                        Instantiate(personaje, new Vector3(i * 5, 1.5f, j * 5), Quaternion.identity);
                        existePersonaje = true;
                    }
                }
            }
        }
    }

}
