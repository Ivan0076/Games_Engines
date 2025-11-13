using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PuertaDesbloqueable : MonoBehaviour
{
    private bool jugadorCerca = false;
    private JugadorLlave jugadorLlave;
    private InputAction interactuar;

    void Update()
    {
        if (jugadorCerca && interactuar != null && interactuar.WasPressedThisFrame())
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

    public void SetJugadorCerca(bool cerca, JugadorLlave llave, PlayerInput playerInput)
    {
        jugadorCerca = cerca;
        jugadorLlave = cerca ? llave : null;

        if (cerca && playerInput != null)
        {
            interactuar = playerInput.actions["Interact"];
            interactuar.Enable(); // ← habilita la acción para que WasPressedThisFrame funcione
            Debug.Log("Acción Interact conectada y habilitada");
        }
        else
        {
            if (interactuar != null)
                interactuar.Disable(); // ← deshabilita cuando el jugador se aleja
            interactuar = null;
        }
    }

    void AbrirPuerta()
    {
        Debug.Log("¡Puerta desbloqueada y destruida!");
        Destroy(gameObject);
    }
}
