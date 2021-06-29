using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove2 : MonoBehaviour
{
    public Transform vrCamera;
    //
    private bool Arriba, Abajo;
    //
    /*public float LimiteSentado { get; set; }
    public float LimiteArrodillado { get; set; }*/
    //
    private bool Derecha, Izquierda;
    //
    public float DestinoX { get; set; }
    public float DestinoY { get; set; }
    //
    public int opcion { get; set; }
    public float AnguloDerIzq = 30.0f;
    private float AnguloAux;
    public float AnguloUpDown = 30.0f;
    private float AnguloUDAux;
    //
    public float speed = 2.0f;
    //
    private Vector3 desplazamiento;
    private float x, y;
    private float Estatura;
    //
    public bool llegada { get; set; }
    public bool volver { get; set; }
    //private CapsuleCollider cap
    // Use this for initialization
    void Start()
    {
        //cc = GetComponent<CharacterController>();
        //cap = GetComponent<CapsuleCollider>();
        AnguloAux = 360 - AnguloDerIzq;
        AnguloUDAux = 360 - AnguloUpDown;
        //AnguloUDAux = 360 - 30;
        Estatura = transform.position.y;
        volver = false;
    }
    // Update is called once per frame
    void Update()
    {
        switch (opcion)
        {
            case 1:
                //if (vrCamera.eulerAngles.z >= AnguloDerIzq && vrCamera.eulerAngles.z <= 30f)
                if (vrCamera.eulerAngles.z <= AnguloAux && vrCamera.eulerAngles.z >= 330f)
                {
                    Derecha = true;
                    Izquierda = false;
                    Arriba = false;
                    Abajo = false;
                    //Debug.Log("DERECHA");
                }
                break;
            case 2:
                //if (vrCamera.eulerAngles.z <= AnguloAux && vrCamera.eulerAngles.z >= 330f)
                if (vrCamera.eulerAngles.z >= AnguloDerIzq && vrCamera.eulerAngles.z <= 30f)
                    {
                    Izquierda = true;
                    Derecha = false;
                    Arriba = false;
                    Abajo = false;
                    //Debug.Log("IZQUIERDA");
                }
                break;
            case 3:
                if (vrCamera.eulerAngles.x <= 340f && vrCamera.eulerAngles.x >= AnguloUDAux)
                {
                    Arriba = true;
                    Abajo = false;
                    Derecha = false;
                    Izquierda = false;
                    //Debug.Log("ARRIBA");
                }
                break;
            case 4:
                if (vrCamera.eulerAngles.x >= AnguloUpDown && vrCamera.eulerAngles.x <= 70f)
                {
                    Abajo = true;
                    Arriba = false;
                    Derecha = false;
                    Izquierda = false;
                    //Debug.Log("ABAJO");
                }
                break;
            default:
                Abajo = false;
                Arriba = false;
                Derecha = false;
                Izquierda = false;
                break;
        }
    }
    private void LateUpdate()
    {
        if (Derecha == true)
        {
            desplazamiento = vrCamera.TransformDirection(Vector3.right);
            x = desplazamiento.x * Time.deltaTime * speed;
            //Debug.Log(transform.position.z);
            if (transform.position.x <= DestinoX)
            {
                transform.Translate(x, 0, 0);
                llegada = false;
            }
            else
            {
                llegada = true;
                //donde he llegado es distinto de cero?
                if (DestinoX != 0.0f) { volver = true; }
                else volver = false;
                //
                Derecha = false;
                opcion = 2;
                DestinoX = 0.0f;
                //Izquierda = true;
            }
        }
        if (Izquierda == true)
        {
            desplazamiento = vrCamera.TransformDirection(Vector3.left);
            x = desplazamiento.x * Time.deltaTime * speed;
            //Debug.Log(transform.position.z);
            if (transform.position.x >= DestinoX)
            {
                transform.Translate(x, 0, 0);
                llegada = false;
            }
            else
            {
                llegada = true;
                //donde llegue es distinto de cero?
                if (DestinoX != 0) { volver = true; }
                else volver = false;
                Izquierda = false;
                opcion = 1;
                DestinoX = 0.0f;
                //Derecha = true;
            }
        }
        if (Arriba == true)
        {
            desplazamiento = vrCamera.TransformDirection(Vector3.up);
            y = desplazamiento.y * Time.deltaTime * speed;
            //Debug.Log(transform.position.z);
            if (transform.position.y <= Estatura)
            {
                transform.Translate(0, y, 0);
                llegada = false;
            }
            else
            {
                llegada = true;
                Arriba = false;
                opcion = 0;
                volver = false;
            }
        }
        if (Abajo == true)
        {
            desplazamiento = vrCamera.TransformDirection(Vector3.down);
            y = desplazamiento.y * Time.deltaTime * speed;
            //Debug.Log(transform.position.z);
            if (transform.position.y >= DestinoY)
            {
                transform.Translate(0, y, 0);
                llegada = false;
            }
            else
            {
                llegada = true;
                Abajo = false;
                opcion = 3;
                volver = true;
                //Arriba = true;
            }
        }
    }
}