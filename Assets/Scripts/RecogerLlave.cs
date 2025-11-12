using UnityEngine;

public class RecogerLlave : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        JugadorLlave inventario = other.GetComponent<JugadorLlave>();
        if (inventario != null)
        {
            inventario.tieneLlave = true;
            Debug.Log("Llave recogida");
            Destroy(gameObject);
        }
    }
}
