using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTronco : MonoBehaviour
{
    #region DataTronco
    const float AnchoTronco = 0.445f;
    const float AltoTronco = 0.386f;
    const float EspacioSuelo = 0.099f;
    private float Porcentaje;
    #endregion
    #region TroncoNuevo
    private float N_AnchoTronco=0;
    private float N_AltoTronco=0;
    private float N_EspacioSuelo=0;
    #endregion
    public GameObject proyectil;
    // Start is called before the first frame update
    /*private Vector3[] posiciones = {
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
    };*/
    private Vector3[] posiciones = new Vector3[16];
    private void Awake()
    {
        float Estatura = EstadoJuego.estadoJuego.Estatura;
        //float Estatura = 1.07f;
        Porcentaje =(Estatura == 0?1.8f:Estatura * 1)/ 1.8f;
        N_AltoTronco = AltoTronco * Porcentaje;
        N_AnchoTronco = AnchoTronco * Porcentaje;
        N_EspacioSuelo = EspacioSuelo * Porcentaje;
        LlenarPosiciones();
    }
    void LlenarPosiciones()
    {
        float Fila1 = (N_AltoTronco * 3.5f) + N_EspacioSuelo;
        float Fila2 = (N_AltoTronco * 2.5f) + N_EspacioSuelo;
        float Fila3 = (N_AltoTronco * 1.5f) + N_EspacioSuelo;
        float Fila4 = (N_AltoTronco * 0.5f) + N_EspacioSuelo;
        float A = (N_AnchoTronco * 0.5f);
        float B = (N_AnchoTronco * 1.5f);
        float nA = A*(-1);
        float nB = B * (-1);
        posiciones[0] = new Vector3(nB, Fila1, 2.5f);
        posiciones[1] = new Vector3(nA, Fila1, 2.5f);
        posiciones[2] = new Vector3(A, Fila1, 2.5f);
        posiciones[3] = new Vector3(B, Fila1, 2.5f);
        //
        posiciones[4] = new Vector3(nB, Fila2, 2.5f);
        posiciones[5] = new Vector3(nA, Fila2, 2.5f);
        posiciones[6] = new Vector3(A, Fila2, 2.5f);
        posiciones[7] = new Vector3(B, Fila2, 2.5f);
        //
        posiciones[8] = new Vector3(nB, Fila3, 2.5f);
        posiciones[9] = new Vector3(nA, Fila3, 2.5f);
        posiciones[10] = new Vector3(A, Fila3, 2.5f);
        posiciones[11] = new Vector3(B, Fila3, 2.5f);
        //
        posiciones[12] = new Vector3(nB, Fila4, 2.5f);
        posiciones[13] = new Vector3(nA, Fila4, 2.5f);
        posiciones[14] = new Vector3(A, Fila4, 2.5f);
        posiciones[15] = new Vector3(B, Fila4, 2.5f);
    }
    public void GeneraTronco(int i)
    {
        GameObject proy=Instantiate(proyectil, posiciones[i], transform.rotation);
        proy.transform.localScale = new Vector3(proy.transform.localScale.x * Porcentaje, proy.transform.localScale.y * Porcentaje, proy.transform.localScale.z);
        //proy.GetComponent<Fisica>().velocity();
    }
    public void AplicarVelocidad(float th)
    {
        GameObject[] proyectiles = GameObject.FindGameObjectsWithTag("proyectil");
        foreach (GameObject item in proyectiles)
        {
            //item.GetComponent<Fisica>().velocity(th);
            item.GetComponent<Fisica>().Desplazar(th);
        }
    }
    public void GeneraPoseProyectil(int[] troncos)
    {
        for (int i = 0; i < troncos.Length; i++)
        {
            GeneraTronco(troncos[i]-1);
        }
    }
}
