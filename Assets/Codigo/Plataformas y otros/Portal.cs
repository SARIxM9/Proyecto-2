using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [Header("Posicion del otro portal")]
    [SerializeField] private Transform Destino;
    [Header("Animacion del jugador entrando y saliendo")]
    [SerializeField] private Animation Jugador;
    [Header("Fuerzas")]
    [SerializeField] private float DistanciaMinima;
    [SerializeField] private float VelocidadEntrar;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(Vector2.Distance(transform.position,collision.transform.position) > DistanciaMinima)
            {
                StartCoroutine(Teletransportacion());
            }
        }
    }

    IEnumerator Teletransportacion()
    {
        Jugador.Play("Inicio"); 
        Jugador_Controlador.Instance.rb.simulated = false;
        StartCoroutine(Movimiento());
        yield return new WaitForSeconds(0.5f);
        Jugador_Controlador.Instance.transform.position = new Vector2(Destino.position.x, Destino.position.y);
        Jugador.Play("Final");
        yield return new WaitForSeconds(0.5f);
        Jugador_Controlador.Instance.rb.simulated = true;
        Jugador_Controlador.Instance.rb.velocity = Vector2.zero;
    }

    IEnumerator Movimiento()
    {
        float timer = 0;
        while (timer < 0.5f)
        {
            Jugador_Controlador.Instance.transform.position = Vector2.MoveTowards(Jugador_Controlador.Instance.transform.position, transform.position, VelocidadEntrar * Time.deltaTime);
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;

        }
    }

}
