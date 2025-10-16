using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb; 
    [SerializeField] private Transform[] Puntos; 
    [SerializeField] private Vector3 Direccion; 
    [SerializeField] private int indice;
    [SerializeField] private float velocidad;

    private void Start()
    {
        DireccionPlataforma();
    }

    void Update()
    {
        CambiarPunto();
    }

    private void FixedUpdate()
    {
        MoverPlataforma();
    }

    private void MoverPlataforma()
    {
        rb.velocity = Direccion * velocidad;
    }
    
    private void DireccionPlataforma()
    {
        Direccion = (Puntos[indice].position - transform.position).normalized;
    }

    private void CambiarPunto()
    {
        if (Vector2.Distance(rb.position, Puntos[indice].position) < 0.1f)
        {
            indice++;
            if (indice >= Puntos.Length)
            {
                indice = 0;
            }
            if(indice < Puntos.Length)
            {
                DireccionPlataforma();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Jugador_Controlador.Instance.RBplataforma = rb;
            Jugador_Controlador.Instance.plataforma = true;
            Jugador_Controlador.Instance.rb.gravityScale = 10;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Jugador_Controlador.Instance.plataforma = false;
            Jugador_Controlador.Instance.rb.gravityScale = 4;
        }
    }
}
