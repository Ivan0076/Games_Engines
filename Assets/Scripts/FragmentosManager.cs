using UnityEngine;

public class FragmentosManager : MonoBehaviour
{
    public static FragmentosManager instancia;

    public int fragmentosReunidos = 0;
    public int fragmentosNecesarios = 5;

    public GameObject salidaPrefab;
    public Transform puntoSalida;

    void Awake()
    {
        if (instancia == null)
            instancia = this;
        else
            Destroy(gameObject);
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
        Instantiate(salidaPrefab, puntoSalida.position, Quaternion.identity);
        Debug.Log("¡Has reunido todos los fragmentos! La salida ha aparecido.");
    }
}
