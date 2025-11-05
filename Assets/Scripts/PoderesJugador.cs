using UnityEngine;
using System.Collections;


public class PoderesJugador : MonoBehaviour
{ 
    public enum PoderActivo
    {
        Ninguno,
        Dash,
        Invisibilidad,
        Intangibilidad
    }

    public PoderActivo poderSeleccionado = PoderActivo.Ninguno;
    public bool tieneDash = false;
    public bool esIntangible = false;
    public bool esInvisible = false;

    public float duracionIntangibilidad = 5f;
    public float duracionInvisibilidad = 5f;

    private Renderer playerRenderer;
    private Collider playerCollider;
    void Update()
    {
        // Selección de poder
        if (Input.GetKeyDown(KeyCode.Alpha1))
            poderSeleccionado = PoderActivo.Dash;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            poderSeleccionado = PoderActivo.Invisibilidad;
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            poderSeleccionado = PoderActivo.Intangibilidad;

        // Activación del poder seleccionado
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (poderSeleccionado)
            {
                case PoderActivo.Dash:
                    ActivarDash();
                    break;
                case PoderActivo.Invisibilidad:
                    ActivarInvisibilidad();
                    break;
                case PoderActivo.Intangibilidad:
                    ActivarIntangibilidad();
                    break;
            }
        }
    }

    void Start()
    {
        playerRenderer = GetComponentInChildren<Renderer>();
        playerCollider = GetComponent<Collider>();
    }

    public void ActivarDash()
    {
        tieneDash = true;
        // Aquí puedes activar una animación 
    }

    public void ActivarIntangibilidad()
    {
        esIntangible = true;
        StartCoroutine(DesactivarIntangibilidad());
    }

    IEnumerator DesactivarIntangibilidad()
    {
        yield return new WaitForSeconds(duracionIntangibilidad);
        esIntangible = false;
    }

    public void ActivarInvisibilidad()
    {
        esInvisible = true;
        if (playerRenderer != null)
            playerRenderer.enabled = false;

        StartCoroutine(DesactivarInvisibilidad());
    }

    IEnumerator DesactivarInvisibilidad()
    {
        yield return new WaitForSeconds(duracionInvisibilidad);
        esInvisible = false;
        if (playerRenderer != null)
            playerRenderer.enabled = true;
    }
}
