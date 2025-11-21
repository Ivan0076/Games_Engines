using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoderesJugador : MonoBehaviour
{
    public bool tieneDash = false;
    public bool tieneInvisibilidad = false;
    public bool tieneIntangibilidad = false;

    public float duracionIntangibilidad = 3f;
    public float duracionInvisibilidad = 3f;

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
    private Material materialOriginalJugador;

    // Cache de materiales originales de paredes traspasables
    private readonly Dictionary<Renderer, Material> paredesOriginales = new Dictionary<Renderer, Material>();

    void Start()
    {
        playerRenderer = GetComponentInChildren<Renderer>();
        playerCollider = GetComponent<Collider>();
        if (playerRenderer != null)
            materialOriginalJugador = playerRenderer.material;
    }

    public void ActivarIntangibilidad()
    {
        if (tieneIntangibilidad && !intangibilidadActiva && puedeUsarIntangibilidad)
        {
            intangibilidadActiva = true;
            puedeUsarIntangibilidad = false;

            if (playerRenderer != null && materialTransparente != null)
                playerRenderer.material = materialTransparente;

            // Ilumina paredes y cachea material original de cada una
            foreach (var pared in GameObject.FindGameObjectsWithTag("ParedTraspasable"))
            {
                var rend = pared.GetComponent<Renderer>();
                if (rend == null) continue;

                if (!paredesOriginales.ContainsKey(rend))
                    paredesOriginales[rend] = rend.material;

                if (materialBrillante != null)
                    rend.material = materialBrillante;
            }

            StartCoroutine(DesactivarIntangibilidad());
        }
    }

    IEnumerator DesactivarIntangibilidad()
    {
        Debug.Log("Intangibilidad activada");
        yield return new WaitForSeconds(duracionIntangibilidad);

        intangibilidadActiva = false;

        // Restaurar material del jugador
        if (playerRenderer != null && materialOriginalJugador != null)
            playerRenderer.material = materialOriginalJugador;

        // Restaurar cada pared a su material original
        foreach (var kvp in paredesOriginales)
        {
            var rend = kvp.Key;
            var mat = kvp.Value;
            if (rend != null && mat != null)
                rend.material = mat;
        }
        paredesOriginales.Clear();

        Debug.Log("Intangibilidad terminada");

        // Cooldown y re-habilitación
        yield return new WaitForSeconds(cooldownIntangibilidad);
        puedeUsarIntangibilidad = true;
    }

    public void ActivarInvisibilidad()
    {
        if (tieneInvisibilidad && !invisibilidadActiva && puedeUsarInvisibilidad)
        {
            invisibilidadActiva = true;
            gameObject.tag = "PlayerInv";
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
        gameObject.tag = "Player";

        

        Debug.Log("Invisibilidad terminada");
        yield return new WaitForSeconds(cooldownInvisibilidad);
        puedeUsarInvisibilidad = true;
    }

    public void ReiniciarEstado()
    {
        tieneDash = false;
        tieneIntangibilidad = false;
        tieneInvisibilidad = false;
        poderSeleccionado = PoderActivo.Ninguno;

        intangibilidadActiva = false;
        invisibilidadActiva = false;
        puedeUsarIntangibilidad = true;
        puedeUsarInvisibilidad = true;

        if (playerRenderer != null)
        {
            playerRenderer.enabled = true;
            if (materialOriginalJugador != null)
                playerRenderer.material = materialOriginalJugador;
        }

        // Restaurar paredes por si quedó activado en mitad
        foreach (var kvp in paredesOriginales)
        {
            var rend = kvp.Key;
            var mat = kvp.Value;
            if (rend != null && mat != null)
                rend.material = mat;
        }
        paredesOriginales.Clear();

        gameObject.tag = "Player";

        var vida = GetComponent<Vida>();
        if (vida != null)
            vida.vidaActual = vida.vidaMaxima;

        Debug.Log("Estado del jugador reiniciado.");
    }
}
