using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particula : MonoBehaviour
{
    [Header("Particula de Movimiento")]
    [SerializeField] private ParticleSystem particle;
    [SerializeField,Range(0,10)] private int velocidad;
    [SerializeField,Range(0f,0.2f)] private float periodo;
    [SerializeField] private Rigidbody2D JugadorRB;
    private float contador;

    private void Update()
    {
        contador += Time.deltaTime;
        if (Mathf.Abs(JugadorRB.velocity.x) > velocidad && Jugador_Controlador.Instance.TocaPiso)
        {
            if(contador > periodo)
            {
                particle.Play();
                contador = 0;
            }
        }
    }
}
