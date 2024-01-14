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

    /// <summary>
    /// Defines the values for the current door in opening, doesn't actually handle the movement of the door pieces. 
    /// Triggered by calls made from other scripts or other event triggers.
    /// </summary>
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

    /// <summary>
    /// Similar to the OpenDoor() Method, only shorter due to the door pieces simply returning to their initial positions.
    /// </summary>
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

    /// <summary>
    /// The code that actually moves the door pieces. Utilizes an IEnumerator due to it's usage of timed aspects.
    /// </summary>
    /// <param name="door"></param>
    /// <param name="targetPosition"></param>
    /// <param name="speed"></param>
    /// <returns></returns>
    IEnumerator SlideDoor(GameObject door, Vector3 targetPosition, float speed)
    {
        isOpening = true;

        var startPos = door.transform.position;

        var distance = Vector3.Distance(startPos, targetPosition);

        var duration = distance / speed;

        var timePassed = 0f;
        while (timePassed < duration)
        {
            var factor = timePassed / duration;

            factor = Mathf.SmoothStep(0, 1, factor);

            door.transform.position = Vector3.Lerp(startPos, targetPosition, factor);

            yield return null;

            timePassed += Time.deltaTime;
        }

        door.transform.position = targetPosition;

        isOpening = false;
    }

    /// <summary>
    /// Plays the sound effects, randomized from a pre-defined array of sounds. Pretty simple to understand.
    /// </summary>
    /// <param name="soundEffects"></param>
    void PlayDoorSound(AudioClip[] soundEffects)
    {
        if (doorAudio != null && soundEffects.Length > 0)
        {
            doorAudio.PlayOneShot(soundEffects[Random.Range(0, soundEffects.Length)]);
        }
    }
}