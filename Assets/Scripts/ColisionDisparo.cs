using UnityEngine;

public class ColisionDisparo : MonoBehaviour
{
    void Start()
    {

    }

    void OnTriggerEnter(Collider other)
    {
    if (other.gameObject.CompareTag("Enemigo"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Entorno"))
        {
            Destroy(gameObject);
        }
    }
}
