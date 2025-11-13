using UnityEngine;
using UnityEngine.InputSystem;

public class PuertaInteraccion : MonoBehaviour
{
    private PuertaDesbloqueable puerta;

    void Awake()
    {
        puerta = GetComponentInParent<PuertaDesbloqueable>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var llave = other.GetComponent<JugadorLlave>();
            var playerInput = other.GetComponent<PlayerInput>();
            puerta?.SetJugadorCerca(true, llave, playerInput);
            Debug.Log("Jugador cerca de la puerta");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            puerta?.SetJugadorCerca(false, null, null);
            Debug.Log("Jugador se alejó de la puerta");
        }
    }
}
