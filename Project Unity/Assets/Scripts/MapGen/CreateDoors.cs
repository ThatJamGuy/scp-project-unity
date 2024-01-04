using LevelGenerator.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateDoors : MonoBehaviour
{
    [SerializeField] GameObject doorPrefab;

    private void Start()
    {
        StartCoroutine(AutoDoor());
    }

    [ContextMenu("Do The Thing")]
    public void DoTheThing()
    {
        Exits[] allExits = GameObject.FindObjectsOfType<Exits>();

        foreach (Exits exit in allExits)
        {
            // Instantiate the doorPrefab at the exit's position
            GameObject door = Instantiate(doorPrefab, exit.transform.position, exit.transform.rotation);
        }
    }

    IEnumerator AutoDoor()
    {
        yield return new WaitForSeconds(1);
        DoTheThing();
    }
}