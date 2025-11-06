using UnityEngine;

public class ColisionJugador : MonoBehaviour
{
    private PoderesJugador poderesJugador;

    void Awake()
    {
        poderesJugador = GetComponent<PoderesJugador>();
    }

    void Update()
    {
        if (poderesJugador != null &&
            poderesJugador.poderSeleccionado == PoderActivo.Intangibilidad &&
            poderesJugador.tieneIntangibilidad)
        {
            // Buscar todas las paredes traspasables en la escena
            GameObject[] paredes = GameObject.FindGameObjectsWithTag("ParedTraspasable");

            foreach (GameObject pared in paredes)
            {
                Collider paredCollider = pared.GetComponent<Collider>();
                Collider jugadorCollider = GetComponent<Collider>();

                if (paredCollider != null && jugadorCollider != null)
                {
                    Physics.IgnoreCollision(jugadorCollider, paredCollider, true);
                }
            }
        }
        else
        {
            // Restaurar colisiones si no está activo el poder
            GameObject[] paredes = GameObject.FindGameObjectsWithTag("ParedTraspasable");

            foreach (GameObject pared in paredes)
            {
                Collider paredCollider = pared.GetComponent<Collider>();
                Collider jugadorCollider = GetComponent<Collider>();

                if (paredCollider != null && jugadorCollider != null)
                {
                    Physics.IgnoreCollision(jugadorCollider, paredCollider, false);
                }
            }
        }
    }
}
