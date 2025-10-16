using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandera : MonoBehaviour
{
    [SerializeField] private Collider2D BanderaColider;
    [SerializeField] private SpriteRenderer[] sprite;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameManager.instance.CambiarPosicion(transform.position);
            BanderaColider.enabled = false;
            sprite[0].enabled = false;
            sprite[1].enabled = true;
        }
    }
}
