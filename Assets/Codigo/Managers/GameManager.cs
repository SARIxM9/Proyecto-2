using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Posicion del Jugador")]
    [SerializeField] private Vector3 PosicionInicial;
    [Header("Animaciones de muerte del jugador")]
    [SerializeField] private ParticleSystem Morir;


    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PosicionInicial = Jugador_Controlador.Instance.transform.position;
    }

    public void ResetearPosicion()
    {
        StartCoroutine(ResetearJugador());
    }

    public void CambiarPosicion(Vector3 posicion)
    {
        PosicionInicial = posicion;
    }

    public void SiguienteNivel()
    {
        StartCoroutine(PasarNivel());
    }

    IEnumerator ResetearJugador()
    {
        Jugador_Controlador.Instance.transform.localScale = Vector3.zero;
        Jugador_Controlador.Instance.rb.simulated = false;
        CanvasManager.Instance.Muerte.Play();
        Morir.Play();
        AudioManager.instance.ReproducirSFX(0);
        yield return new WaitForSeconds(0.3f);
        Jugador_Controlador.Instance.transform.position = PosicionInicial;
        Jugador_Controlador.Instance.transform.localScale = Vector3.one;
        Jugador_Controlador.Instance.rb.velocity = Vector2.zero;
        Jugador_Controlador.Instance.rb.simulated = true;
    }

    IEnumerator PasarNivel()
    {
        CanvasManager.Instance.Nivel.Play();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
