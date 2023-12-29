using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomRenderer : MonoBehaviour
{
    public float radius = 10f; // Set the radius

    private void Update()
    {
        // Get all game objects in the scene
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            // Check if the object has a Renderer component
            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer != null)
            {
                // Calculate the distance between the player and the object
                float distance = Vector3.Distance(transform.position, obj.transform.position);

                // Check if the object is outside the radius
                if (distance > radius)
                {
                    // If it is, disable the renderer
                    renderer.enabled = false;
                }
                else
                {
                    // If it is inside the radius, enable the renderer
                    renderer.enabled = true;
                }
            }
        }
    }
}