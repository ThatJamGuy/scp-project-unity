using UnityEngine;
using UnityEngine.AI;

public class SCP_173 : MonoBehaviour
{
    [SerializeField] private AudioSource moveSound;

    private NavMeshAgent agent;
    private GameObject playerObject;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;

        agent = GetComponent<NavMeshAgent>();
        playerObject = GameObject.FindGameObjectWithTag("Player");
    }

    public bool IsVisible(GameObject obj)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(mainCamera);
        return GeometryUtility.TestPlanesAABB(planes, obj.GetComponentInChildren<Renderer>().bounds);
    }

    private void Update()
    {
        GameObject scp173Obj = gameObject;
        bool isVisible = IsVisible(scp173Obj);
        Debug.Log($"Is object visible? {isVisible}");

        if (!isVisible || playerObject.GetComponent<Player>().isBlinking)
        {
            agent.SetDestination(playerObject.transform.position);
            moveSound.enabled = true;
        }
        else
        {
            agent.SetDestination(transform.position);
            moveSound.enabled = false;
        }
    }
}