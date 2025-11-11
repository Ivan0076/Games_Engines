using UnityEngine;
using UnityEngine.InputSystem;

public class Cofres : MonoBehaviour
{
    private bool jugadorCerca = false;
    private bool abierto = false;

    void Update()
    {
        if (jugadorCerca && !abierto && Keyboard.current.eKey.wasPressedThisFrame)
        {
            AbrirCofre();
        }
    }

    void AbrirCofre()
    {
        abierto = true;
        FragmentosManager.instancia.AgregarFragmento();
        // Aquí podemos agregar animación o efecto visual
        Destroy(gameObject); // El cofre desaparece al abrirse
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            jugadorCerca = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            jugadorCerca = false;
    }
}
