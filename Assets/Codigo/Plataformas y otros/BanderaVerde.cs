using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BanderaVerde : MonoBehaviour
{
    [SerializeField] private bool FIN;
    [SerializeField] private bool Especial;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (FIN)
            {
                CanvasManager.Instance.BanderaFIN();
                PlayerPrefs.DeleteAll();
                AudioManager.instance.ReproducirSFX(1);
            }
            else if (Especial)
            {
                GameManager.instance.SiguienteNivel();
                AudioManager.instance.ReproducirSFX(1);
            }
            else
            {
                DesbloquearNivel();
                GameManager.instance.SiguienteNivel();
                AudioManager.instance.ReproducirSFX(1);
            }
        }
    }

    public void DesbloquearNivel()
    {
        if(SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("NivelIndex"))
        {
            PlayerPrefs.SetInt("NivelIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("Desbloquear", PlayerPrefs.GetInt("Desbloquear",1) + 1);
            PlayerPrefs.Save();
        }
    }
}
