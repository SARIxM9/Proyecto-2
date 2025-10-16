using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador_Controlador : MonoBehaviour
{
    [Header("Atributos del jugador")]
    public Rigidbody2D rb;
    [SerializeField] private float direccion;
    [SerializeField] private float velocidad;
    [Header("Cambiar direccion")]
    [SerializeField] private Transform ParedTrasnform;
    [SerializeField] private LayerMask layer;
    [Header("Condiciones")]
    public bool paredBool;
    public bool TocaPiso;
    [Header("Particulas")]
    [SerializeField] private ParticleSystem PisoParticle;
    [SerializeField] private ParticleSystem ParedParticle;
    [Header("Plataforma")]
    public Rigidbody2D RBplataforma;
    public bool plataforma;

    public static Jugador_Controlador Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ParedParticle.transform.parent = null;
    }

    void Update()
    {
        Movimiento();
        Piso();
    }

    private void Movimiento()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (plataforma)
            {
                rb.velocity = new Vector2(direccion * velocidad + RBplataforma.velocity.x, rb.velocity.y);

            }
            else
            {
                rb.velocity = new Vector2(direccion * velocidad, rb.velocity.y);
            }
        }
    }

    private void Piso()
    {
        paredBool = Physics2D.OverlapBox(ParedTrasnform.position, new Vector2(0.04f, 0.82f), 0,layer);
        if (paredBool)
        {
            ParedParticula(ParedTrasnform);
            direccion = -direccion;
            transform.eulerAngles = new Vector2(0f, transform.eulerAngles.y + 180);
        }
    }

    private void ParedParticula(Transform posicion)
    {
        ParedParticle.transform.position = posicion.position;
        ParedParticle.Play();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Palette")
        {
            PisoParticle.Play();
            TocaPiso = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Palette")
        {
            TocaPiso = false;
        }
    }
}
