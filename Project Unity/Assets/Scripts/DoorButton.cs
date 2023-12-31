using UnityEngine;

public class DoorButton : MonoBehaviour
{
    [SerializeField] AudioSource buttonSFX;
    [SerializeField] AudioSource lockedSFX;
    public Door door;
    public bool doorButton = true;
    public float interactCooldown = 0.1f;

    private void Update()
    {
        interactCooldown -= Time.deltaTime;
    }

    public void UseButton()
    {
        if (!door.isLocked)
        {
            if (interactCooldown > 0) return;

            buttonSFX.Play();
            interactCooldown = 0.1f;

            if (door != null)
            {
                if (door.isOpen)
                {
                    door.CloseDoor();
                }
                else
                {
                    door.OpenDoor();
                }
            }
        }
        else
        {
            lockedSFX.Play();
        }
    }
}