using System.Collections;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource buttonSFX;
    [SerializeField] private AudioSource lockedSFX;

    [Header("Door Settings")]
    public Door door;
    public bool doorButton = true;

    private bool canInteract;

    private void Start()
    {
        canInteract = true;
    }

    public void UseButton()
    {
        if (!door.isLocked && canInteract)
        {
            buttonSFX.Play();

            ToggleDoorState();
        }
        else if (canInteract)
        {
            lockedSFX.Play();
            canInteract = false;
            StartCoroutine(Cooldown());
        }
    }

    private void ToggleDoorState()
    {
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

            canInteract = false;
            StartCoroutine(Cooldown());
        }
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(1f);
        canInteract = true;
    }
}