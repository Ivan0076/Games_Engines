using UnityEngine;
using UnityEngine.AI;

public class MovArmadura : MonoBehaviour
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
        (target.tag == "PlayerInv"))
        {


            if (target == null)
            {
                agente.SetDestination(gameObject.transform.position);
            }
        }

        if (target.tag == "Player")
        {
            if (Vector3.Distance(transform.position, target.transform.position) < distanciaMinima)
            {
                agente.SetDestination(target.transform.position);
            }
        }

        else
        {
            agente.SetDestination(gameObject.transform.position);
        }
    }
}
