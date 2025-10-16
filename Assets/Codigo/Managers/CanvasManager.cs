using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [Header("Animacion de muerte")]
    public Animation Muerte;
    [Header("Animaciones de nivel")]
    public Animation Nivel;
    [Header("Paneles")]
    [SerializeField] private GameObject Menu;
    [SerializeField] private GameObject Interfaz;
    [SerializeField] private GameObject MenuPausa;
    [SerializeField] private GameObject MenuOpciones;
    [SerializeField] private GameObject FIN;
    [SerializeField] private GameObject System;
    [Header("Paneles Menu niveles")]
    [SerializeField] private GameObject MenuPanel;
    [SerializeField] private GameObject Niveles;
    [Header("LeenTween Menu")]
    [SerializeField] private GameObject Titulo;
    [SerializeField] private GameObject Boton1;
    [SerializeField] private GameObject Boton2;
    [SerializeField] private GameObject Boton3;
    [Header("Musica y SFX")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider Musica;
    [SerializeField] private Slider SFX;
    [Header("Panel Activar")]
    [SerializeField] private string OpcionesVariable;
    [Header("LeenTween")]
    [SerializeField] private GameObject Fondo;
    [SerializeField] private LeanTweenType type;
    [SerializeField] private LeanTweenType type2;
    [SerializeField] private LeanTweenType type3;
    [Header("Niveles")]
    [SerializeField] private Button[] NivelBoton;


    public static CanvasManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        NivelesInicio();
    }

    private void Start()
    {
        AnimacionMenu();

        if (PlayerPrefs.HasKey("Musica"))
        {
            Musica.value = PlayerPrefs.GetFloat("Musica");
        }
        if(PlayerPrefs.HasKey("SFX"))
        {
            SFX.value = PlayerPrefs.GetFloat("SFX");
        }
    }

    //Interfaz
    public void Pausar()
    {
        StartCoroutine(AnimacionAbrirPausa());
    }

    //Menu Pausa

    public void Reanudar()
    {
        StartCoroutine(AnimacionCerrarPausa());
    }

    public void OpcionesPausa()
    {
        OpcionesVariable = "Pausa";
        StartCoroutine(AnimacionAbrirOpciones(MenuPausa));
    }

    public void SalirMenuPausa()
    {
        StartCoroutine(SiguienteNivel(MenuPausa, Menu, "Menu", true));
        Time.timeScale = 1;
    }


    //Menu Opciones

    public void SliderMusica()
    {
        audioMixer.SetFloat("Musica", Mathf.Log10(Musica.value) * 20);
        PlayerPrefs.SetFloat("Musica", Musica.value);
    }

    public void SliderSFX()
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(SFX.value) * 20);
        PlayerPrefs.SetFloat("SFX", SFX.value);
    }

    public void OpcionesSalir()
    {
        if(OpcionesVariable == "Menu")
        {
            StartCoroutine(AnimacionCerrarOpciones(Menu));
        }
        else if(OpcionesVariable == "Pausa")
        {
            StartCoroutine(AnimacionCerrarOpciones(MenuPausa));
        }
    }

    //Menu Juego

    public void Jugar()
    {
        StartCoroutine(AnimacionAbrirNiveles());
        NivelesInicio();
    }

    public void Opciones()
    {
        OpcionesVariable = "Menu";
        StartCoroutine(AnimacionAbrirOpciones(Menu));
    }

    public void SalirJuego()
    {
        Application.Quit();
    }

    //Corrutinas

    IEnumerator SiguienteNivel(GameObject PanelDesactivar,GameObject PanelActivar,string nivel,bool Menu)
    {
        if (Menu)
        {
            System.SetActive(false);
            Nivel.Play();
            yield return new WaitForSeconds(0.5f);
            PanelDesactivar.SetActive(false);
            PanelActivar.SetActive(true);
            System.SetActive(true);
            SceneManager.LoadScene(nivel);
            yield return new WaitForSeconds(0.2f);
            AnimacionMenu();
        }
        else
        {
            System.SetActive(false);
            Nivel.Play();
            yield return new WaitForSeconds(0.5f);
            PanelDesactivar.SetActive(false);
            PanelActivar.SetActive(true);
            System.SetActive(true);
            SceneManager.LoadScene(nivel);
        }
        
    }

    //LeenTween

    //Abrir Pausa y cerrar

    IEnumerator AnimacionAbrirPausa()
    {
        System.SetActive(false);
        Interfaz.SetActive(false);
        MenuPausa.SetActive(true);
        Time.timeScale = 0;
        LeanTween.reset();
        Fondo.transform.position = new Vector2(-1500,540);
        LeanTween.moveX(Fondo,960,0.5f).setEase(type).setIgnoreTimeScale(true);
        yield return new WaitForSecondsRealtime(0.5f);
        System.SetActive(true);

    }

    IEnumerator AnimacionCerrarPausa()
    {
        System.SetActive(false);
        LeanTween.moveX(Fondo, 2880, 0.5f).setEase(type2).setIgnoreTimeScale(true);
        yield return new WaitForSecondsRealtime(0.5f);
        MenuPausa.SetActive(false);
        Interfaz.SetActive(true);
        Time.timeScale = 1;
        System.SetActive(true);
    }

    //Abrir Opciones y cerrar

    IEnumerator AnimacionAbrirOpciones(GameObject Desactivar)
    {
        System.SetActive(false);
        MenuOpciones.SetActive(true);
        LeanTween.reset();
        MenuOpciones.transform.position = new Vector2(960, 1100);
        LeanTween.moveY(MenuOpciones, 540, 0.5f).setEase(type).setIgnoreTimeScale(true);
        yield return new WaitForSecondsRealtime(0.5f);
        Desactivar.SetActive(false);
        System.SetActive(true);
    }

    IEnumerator AnimacionCerrarOpciones(GameObject Activar)
    {
        System.SetActive(false);
        Activar.SetActive(true);
        LeanTween.moveY(MenuOpciones, -600, 0.5f).setEase(type2).setIgnoreTimeScale(true);
        yield return new WaitForSecondsRealtime(0.5f);
        MenuOpciones.SetActive(false);
        System.SetActive(true);
    }

    //Animacion Menu

    IEnumerator MenuIE()
    {
        //Resetear
        LeanTween.reset();
        Titulo.transform.localScale = Vector3.zero;
        Boton1.transform.position = new Vector2(-500,650);
        Boton2.transform.position = new Vector2(2500,450);
        Boton3.transform.position = new Vector2(-500,250);

        //Siguiente
        System.SetActive(false);
        LeanTween.scale(Titulo, Vector3.one * 1.5f, 1.5f).setEase(type3);
        LeanTween.moveX(Boton1, 960, 1.5f).setEase(type);
        LeanTween.moveX(Boton2, 960, 1.5f).setEase(type);
        LeanTween.moveX(Boton3, 960, 1.5f).setEase(type);
        yield return new WaitForSeconds(1.5f);
        System.SetActive(true);
    }

    public void AnimacionMenu()
    {
        StartCoroutine(MenuIE());
    }

    //Botones y Menu

    public void Boton(string nivel)
    {
        StartCoroutine(SiguienteNivel(Menu, Interfaz, "Nivel " + nivel, false));
        StartCoroutine(AnimacionCerrarNiveles());
    }

    IEnumerator AnimacionAbrirNiveles()
    {
        System.SetActive(false);
        MenuPanel.SetActive(false);
        Niveles.SetActive(true);

        Niveles.transform.localScale = Vector3.zero;
        LeanTween.scale(Niveles, Vector3.one, 1.5f).setEase(type3);
        yield return new WaitForSeconds(1.5f);

        System.SetActive(true);
    }

    IEnumerator AnimacionCerrarNiveles()
    {
        yield return new WaitForSeconds(1.5f);
        MenuPanel.SetActive(true);
        Niveles.SetActive(false);
    }


    //Niveles

    public void NivelesInicio()
    {
        int Desbloquear = PlayerPrefs.GetInt("Desbloquear",1);

        for (int i = 0; i < NivelBoton.Length; i++)
        {
            NivelBoton[i].interactable = false;
        }

        for (int i = 0; i < Desbloquear; i++)
        {
            NivelBoton[i].interactable = true;
        }
    }


    //FIN

    public void BanderaFIN()
    {
        StartCoroutine(SiguienteNivel(Interfaz,FIN, "Final", false));
    }

    public void SALIR()
    {
        StartCoroutine(SiguienteNivel(FIN,Menu, "Menu", true));
    }
}
