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
    private Vida vidaActual;

    void Awake()
    {
        interactuar = InputActions.FindAction("Interact");
        interactuar.Enable(); // ← Asegura que esté activa
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
        vidaActual = other.GetComponent<Vida>();

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
        Time.timeScale = 0;
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
                return "En este mundo puedes hacer ciertas cosas que no puedes hacer en tu mundo. Por ejemplo: tu alma aqui es mas liviana, asi que puedes ir mas rapido. ";
            case PoderActivo.Intangibilidad:
                return "Tu cuerpo fisico no te acompaña del todo en este mundo, puedes utilizar esto para tu ventaja cruzando ciertas barreras o paredes.";
            case PoderActivo.Invisibilidad:
                return "El tiempo pasa distinto por aqui, si te concentras en eso los enemigos no podrán verte por un tiempo...";
            default:
                return "¿Crees poder usar este poder?";
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

        if (vidaActual != null)
        {
            vidaActual.vidaActual -= 5;
            Debug.Log("Vida reducida por aceptar poder. Vida actual: " + vidaActual.vidaActual);
        }


        FinalizarInteraccion();
    }



    void RechazarPoder()
    {
        FinalizarInteraccion();
    }

    void FinalizarInteraccion()
    {
        Time.timeScale = 1;
        yaInteractuado = true;
        panelConfirmacion.SetActive(false);
        botonAceptar.onClick.RemoveListener(AceptarPoder);
        botonRechazar.onClick.RemoveListener(RechazarPoder);
    }
}