using System.Collections;
using UnityEngine;

public class RoomRenderer : MonoBehaviour
{
    public float radius = 10f; // Set the radius
    public LayerMask renderLayer; // Set the layer to filter objects

    private Renderer[] renderers;

    private void Start()
    {
        // Find all renderers on the specified layer
        renderers = FindObjectsOfType<Renderer>();
    }

    private void Update()
    {
        // Find all renderers on the specified layer
        renderers = FindObjectsOfType<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            // Check if the object is on the specified layer
            if (((1 << renderer.gameObject.layer) & renderLayer) != 0)
            {
                // Calculate the distance between the player and the object
                float distance = Vector3.Distance(transform.position, renderer.transform.position);

                // Toggle renderer based on distance
                renderer.enabled = distance <= radius;
            }
        }
    }
}