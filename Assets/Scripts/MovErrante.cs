using UnityEngine;
using UnityEngine.AI;

public class MovErrante : MonoBehaviour
{
    public float rangoMovimiento = 10f;
    public float tiempoEspera = 2f;
    public float rangoDeteccion = 8f;

    private NavMeshAgent agente;
    private float temporizador;
    private Transform jugador;

    public SpriteRenderer spriteRenderer; // ← Asigna en el Inspector
    public Animator animator;             // ← Asigna en el Inspector

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
            agente.SetDestination(jugador.position);
        }
        else
        {
            Patrullar();
        }

        ActualizarDireccionVisual();
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

    void ActualizarDireccionVisual()
    {
        Vector3 direccion = agente.velocity;

        if (direccion.x < -0.1f)
        {
            animator.Play("IdleIzquierda");
        }
    }

}
