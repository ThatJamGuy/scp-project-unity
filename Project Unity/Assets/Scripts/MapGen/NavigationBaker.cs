using UnityEngine;
using Unity.AI.Navigation;
using System.Collections;

public class NavigationBaker : MonoBehaviour
{
    [SerializeField] bool useTimer;
    [SerializeField] int timer;
    [SerializeField] NavMeshSurface[] navMeshSurfaces;

    private void Start()
    {
        if (!useTimer)
        {
            navMeshSurfaces = FindObjectsOfType<NavMeshSurface>();

            for (int i = 0; i < navMeshSurfaces.Length; i++)
            {
                navMeshSurfaces[i].BuildNavMesh();
                Debug.Log("Baked Navigataion Mesh without timer.");
            }
        }
        else
        {
            StartCoroutine(BakeAfterTimer());
        }
    }

    IEnumerator BakeAfterTimer()
    {
        yield return new WaitForSeconds(timer);

        navMeshSurfaces = FindObjectsOfType<NavMeshSurface>();

        for (int i = 0; i < navMeshSurfaces.Length; i++)
        {
            navMeshSurfaces[i].BuildNavMesh();
            Debug.Log("Baked Navigataion Mesh with timer.");
        }
    }
}