using UnityEngine;

public class Vida : MonoBehaviour
{
    public int vida;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vida = 25;
    }

    // Update is called once per frame
    void Update()
    {
        if (vida <= 0)
        {
            Time.timeScale = 0f;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemigo"))
        {
            vida--;

        }

    }
}
