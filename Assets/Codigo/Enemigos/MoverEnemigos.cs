using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverEnemigos : MonoBehaviour
{
    [SerializeField] private Transform[] Puntos;
    private int indice;
    [SerializeField] private float velocidad;
    [SerializeField] private float esperar;
    private float esperarVariable;

    private void Start()
    {
        esperarVariable = esperar;
    }

    void Update()
    {
        Mover();
        Cambiar();
    }

    public void Mover()
    {
        transform.position = Vector2.MoveTowards(transform.position, Puntos[indice].position,velocidad * Time.deltaTime);
    }

    public void Cambiar()
    {
        if (Vector2.Distance(transform.position, Puntos[indice].position) < 0.1f)
        {
            if (esperarVariable <= 0)
            {
                indice++;
                esperarVariable = esperar;
                if (indice == Puntos.Length)
                {
                    indice = 0;
                }
            }
            else
            {
                esperarVariable -= Time.deltaTime;
            }
            
        }
    }
}
