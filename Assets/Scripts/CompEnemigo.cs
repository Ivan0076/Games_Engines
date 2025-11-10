using UnityEngine;

public class CompEnemigo : MonoBehaviour
{
    int contador;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        contador = 1;
    }
    void Update()
    {
        if (contador <= 0)
        {
            Destroy(this.gameObject);
        }

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bala"))
        {
            contador--;
        }

    }
}
