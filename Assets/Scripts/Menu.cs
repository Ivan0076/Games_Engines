using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject panelPausa;
    public GameObject botonPausa;
    public GameObject panelGameOver;
    public void IrJuego()
    {
        Time.timeScale = 1;

        GameObject jugador = GameObject.FindGameObjectWithTag("Player");
        if (jugador != null)
        {
            PoderesJugador poderes = jugador.GetComponent<PoderesJugador>();
            if (poderes != null)
            {
                poderes.ReiniciarEstado();
            }

        }
        SceneManager.LoadScene("Depuracion");
    }

    public void IrMenu()
    {
        SceneManager.LoadScene("Menu_Principal");
    }

    public void Pausar()
    {
        panelPausa.SetActive(true); //Activamos y prendemos GameObjects
        botonPausa.SetActive(false); //Desaparece el boton de pausa
        Time.timeScale = 0; //Todo el juego se detiene
    }

    public void Reanudar()
    {
        panelPausa.SetActive(false);
        botonPausa.SetActive(true);
        Time.timeScale = 1;
    }
}
