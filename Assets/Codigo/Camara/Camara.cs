using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    [Header("Transform del jugador")]
    [SerializeField] private Transform jugador;
    [Header("Referencia para el SmoothDamp")]
    [SerializeField] private Vector3 Referencia;
    [Header("Ajustes")]
    [SerializeField] private Vector3 AjustarPosicion;
    [SerializeField] private Vector2 LimitesX;
    [SerializeField] private Vector2 LimitesY;
    [Header("Retraso de la Camara")]
    [SerializeField, Range(0f, 2f)] private float retraso;

    void Update()
    {
        SeguirJugador();
    }

    private void SeguirJugador()
    {
        Vector3 Ajustes = jugador.position + AjustarPosicion;
        Ajustes = new Vector3(Mathf.Clamp(Ajustes.x, LimitesX.x, LimitesX.y), Mathf.Clamp(Ajustes.y, LimitesY.x, LimitesY.y), -10f);
        transform.position = Vector3.SmoothDamp(transform.position, Ajustes, ref Referencia, retraso);
    }
}
