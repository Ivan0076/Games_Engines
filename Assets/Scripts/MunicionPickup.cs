using UnityEngine;

public class MunicionPickup : MonoBehaviour
{
    public int cantidadMunicion = 1;

    void OnTriggerEnter(Collider other)
    {
        Disparar disparo = other.GetComponent<Disparar>();
        if (disparo != null)
        {
            disparo.RecogerMunicion(cantidadMunicion);
            Destroy(gameObject);
        }
    }
}
