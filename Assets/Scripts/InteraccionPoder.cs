using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteraccionPoder : MonoBehaviour
{
    public TipoPoder poder;
    public GameObject panelConfirmacion;
    public TMP_Text textoDescripcion; // Asigna el texto del panel desde el inspector
    public Button botonAceptar;
    public Button botonRechazar;

    private bool jugadorCerca = false;
    private bool yaInteractuado = false;
    private PoderesJugador jugador;

    private void OnTriggerEnter(Collider other)
    {
        if (yaInteractuado) return;

        jugador = other.GetComponent<PoderesJugador>();
        if (jugador != null)
        {
            jugadorCerca = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PoderesJugador>() != null)
        {
            jugadorCerca = false;
        }
    }

    private void Update()
    {
        if (jugadorCerca && !yaInteractuado && Input.GetKeyDown(KeyCode.E))
        {
            MostrarPanel();
        }
    }

    void MostrarPanel()
    {
        panelConfirmacion.SetActive(true);
        textoDescripcion.text = ObtenerTextoPoder();

        botonAceptar.onClick.AddListener(AceptarPoder);
        botonRechazar.onClick.AddListener(RechazarPoder);
    }

    string ObtenerTextoPoder()
    {
        switch (poder)
        {
            case TipoPoder.Dash:
                return "¿Quieres obtener el poder de Dash? Te permitirá moverte rápidamente por el mapa.";
            case TipoPoder.Intangibilidad:
                return "¿Quieres ser intangible? Podrás atravesar ciertas paredes y evitar enemigos.";
            case TipoPoder.Invisibilidad:
                return "¿Quieres volverte invisible? Los enemigos no podrán verte por un tiempo.";
            default:
                return "¿Quieres aceptar este poder?";
        }
    }

    void AceptarPoder()
    {
        switch (poder)
        {
            case TipoPoder.Dash:
                jugador.ActivarDash();
                break;
            case TipoPoder.Intangibilidad:
                jugador.ActivarIntangibilidad();
                break;
            case TipoPoder.Invisibilidad:
                jugador.ActivarInvisibilidad();
                break;
        }

        FinalizarInteraccion();
    }

    void RechazarPoder()
    {
        FinalizarInteraccion();
    }

    void FinalizarInteraccion()
    {
        yaInteractuado = true;
        panelConfirmacion.SetActive(false);
        botonAceptar.onClick.RemoveListener(AceptarPoder);
        botonRechazar.onClick.RemoveListener(RechazarPoder);
    }
}