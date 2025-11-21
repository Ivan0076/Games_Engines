using TMPro;
using UnityEngine;

public class FragmentosManager : MonoBehaviour
{
    public static FragmentosManager instancia;

    public int fragmentosReunidos = 0;
    public int fragmentosNecesarios = 5;

    public TMP_Text textoFragmentos;

    public GameObject salidaPrefab;
    public Transform puntoSalida;

    // Referencia al panel de victoria en la escena
    public GameObject panelVictoria;

    void Awake()
    {
        if (instancia == null)
            instancia = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        if (textoFragmentos != null)
            textoFragmentos.text = "Fragmentos Encontrados: " + fragmentosReunidos + " / " + fragmentosNecesarios;
    }

    public void AgregarFragmento()
    {
        fragmentosReunidos++;
        Debug.Log("Fragmentos reunidos: " + fragmentosReunidos);

        if (fragmentosReunidos >= fragmentosNecesarios)
        {
            ActivarSalida();
        }
    }

    void ActivarSalida()
    {
        GameObject salida = Instantiate(salidaPrefab, puntoSalida.position, Quaternion.identity);

        // Asignar automáticamente el panel al script de la salida
        SalidaVictoria scriptSalida = salida.GetComponent<SalidaVictoria>();
        if (scriptSalida != null && panelVictoria != null)
        {
            scriptSalida.panelVictoria = panelVictoria;
        }

        Debug.Log("¡Has reunido todos los fragmentos! La salida ha aparecido.");
    }
}
