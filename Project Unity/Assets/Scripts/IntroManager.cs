using System.Collections;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    [Header("PA System")]
    public AudioSource speakerSource;
    public AudioClip[] paClips;

    [Header("Intro Settings & Values")]
    public bool isInChamber;
    public int timeForIntro1;
    public int timeForIntro2;
    public int timeForIntro3;

    [Header("Intro Triggers")]
    public GameObject chamberEnterTrigger;

    private void Start()
    {
        StartCoroutine(IntroSequenceStart());
    }

    public void EnteredChamber()
    {
        Debug.Log("Player has entered the chamber");
        isInChamber = true;
    }

    IEnumerator IntroSequenceStart()
    {
        yield return new WaitForSeconds(timeForIntro1);
        speakerSource.clip = paClips[0];
        speakerSource.Play();

        yield return new WaitForSeconds(timeForIntro2);
        if (isInChamber)
            StartCoroutine(IntroSequenceFinish());
        else
            Debug.Log("Player is not in the chamber!");
    }

    IEnumerator IntroSequenceFinish()
    {
        yield return new WaitForSeconds(1);
        speakerSource.clip = paClips[1];
        speakerSource.Play();
        yield return new WaitForSeconds(timeForIntro3);
        speakerSource.clip = paClips[2];
        speakerSource.Play();
    }
}