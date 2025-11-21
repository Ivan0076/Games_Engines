using UnityEngine;

public class SalidaVictoria : MonoBehaviour
{
    public GameObject panelVictoria;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            panelVictoria.SetActive(true);
            Debug.Log("¡Has ganado la partida!");
            
        }
    }
}
