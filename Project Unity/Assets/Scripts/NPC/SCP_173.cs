using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SCP_173 : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject player;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        agent.SetDestination(player.transform.position);
    }
}