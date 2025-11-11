using UnityEngine;

public class SalidaVictoria : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("¡Has ganado la partida!");
            // Aquí podemos cargar una escena, mostrar un panel de victoria, etc.
        }
    }
}
