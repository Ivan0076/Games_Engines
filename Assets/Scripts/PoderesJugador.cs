using UnityEngine;
using System.Collections;

public class PoderesJugador : MonoBehaviour
{
    public bool tieneDash = false;
    public bool tieneInvisibilidad = false;
    public bool tieneIntangibilidad = false;

    public float duracionIntangibilidad = 5f;
    public float duracionInvisibilidad = 5f;

    public PoderActivo poderSeleccionado = PoderActivo.Ninguno;

    private Renderer playerRenderer;
    private Collider playerCollider;

    private bool intangibilidadActiva = false;
    private bool invisibilidadActiva = false;

    void Start()
    {
        playerRenderer = GetComponentInChildren<Renderer>();
        playerCollider = GetComponent<Collider>();
    }

    public void ActivarDash()
    {
        Debug.Log("Dash activado");
        // El dash se activa desde Movimiento.cs con fuerza física
    }

    public void ActivarIntangibilidad()
    {
        if (!intangibilidadActiva)
        {
            intangibilidadActiva = true;
            StartCoroutine(DesactivarIntangibilidad());
        }
    }

    IEnumerator DesactivarIntangibilidad()
    {
        Debug.Log("Intangibilidad activada");
        yield return new WaitForSeconds(duracionIntangibilidad);
        intangibilidadActiva = false;
        Debug.Log("Intangibilidad terminada");
    }

    public void ActivarInvisibilidad()
    {
        if (!invisibilidadActiva)
        {
            invisibilidadActiva = true;
            if (playerRenderer != null)
                playerRenderer.enabled = false;

            StartCoroutine(DesactivarInvisibilidad());
        }
    }

    IEnumerator DesactivarInvisibilidad()
    {
        Debug.Log("Invisibilidad activada");
        yield return new WaitForSeconds(duracionInvisibilidad);
        invisibilidadActiva = false;
        if (playerRenderer != null)
            playerRenderer.enabled = true;
        Debug.Log("Invisibilidad terminada");
    }
}
