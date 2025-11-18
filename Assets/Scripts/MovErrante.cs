using UnityEngine;
using UnityEngine.AI;

public class MovErrante : MonoBehaviour
{
    public float rangoMovimiento = 10f;     // radio de patrulla
    public float tiempoEspera = 2f;         // tiempo que espera antes de moverse de nuevo
    public float rangoDeteccion = 8f;       // distancia para detectar al jugador

    private NavMeshAgent agente;
    private float temporizador;
    private Transform jugador;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
        temporizador = tiempoEspera;
        MoverANuevaPosicion();
    }

    void Update()
    {
        if (jugador == null) return;

        float distanciaJugador = Vector3.Distance(transform.position, jugador.position);

        if (distanciaJugador <= rangoDeteccion)
        {
            // Si el jugador está cerca lo persigue
            agente.SetDestination(jugador.position);
        }
        else
        {
            // Si el jugador está lejos → patrullar
            Patrullar();
        }
    }

    void Patrullar()
    {
        if (!agente.pathPending && agente.remainingDistance <= agente.stoppingDistance)
        {
            temporizador -= Time.deltaTime;
            if (temporizador <= 0f)
            {
                MoverANuevaPosicion();
                temporizador = tiempoEspera;
            }
        }
    }

    void MoverANuevaPosicion()
    {
        Vector3 puntoAleatorio = Random.insideUnitSphere * rangoMovimiento;
        puntoAleatorio += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(puntoAleatorio, out hit, rangoMovimiento, NavMesh.AllAreas))
        {
            agente.SetDestination(hit.position);
        }
    }
}
