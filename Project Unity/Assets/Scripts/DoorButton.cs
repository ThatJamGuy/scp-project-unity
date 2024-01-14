using UnityEngine;

public class DoorButton : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource buttonSFX;
    [SerializeField] private AudioSource lockedSFX;

    [Header("Door Settings")]
    public Door door; // Assuming Door is another script/class
    public bool doorButton = true;
    public float interactCooldown = 0.1f;

    private void Update()
    {
        // Update the interaction cooldown
        interactCooldown -= Time.deltaTime;
    }

    public void UseButton()
    {
        // Check if the door is not locked
        if (!door.isLocked)
        {
            // Check if interaction cooldown is still active
            if (interactCooldown > 0) return;

            // Play button sound and reset cooldown
            buttonSFX.Play();
            interactCooldown = 0.1f;

            // Check if the door is assigned
            if (door != null)
            {
                // Toggle door state
                if (door.isOpen)
                    door.CloseDoor();
                else
                    door.OpenDoor();
            }
        }
        else
        {
            // Play locked sound if the door is locked and reset cooldown
            if (interactCooldown < 0)
            {
                lockedSFX.Play();
                interactCooldown = 0.1f;
            }
        }
    }
}