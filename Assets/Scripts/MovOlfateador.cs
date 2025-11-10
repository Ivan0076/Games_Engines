using UnityEngine;
using UnityEngine.AI;

public class MovOlfateador : MonoBehaviour
{
    public GameObject target;
    public GameObject targetBase;
    NavMeshAgent agente;

    public float distanciaMinima;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null ||
        (target.tag != "Player" && target.tag != "PlayerInv"))
        {
            target = GameObject.FindGameObjectWithTag("Player");

            if (target == null)
            {
                target = GameObject.FindGameObjectWithTag("PlayerInv");
            }
        }

        if (target != null)
        {
            if (Vector3.Distance(transform.position, target.transform.position) < distanciaMinima)
            {
                agente.SetDestination(target.transform.position);
            }
        }
        else
        {
            agente.SetDestination(targetBase.transform.position);
        }
    }
}
