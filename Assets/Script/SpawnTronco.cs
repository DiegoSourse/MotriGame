using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTronco : MonoBehaviour
{
    public GameObject proyectil;
    // Start is called before the first frame update
    private Vector3[] posiciones = {
                                    new Vector3(-0.6675f, 1.45f, 2.5f),
                                    new Vector3(-0.2225f, 1.45f, 2.5f),
                                    new Vector3(0.2225f, 1.45f, 2.5f),
                                    new Vector3(0.6675f, 1.45f, 2.5f),
                                    new Vector3(-0.6675f, 1.064f, 2.5f),
                                    new Vector3(-0.2225f, 1.064f, 2.5f),
                                    new Vector3(0.2225f, 1.064f, 2.5f),
                                    new Vector3(0.6675f, 1.064f, 2.5f),
                                    new Vector3(-0.6675f, 0.678f, 2.5f),
                                    new Vector3(-0.2225f, 0.678f, 2.5f),
                                    new Vector3(0.2225f, 0.678f, 2.5f),
                                    new Vector3(0.6675f, 0.678f, 2.5f),
                                    new Vector3(-0.6675f, 0.292f, 2.5f),
                                    new Vector3(-0.2225f, 0.292f, 2.5f),
                                    new Vector3(0.2225f, 0.292f, 2.5f),
                                    new Vector3(0.6675f, 0.292f, 2.5f)
    };
    public void generaTronco(int i)
    {
        GameObject proy=Instantiate(proyectil, posiciones[i], transform.rotation);
        //proy.GetComponent<Fisica>().velocity();
    }
    public void aplicarVelocidad(float th)
    {
        GameObject[] proyectiles = GameObject.FindGameObjectsWithTag("proyectil");
        //Fisica fis = GetComponent<Fisica>();
        foreach (GameObject item in proyectiles)
        {
            item.GetComponent<Fisica>().velocity(th);
        }
    }
    public void generaPoseProyectil(int[] troncos)
    {
        for (int i = 0; i < troncos.Length; i++)
        {
            generaTronco(troncos[i]-1);
        }
    }
}
