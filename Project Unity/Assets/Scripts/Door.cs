using System.Collections;
using UnityEngine;

public enum DoorType
{ 
    RegularSliding,
    HeavySliding,
    ContainmentDoor
}
public class Door : MonoBehaviour
{
    [Header("Door Type")]
    public DoorType DoorType;

    [Header("Door Settings")]
    public bool isOpen;
    public float regularOpenCloseSpeed;
    public float regularOpenCloseDistance;

    [Header("Door Objects")]
    public GameObject door01;
    public GameObject door02;

    [Header("Audio")]
    public AudioSource doorAudio;
    public AudioClip[] doorOpenSFX;
    public AudioClip[] doorCloseSFX;

    private Vector3 door01InitialPosition;
    private Vector3 door01TargetPosition;
    private Vector3 door02InitialPosition;
    private Vector3 door02TargetPosition;
    private bool isOpening = false;

    private void Start()
    {
        door01InitialPosition = door01.transform.position;
        door02InitialPosition = door02.transform.position;
    }

    public void OpenDoor()
    {
        if (DoorType == DoorType.RegularSliding && !isOpening && !isOpen)
        {
            door01TargetPosition = door01InitialPosition + new Vector3(regularOpenCloseDistance, 0f, 0f);
            door02TargetPosition = door02InitialPosition - new Vector3(regularOpenCloseDistance, 0f, 0f);
            StartCoroutine(SlideDoor(door01, door01TargetPosition, regularOpenCloseSpeed));
            StartCoroutine(SlideDoor(door02, door02TargetPosition, regularOpenCloseSpeed));
            PlayDoorSound(doorOpenSFX);
            isOpen = true;
        }
        if (DoorType == DoorType.ContainmentDoor && !isOpening && !isOpen)
        {
            door01TargetPosition = door01InitialPosition + new Vector3(0f, 0f, regularOpenCloseDistance);
            door02TargetPosition = door02InitialPosition - new Vector3(0f, 0f, regularOpenCloseDistance);
            StartCoroutine(SlideDoor(door01, door01TargetPosition, regularOpenCloseSpeed));
            StartCoroutine(SlideDoor(door02, door02TargetPosition, regularOpenCloseSpeed));
            PlayDoorSound(doorOpenSFX);
            isOpen = true;
        }
    }

    public void CloseDoor()
    {
        if (DoorType == DoorType.RegularSliding && isOpen && !isOpening)
        {
            door01TargetPosition = door01InitialPosition;
            door02TargetPosition = door02InitialPosition;
            StartCoroutine(SlideDoor(door01, door01TargetPosition, regularOpenCloseSpeed));
            StartCoroutine(SlideDoor(door02, door02TargetPosition, regularOpenCloseSpeed));
            PlayDoorSound(doorCloseSFX);
            isOpen = false;
        }
        if (DoorType == DoorType.ContainmentDoor && isOpen && !isOpening)
        {
            door01TargetPosition = door01InitialPosition;
            door02TargetPosition = door02InitialPosition;
            StartCoroutine(SlideDoor(door01, door01TargetPosition, regularOpenCloseSpeed));
            StartCoroutine(SlideDoor(door02, door02TargetPosition, regularOpenCloseSpeed));
            PlayDoorSound(doorCloseSFX);
            isOpen = false;
        }
    }

    IEnumerator SlideDoor(GameObject door, Vector3 targetPosition, float speed)
    {
        isOpening = true;

        while (door.transform.position != targetPosition)
        {
            door.transform.position = Vector3.MoveTowards(door.transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }

        isOpening = false;
    }

    void PlayDoorSound(AudioClip[] soundEffects)
    {
        if (doorAudio != null && soundEffects.Length > 0)
        {
            doorAudio.PlayOneShot(soundEffects[Random.Range(0, soundEffects.Length)]);
        }
    }
}