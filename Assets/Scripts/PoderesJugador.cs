using UnityEngine;
using System.Collections;

public class PoderesJugador : MonoBehaviour
{
    public bool tieneDash = false;
    public bool tieneInvisibilidad = false;
    public bool tieneIntangibilidad = false;

    public float duracionIntangibilidad = 5f;
    public float duracionInvisibilidad = 5f;

    public float cooldownIntangibilidad = 5f;
    private bool puedeUsarIntangibilidad = true;

    public float cooldownInvisibilidad = 5f;
    private bool puedeUsarInvisibilidad = true;

    public PoderActivo poderSeleccionado = PoderActivo.Ninguno;

    private Renderer playerRenderer;
    private Collider playerCollider;

    private bool intangibilidadActiva = false;
    public bool IntangibilidadActiva => intangibilidadActiva;

    private bool invisibilidadActiva = false;

    public Material materialTransparente;
    public Material materialBrillante;
    private Material materialOriginal;


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
        if (!intangibilidadActiva && puedeUsarIntangibilidad)
        {
            intangibilidadActiva = true;
            puedeUsarIntangibilidad = false;

            if (playerRenderer != null)
            {
                materialOriginal = playerRenderer.material;
                playerRenderer.material = materialTransparente;
            }

            GameObject[] paredes = GameObject.FindGameObjectsWithTag("ParedTraspasable");
            foreach (GameObject pared in paredes)
            {
                Renderer paredRenderer = pared.GetComponent<Renderer>();
                if (paredRenderer != null)
                    paredRenderer.material = materialBrillante;
            }

            StartCoroutine(DesactivarIntangibilidad());
        }
    }

    IEnumerator DesactivarIntangibilidad()
    {
        Debug.Log("Intangibilidad activada");
        yield return new WaitForSeconds(duracionIntangibilidad);

        intangibilidadActiva = false;

        if (playerRenderer != null && materialOriginal != null)
            playerRenderer.material = materialOriginal;

        GameObject[] paredes = GameObject.FindGameObjectsWithTag("ParedTraspasable");
        foreach (GameObject pared in paredes)
        {
            Renderer paredRenderer = pared.GetComponent<Renderer>();
            if (paredRenderer != null)
                paredRenderer.material = materialOriginal; // o guarda el original por separado si es distinto
        }

        Debug.Log("Intangibilidad terminada");
    }


    public void ActivarInvisibilidad()
    {
        if (!invisibilidadActiva && puedeUsarInvisibilidad)
        {
            invisibilidadActiva = true;
            puedeUsarInvisibilidad = false;

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
        yield return new WaitForSeconds(cooldownInvisibilidad);
        puedeUsarInvisibilidad = true;
    }
}
