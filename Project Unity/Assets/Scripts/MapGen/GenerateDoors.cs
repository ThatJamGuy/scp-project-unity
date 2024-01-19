using System.Collections;
using System.Linq;
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

        // Instantiate the door prefab at the DoorSpot's position and rotation if there are no overlapping doors
        if (!IsOverlappingDoors(doorSpot.transform.position))
        {
            GameObject door = Instantiate(doorPrefab, doorSpot.transform.position, rotation);
            door.layer = LayerMask.NameToLayer("NoTouch");
            door.transform.parent = doorSpot.transform;
        }
    }

    // Check to ensure that no doors overlap upon placing them in the scene.
    bool IsOverlappingDoors(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, overlapRadius, noTouchLayer);

        return colliders.Any(collider => collider.gameObject.layer == LayerMask.NameToLayer("NoTouch"));
    }
}