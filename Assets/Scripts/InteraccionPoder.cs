using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;


public class InteraccionPoder : MonoBehaviour
{
    public InputActionAsset InputActions;
    private InputAction interactuar;


    public PoderActivo poder;
    public GameObject panelConfirmacion;
    public TMP_Text textoDescripcion; // Asigna el texto del panel desde el inspector
    public Button botonAceptar;
    public Button botonRechazar;

    private bool jugadorCerca = false;
    private bool yaInteractuado = false;
    private PoderesJugador jugador;

    void Awake()
    {
        interactuar = InputActions.FindAction("Interact");
    }

    void OnEnable()
    {
        InputActions.Enable();
    }

    void OnDisable()
    {
        InputActions.Disable();
    }


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

    void Update()
    {
        if (jugadorCerca && !yaInteractuado && interactuar.WasPressedThisFrame())
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
            case PoderActivo.Dash:
                return "¿Quieres obtener el poder de Dash? Te permitirá moverte rápidamente por el mapa.";
            case PoderActivo.Intangibilidad:
                return "¿Quieres ser intangible? Podrás atravesar ciertas paredes y evitar enemigos.";
            case PoderActivo.Invisibilidad:
                return "¿Quieres volverte invisible? Los enemigos no podrán verte por un tiempo.";
            default:
                return "¿Quieres aceptar este poder?";
        }
    }

    void AceptarPoder()
    {
        if (jugador == null)
        {
            Debug.LogWarning("No se encontró referencia al jugador.");
            return;
        }

        switch (poder)
        {
            case PoderActivo.Dash:
                jugador.tieneDash = true;
                break;
            case PoderActivo.Intangibilidad:
                jugador.tieneIntangibilidad = true;
                break;
            case PoderActivo.Invisibilidad:
                jugador.tieneInvisibilidad = true;
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