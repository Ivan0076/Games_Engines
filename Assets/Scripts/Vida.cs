using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Vida : MonoBehaviour
{
    public GameObject panelGameOver;

    public int vidaMaxima = 25;
    public int vidaActual;

    private bool gameOver = false; //Un booleano para activar y desactivar el GameOver


    public Slider barraVida; // Asigna el slider desde el Inspector

    void Start()
    {
        vidaActual = vidaMaxima;

        if (barraVida != null)
        {
            barraVida.maxValue = vidaMaxima;
            barraVida.value = vidaActual;
        }
    }

    void Update()
    {
        if (vidaActual <= 0)
        {
            Time.timeScale = 0f;
            Debug.Log("¡Jugador derrotado!");
        }

        if (barraVida != null)
        {
            barraVida.value = vidaActual;
        }

        if (vidaActual <= 0 && !gameOver) //Verifica si la vida esta en 0 y si el GameOver no ha sido activado
        {
            GameOver(); //Llama a la funcion 
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemigo"))
        {
            vidaActual--;
            Debug.Log("Vida actual: " + vidaActual);
        }
    }
    void GameOver()
    {
        gameOver = true;
        Time.timeScale = 0;

        if (panelGameOver != null)
            panelGameOver.SetActive(true);

        Debug.Log("¡Game Over!");
    }

}
