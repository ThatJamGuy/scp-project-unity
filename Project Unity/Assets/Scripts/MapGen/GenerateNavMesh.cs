using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class GenerateNavMesh : MonoBehaviour
{
    public NavMeshSurface surface;

    private void Start()
    {
        StartCoroutine(GenerateNavigationMesh());
    }

    public void GenerateNewMesh()
    {
        surface.BuildNavMesh();
    }

    IEnumerator GenerateNavigationMesh()
    {
        yield return new WaitForSeconds(2);
        GenerateNewMesh();
    }
}