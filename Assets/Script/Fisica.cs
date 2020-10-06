using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fisica : MonoBehaviour
{
    public float thrust=1;
    public Rigidbody rb;
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        //rb.AddForce(Vector3.back*thrust, ForceMode.VelocityChange);
        //rb.velocity = Vector3.back*thrust;
    }

    void FixedUpdate()
    {
        //rb.AddForce(Vector3.back*thrust,ForceMode.Force);
        //rb.AddForce(Vector3.back, ForceMode.VelocityChange);
    }
    public void velocity(float vel)
    {
        rb = GetComponent<Rigidbody>();
        //rb.AddForce(Vector3.back*thrust, ForceMode.VelocityChange);
        thrust = vel;
        rb.velocity = Vector3.back * thrust;
    }
}
