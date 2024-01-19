using System.Collections;
using UnityEngine;

public class GenerateDoors : MonoBehaviour
{
    [Header("THE DOOR :O WOW")]
    [SerializeField] GameObject doorPrefab;

    [Header("Other Vaalues")]
    public LayerMask noTouchLayer;
    public float overlapRadius = 0.5f;

    private void Start()
    {
        StartCoroutine(GenerateDoorsAuto());
    }

    // Using an IEnumerator here, as the map takes a few frames to generate, meaning attempting to spawn the doors
    // on frame 1 will result in nothing, as the DoorSpots have not been created yet.
    IEnumerator GenerateDoorsAuto()
    {
        yield return new WaitForSeconds(1);
        InstantiateDoors();
    }

    void InstantiateDoors()
    {
        GameObject[] doorSpots = GameObject.FindGameObjectsWithTag("DoorSpot");

        foreach (GameObject doorSpot in doorSpots)
        {
            InstantiateDoorAtSpot(doorSpot);
        }
    }

    // Method that instantiates the doors, utilizing DoorSpot data and various checks to ensure proper placement.
    void InstantiateDoorAtSpot(GameObject doorSpot)
    {
        // Get the rotation of the DoorSpot
        Quaternion rotation = doorSpot.transform.rotation;

        // Check for overlapping doors before instantiation
        if (!IsOverlappingDoors(doorSpot.transform.position))
        {
            // Instantiate the door prefab at the DoorSpot's position and rotation
            GameObject door = Instantiate(doorPrefab, doorSpot.transform.position, rotation);

            door.layer = LayerMask.NameToLayer("NoTouch");

            // Parent the door to the DoorSpot for organization
            door.transform.parent = doorSpot.transform;
        }
    }

    // Check to ensure that no doors overlap upon placing them in the scene.
    bool IsOverlappingDoors(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, overlapRadius, noTouchLayer);

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.layer == LayerMask.NameToLayer("NoTouch"))
            {
                return true;
            }
        }

        return false;
    }
}