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
    public bool isLocked;
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
            door01TargetPosition = door01InitialPosition + transform.right * regularOpenCloseDistance;
            door02TargetPosition = door02InitialPosition - transform.right * regularOpenCloseDistance;
            StartCoroutine(SlideDoor(door01, door01TargetPosition, regularOpenCloseSpeed));
            StartCoroutine(SlideDoor(door02, door02TargetPosition, regularOpenCloseSpeed));
            PlayDoorSound(doorOpenSFX);
            isOpen = true;
        }
        if (DoorType == DoorType.ContainmentDoor && !isOpening && !isOpen)
        {
            door01TargetPosition = door01InitialPosition + transform.right * regularOpenCloseDistance;
            door02TargetPosition = door02InitialPosition - transform.right * regularOpenCloseDistance;
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

        // Pre-cache the initial position
        var startPos = door.transform.position;

        // Calculate the distance between the start and target positions
        var distance = Vector3.Distance(startPos, targetPosition);

        // Calculate the duration of the animation based on the distance and speed
        var duration = distance / speed;

        var timePassed = 0f;
        while (timePassed < duration)
        {
            // This factor moves linear from 0 to 1
            var factor = timePassed / duration;

            // Add ease-in and ease-out using Mathf.SmoothStep
            factor = Mathf.SmoothStep(0, 1, factor);

            // Use Lerp to interpolate between the start and target positions
            door.transform.position = Vector3.Lerp(startPos, targetPosition, factor);

            // Yield execution to the next frame
            yield return null;

            // Increase timePassed by the time passed since the last frame
            timePassed += Time.deltaTime;
        }

        // Ensure the final position is accurate
        door.transform.position = targetPosition;

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