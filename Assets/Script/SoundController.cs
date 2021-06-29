using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    private AudioSource[] Audio = new AudioSource[4];
    private int Anterior = 1;
    public float tiempo = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        Audio = GetComponentsInChildren<AudioSource>();
        Audio[Anterior].Play();
        StartCoroutine("Reproducir");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Reproducir()
    {
        while (true)
        {
            if (Audio[Anterior].isPlaying == false)
            {
                int Nro = Random.Range(1, 5);
                Anterior = Nro;
                yield return new WaitForSeconds(tiempo);
                Audio[Nro].Play();
            }
            yield return null;
            
        }
    }
}
