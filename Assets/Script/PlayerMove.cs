using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Transform vrCamera;
    public float toggleAngle = 30.0f;
    public float speed = 3.0f;
    public bool moveForward;
    private CharacterController cc;
    public GameObject flecha;
    private bool WithControl;
    // Use this for initialization
    void Start()
    {
        WithControl = EstadoJuego.estadoJuego.Mando;
        cc = GetComponent<CharacterController>();
    }
    // Update is called once per frame
    void Update()
    {
        if(WithControl == true)
        {
            Vector3 forward = vrCamera.TransformDirection(Vector3.forward);
            float curSpeed = speed * Input.GetAxis("Vertical");
            cc.SimpleMove(forward * curSpeed);
        }
        else
        {
            if (vrCamera.eulerAngles.x >= toggleAngle && vrCamera.eulerAngles.x < 90.0f)
            {
                moveForward = true;
                //flecha.SetActive(true);
            }
            else
            {
                moveForward = false;
                //flecha.SetActive(false);
            }
        }
    }
    private void LateUpdate()
    {
        if (moveForward)
        {
            vrCamera.eulerAngles = new Vector3(toggleAngle, vrCamera.eulerAngles.y, vrCamera.eulerAngles.z);
            Vector3 forward = vrCamera.TransformDirection(Vector3.forward);
            cc.SimpleMove(forward * speed);
        }

    }
}