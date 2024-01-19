using UnityEngine;

public class SoundOcclusion : MonoBehaviour
{
    private AudioSource audioSource;
    private Camera mainCamera; // Automatically set to the main camera

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        mainCamera = Camera.main; // Set the main camera
    }

    void Update()
    {
        // Check if the main camera exists and if the listener is within the audible range
        if (mainCamera != null && Vector3.Distance(transform.position, mainCamera.transform.position) <= audioSource.maxDistance)
        {
            // Calculate the direction from the sound source to the listener
            Vector3 direction = mainCamera.transform.position - transform.position;

            // Perform a raycast to check for obstacles between the sound source and the listener
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction.normalized, out hit, audioSource.maxDistance))
            {
                // Sound is occluded, reduce volume or take other actions
                audioSource.volume = 0.1f; // Adjust the volume based on your preferences
            }
            else
            {
                // No occlusion, play sound at full volume
                audioSource.volume = 1f;
            }
        }
        else
        {
            // Listener is outside the audible range or main camera is not found, stop playing the sound
            audioSource.Stop();
        }
    }
}