using UnityEngine;

public class ColisionJugador : MonoBehaviour
{
    private PoderesJugador poderesJugador;

    private void Awake()
    {
        poderesJugador = GetComponent<PoderesJugador>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (poderesJugador != null && poderesJugador.esIntangible && collision.gameObject.CompareTag("ParedTraspasable"))
        {
            Collider jugadorCollider = GetComponent<Collider>();
            Physics.IgnoreCollision(jugadorCollider, collision.collider);
        }
    }
}
