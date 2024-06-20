using UnityEngine;
using UnityEngine.AI;

public class SCP_173 : MonoBehaviour
{
    [SerializeField] private bool canMove;
    [SerializeField] private Renderer mesh;
    private NavMeshAgent agent;
    private GameObject playerObject;

    private void Start()
    {     
        agent = GetComponent<NavMeshAgent>();
        playerObject = GameObject.FindGameObjectWithTag("Player");

        canMove = true;
    }

    private void Update()
    {
        if(mesh.isVisible)
            canMove = false;
        else
            canMove = true;

        if (canMove)
        {
            agent.SetDestination(playerObject.transform.position);
        }
        else
        {
            agent.SetDestination(transform.position);
        }
    }
}