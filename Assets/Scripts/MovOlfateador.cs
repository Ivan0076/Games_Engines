using UnityEngine;
using UnityEngine.AI;

public class MovOlfateador : MonoBehaviour
{
    public float rangoMovimiento = 10f;     // radio de patrulla
    public float tiempoEspera = 2f;         // tiempo que espera antes de moverse de nuevo
    public float rangoDeteccion = 8f;       // distancia para detectar al jugador
    public float distanciaMinima = 5f;      // distancia mínima para seguir

    private NavMeshAgent agente;
    private float temporizador;
    private GameObject target;

    public SpriteRenderer spriteRenderer; // ← Asigna en el Inspector
    public Animator animator;             // ← Asigna en el Inspector

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");
        temporizador = tiempoEspera;
        MoverANuevaPosicion();
    }

    void Update()
    {
        // Buscar al jugador, ya sea normal o invisible
        if (target == null || (target.tag != "Player" && target.tag != "PlayerInv"))
        {
            target = GameObject.FindGameObjectWithTag("Player");

            if (target == null)
            {
                target = GameObject.FindGameObjectWithTag("PlayerInv");
            }
        }

        if (target != null)
        {
            float distanciaJugador = Vector3.Distance(transform.position, target.transform.position);

            // Si el jugador está cerca (visible o invisible), seguirlo
            if (distanciaJugador <= rangoDeteccion)
            {
                agente.SetDestination(target.transform.position);
            }
            else
            {
                // Si está lejos, patrullar
                Patrullar();
            }

            ActualizarDireccionVisual();
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

    void ActualizarDireccionVisual()
    {
        Vector3 direccion = agente.velocity;

        if (direccion.x < -0.1f)
        {
            animator.Play("Caminar_Olfateador");
        }
    }
}
