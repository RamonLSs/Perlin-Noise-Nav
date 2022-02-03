using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class GeneradorPerlinNoise : MonoBehaviour
{
    public int x, z;
    public GameObject pieza, personaje;
    public NavMeshSurface superficie;
    private bool existePersonaje;

    void Start()
    {
        existePersonaje = false;
        GenerarMapa();
        GenerarPersonaje();
        Invoke("GenerarNavMesh", 10);
    }

    private void GenerarNavMesh()
    {
        superficie.BuildNavMesh();
    }

    private void GenerarMapa()
    {
        float semilla = Random.Range(0.0f, 1000.0f);
        float escala = 10.0f;
        int proporcion = 50;
        

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

                   /* int genP = Random.Range(0, 10);

                    if(genP == 5 && existePersonaje == false)
                    {
                        Instantiate(personaje, new Vector3(i * 5, 1.5f, j * 5), Quaternion.identity);
                        existePersonaje = true;
                    }*/
                }
            }
        }
    }

    private void GenerarPersonaje()
    {
        int iteraciones = 0;
        while (existePersonaje == false)
        {
            if(iteraciones == 1000)
            {
                break;
            }
            iteraciones++;
            Debug.Log(iteraciones);

            int CoorX = Random.Range(0, (x*5)+1);
            int CoorZ = Random.Range(0, (z * 5) + 1);

            Vector3 IniciarRayo = new Vector3(CoorX, 6, CoorZ);

            if (Physics.Raycast(IniciarRayo, Vector3.down, 10))
            {
                Instantiate(personaje, new Vector3(CoorX, 3.5f, CoorZ), Quaternion.identity);
                existePersonaje = true;
            }
        }
    }

}
