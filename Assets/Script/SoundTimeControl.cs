using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundTimeControl : MonoBehaviour
{
    public TMPro.TextMeshPro texto;
    public AudioClip timer;
    public AudioClip wargin;
    private AudioSource source;
    // Start is called before the first frame update
    string tiempo;
    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
        tiempo = "00:00";
    }

    // Update is called once per frame
    void Update()
    {
        if(texto.text != tiempo)
        {
            tiempo = texto.text;
            string[] time = tiempo.Split(':');
            int min = Convert.ToInt32(time[0]);
            int sec = Convert.ToInt32(time[1]);
            if (min == 0 && sec <=10)
            {
                source.PlayOneShot(wargin);
            }
            else
                source.PlayOneShot(timer);
        }
    }
}
