using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnStar : MonoBehaviour
{
    #region DataTronco
    const float AnchoTronco = 0.445f;
    const float AltoTronco = 0.386f;
    const float EspacioSuelo = 0.099f;
    private float Porcentaje;
    #endregion
    #region TroncoNuevo
    private float N_AnchoTronco = 0;
    private float N_AltoTronco = 0;
    private float N_EspacioSuelo = 0;
    #endregion
    public GameObject proyectil;
    private Vector3[] PosicionStar = new Vector3[30];
    private void Awake()
    {
        float Estatura = EstadoJuego.estadoJuego.Estatura;
        //float Estatura = 1.07f;
        Porcentaje = (Estatura == 0 ? 1.8f : Estatura * 1) / 1.8f;
        N_AltoTronco = AltoTronco * Porcentaje;
        N_AnchoTronco = AnchoTronco * Porcentaje;
        N_EspacioSuelo = EspacioSuelo * Porcentaje;
        LlenarPosiciones();
    }
    void LlenarPosiciones()
    {
        float s1 = (N_AltoTronco * 1.0f) + N_EspacioSuelo;
        float s2 = (N_AltoTronco * 2.0f) + N_EspacioSuelo;
        float sa = N_AnchoTronco;
        float sb = 0.0f;
        float sc = sa * (-1);
        PosicionStar[0] = new Vector3(sb, s1, 2.5f);
        PosicionStar[1] = new Vector3(sb, s1, 2.5f);
        PosicionStar[2] = new Vector3(sb, s1, 2.5f);
        PosicionStar[3] = new Vector3(sb, s1, 2.5f);
        PosicionStar[4] = new Vector3(sb, s1, 2.5f);
        PosicionStar[5] = new Vector3(sb, s1, 2.5f);
        PosicionStar[6] = new Vector3(sb, s2, 2.5f);
        PosicionStar[7] = new Vector3(sb, s2, 2.5f);
        PosicionStar[8] = new Vector3(sb, s2, 2.5f);
        PosicionStar[9] = new Vector3(sb, s2, 2.5f);
        PosicionStar[10] = new Vector3(sb, s2, 2.5f);
        PosicionStar[11] = new Vector3(sb, s2, 2.5f);
        PosicionStar[12] = new Vector3(sb, s2, 2.5f);
        PosicionStar[13] = new Vector3(sb, s2, 2.5f);
        PosicionStar[14] = new Vector3(sb, s2, 2.5f);
        PosicionStar[15] = new Vector3(sb, s2, 2.5f);
        PosicionStar[16] = new Vector3(sb, s2, 2.5f);
        PosicionStar[17] = new Vector3(sb, s2, 2.5f);
        PosicionStar[18] = new Vector3(sc, s2, 2.5f);
        PosicionStar[19] = new Vector3(sc, s2, 2.5f);
        PosicionStar[20] = new Vector3(sc, s2, 2.5f);
        PosicionStar[21] = new Vector3(sc, s2, 2.5f);
        PosicionStar[22] = new Vector3(sc, s2, 2.5f);
        PosicionStar[23] = new Vector3(sc, s2, 2.5f);
        PosicionStar[24] = new Vector3(sa, s2, 2.5f);
        PosicionStar[25] = new Vector3(sa, s2, 2.5f);
        PosicionStar[26] = new Vector3(sa, s2, 2.5f);
        PosicionStar[27] = new Vector3(sa, s2, 2.5f);
        PosicionStar[28] = new Vector3(sa, s2, 2.5f);
        PosicionStar[29] = new Vector3(sa, s2, 2.5f);
    }
    public void GeneraStar(int i)
    {
        GameObject proy = Instantiate(proyectil, PosicionStar[i], transform.rotation);
        //proy.transform.localScale = new Vector3(proy.transform.localScale.x * Porcentaje, proy.transform.localScale.y * Porcentaje, proy.transform.localScale.z);
        proy.transform.localScale = proy.transform.localScale * Porcentaje;
        //proy.GetComponent<Fisica>().velocity();
    }
    public void AplicarVelocidad(float th)
    {
        GameObject premio = GameObject.FindGameObjectWithTag("star");
        if(premio != null)
            premio.GetComponent<Fisica>().Desplazar(th);
    }
}
