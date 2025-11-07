using UnityEngine;

public class ColisionJugador : MonoBehaviour
{
    private PoderesJugador poderesJugador;
    private Collider jugadorCollider;

    void Awake()
    {
        poderesJugador = GetComponent<PoderesJugador>();
        jugadorCollider = GetComponent<Collider>();
    }

    void Update()
    {
        if (poderesJugador != null && jugadorCollider != null)
        {
            if (poderesJugador.IntangibilidadActiva)
            {
                GameObject[] paredes = GameObject.FindGameObjectsWithTag("ParedTraspasable");

                foreach (GameObject pared in paredes)
                {
                    Collider paredCollider = pared.GetComponent<Collider>();
                    if (paredCollider != null)
                        Physics.IgnoreCollision(jugadorCollider, paredCollider, true);
                }
            }
            else
            {
                GameObject[] paredes = GameObject.FindGameObjectsWithTag("ParedTraspasable");

                foreach (GameObject pared in paredes)
                {
                    Collider paredCollider = pared.GetComponent<Collider>();
                    if (paredCollider != null)
                        Physics.IgnoreCollision(jugadorCollider, paredCollider, false);
                }
            }
        }
    }
}
