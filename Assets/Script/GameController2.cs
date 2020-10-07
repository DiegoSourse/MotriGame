using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController2 : MonoBehaviour
{
    public GameObject spawner;
    private SpawnTronco spawn;
    public GameObject gameRegion;
    public int estado = 0;
    public bool derecha = false;
    public bool izquierda = false;
    public bool salto= false;
    //public GameObject ent;
    //private Animator anim=new Animator();

    //

    private string[] poses = {  //sentado
                                "1,2,3,4,5,6,7,8,9,12,13,16",
                                "1,2,3,4,5,6,7,8,9,13,16",
                                "1,2,3,4,5,6,7,8,12,13,16",
                                //arrodillado
                                "1,2,3,4,5,8,9,12,13,16",
                                "1,2,3,4,5,9,12,13,16",
                                "1,2,3,4,8,9,12,13,16",
                                //parado centro
                                "1,4,5,8,9,12,13,16",
                                "1,4,5,8,9,12,13,14,16",
                                "1,4,5,8,9,12,13,15,16",
                                "5,8,9,12,13,16",
                                "5,8,9,12,13,14,16",
                                "5,8,9,12,13,15,16",
                                "4,5,8,9,12,13,16",
                                "4,5,8,9,12,13,14,16",
                                "4,5,8,9,12,13,15,16",
                                "1,5,8,9,12,13,16",
                                "1,5,8,9,12,13,14,16",
                                "1,5,8,9,12,13,15,16",
                                //parado derecha
                                "3,4,7,8,11,12,15,16",
                                "3,4,7,8,11,12,13,15,16",
                                "3,4,7,8,11,12,14,15,16",
                                "4,7,8,11,12,15,16",
                                "4,7,8,11,12,13,15,16",
                                "4,7,8,11,12,14,15,16",
                                //parado izquierda
                                "1,2,5,6,9,10,13,14",
                                "1,2,5,6,9,10,13,14,15",
                                "1,2,5,6,9,10,13,14,16",
                                "1,5,6,9,10,13,14",
                                "1,5,6,9,10,13,14,15",
                                "1,5,6,9,10,13,14,16"
    };
    public Animator anim;
    public Rigidbody rb;
    private void Awake()
    {
        spawn = GameObject.FindObjectOfType <SpawnTronco>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //if(Input.GetMouseButtonDown)
        //anim.SetBool("choca", op);
        //anim.
        Debug.Log("Estamos en el nivel " + EstadoJuego.estadoJuego.Getlevel());
        int NroPose = Random.Range(0, 30);
        Debug.Log("POSE: "+NroPose);
        int []pos = obtenerPose(NroPose);
        spawn.generaPoseProyectil(pos);
        spawn.aplicarVelocidad(1.5f);
    }
    

    // Update is called once per frame
    void Update()
    {
        //anim.SetTrigger("salto");
        anim.SetInteger("estado",estado);
        anim.SetBool("derecha",derecha);
        anim.SetBool("izquierda", izquierda);
        if (salto)
        {
            rb.AddForce(Vector3.up * 800, ForceMode.Force);
            //rb.AddForce(Vector3.right * 100, ForceMode.Force);
            anim.SetFloat("salto", rb.transform.position.y);
            
        }
        anim.SetFloat("salto", rb.transform.position.y);
        
    }
    void FixedUpdate()
    {
        
    }
    public int[] obtenerPose(int i)
    {
        string[] pose = poses[i].Split(',');
        int[] ints = System.Array.ConvertAll(pose, int.Parse);
        return ints;
    }
}
