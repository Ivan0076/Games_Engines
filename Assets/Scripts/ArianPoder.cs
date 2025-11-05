using UnityEngine;


public enum TipoPoder { Dash, Intangibilidad, Invisibilidad }
public class ArianPoder : MonoBehaviour
{
    public TipoPoder poder;

    private void OnTriggerEnter(Collider other)
    {
        PoderesJugador jugador = other.GetComponent<PoderesJugador>();
        if (jugador != null)
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

            Destroy(gameObject); // Elimina el objeto tras recogerlo
        }
    }
}
