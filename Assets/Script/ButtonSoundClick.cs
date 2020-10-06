using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonSoundClick : MonoBehaviour
{
    public AudioSource source { get { return GetComponent<AudioSource>(); } }
    public Button btn { get { return GetComponent<Button>(); } }
    public AudioClip clip;

    void Start()
    {
        gameObject.AddComponent<AudioSource>();
        btn.onClick.AddListener(PlaySound);
    }
    void PlaySound()
    {
        source.PlayOneShot(clip);
    }
}
