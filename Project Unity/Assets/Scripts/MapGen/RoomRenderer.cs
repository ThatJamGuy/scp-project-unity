using System.Collections;
using UnityEngine;

public class RoomRenderer : MonoBehaviour
{
    public float cullingRange = 50f;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found. Please tag your camera as 'MainCamera'.");
            return;
        }

        // Perform frustum culling for MeshRenderers
        MeshRenderer[] renderers = FindObjectsOfType<MeshRenderer>();
        foreach (MeshRenderer renderer in renderers)
        {
            if (IsRendererVisibleByCamera(renderer))
            {
                renderer.enabled = true;
            }
            else
            {
                renderer.enabled = false;
            }
        }

        // Perform distance-based culling for lights using spatial partitioning
        Light[] lights = FindObjectsOfType<Light>();
        foreach (Light light in lights)
        {
            float distanceToLight = Vector3.Distance(mainCamera.transform.position, light.transform.position);

            if (distanceToLight <= cullingRange)
            {
                light.enabled = true;
            }
            else
            {
                light.enabled = false;
            }
        }
    }

    private bool IsRendererVisibleByCamera(Renderer renderer)
    {
        // Check if the renderer is within the camera's view frustum
        return GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(mainCamera), renderer.bounds);
    }
}