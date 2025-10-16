using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]private AudioSource[] Musica;
    [SerializeField]private AudioSource[] SFX;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Musica[0].Play();
    }

    public void ReproducirSFX(int numero)
    {
        SFX[numero].Play();
    }
}
