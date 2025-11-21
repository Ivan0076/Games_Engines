using UnityEngine;

public class SalidaVictoria : MonoBehaviour
{
    [HideInInspector] public GameObject panelVictoria;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (panelVictoria != null)
            {
                panelVictoria.SetActive(true);

                // Pausar el juego al ganar
                Time.timeScale = 0f;

                Debug.Log("¡Has ganado la partida!");
            }
        }
    }
}
