using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PuertaDesbloqueable : MonoBehaviour
{
    private bool jugadorCerca = false;
    private bool puertaAbierta = false;
    private JugadorLlave jugadorLlave;

    public GameObject puertaVisual;
    public InputActionAsset inputActions;
    private InputAction interactuar;

    void Awake()
    {
        interactuar = inputActions.FindAction("Interact");
    }

    void OnEnable()
    {
        inputActions.Enable();
    }

    void OnDisable()
    {
        inputActions.Disable();
    }

    void Update()
    {
        if (jugadorCerca && !puertaAbierta && interactuar != null && interactuar.WasPressedThisFrame())
        {
            if (jugadorLlave != null && jugadorLlave.tieneLlave)
            {
                AbrirPuerta();
            }
            else
            {
                Debug.Log("La puerta está cerrada. Necesitas una llave.");
            }
        }
    }

    void AbrirPuerta()
    {
        puertaAbierta = true;

        if (puertaVisual != null)
            puertaVisual.SetActive(false); // Desactiva el modelo visual

        Collider col = GetComponent<Collider>();
        if (col != null)
            col.enabled = false; // Desactiva colisión física

        Debug.Log("¡Puerta desbloqueada!");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true;
            jugadorLlave = other.GetComponent<JugadorLlave>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;
            jugadorLlave = null;
        }
    }
}
