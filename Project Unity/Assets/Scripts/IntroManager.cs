using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class IntroManager : MonoBehaviour
{
    [Header("PA System")]
    public AudioSource speakerSource;
    public AudioClip[] paClips;

    [Header("Other SFX")]
    public AudioSource Bang;
    public AudioSource Elec1;
    public AudioSource Horror3;
    public AudioSource Neck;
    public AudioSource Metal173;
    public AudioSource TheDread;

    [Header("Intro Settings & Values")]
    public bool isInChamber;
    public int timeForIntro1;
    public int timeForIntro2;
    public int timeForIntro3;

    [Header("Intro GameObjects")]
    public GameObject chamberEnterTrigger;
    public Door chamberDoor;
    public GameObject dBoi01;
    public GameObject dBoi02;
    public Transform dBoi01Target;
    public Transform dBoi02Target;
    public GameObject scp173;
    public GameObject introLights;
    public GameObject emergencyLights;
    public Transform scpTarget3;
    public GameObject doorButton;

    private NavMeshAgent dBoi01Agent;
    private NavMeshAgent dBoi02Agent;
    private Animator dBoi01Animator;
    private Animator dBoi02Animator;

    private void Start()
    {
        StartCoroutine(IntroSequenceStart());

        dBoi01Agent = dBoi01.GetComponent<NavMeshAgent>();
        dBoi02Agent = dBoi02.GetComponent<NavMeshAgent>();
        dBoi01Animator = dBoi01.GetComponent<Animator>();
        dBoi02Animator = dBoi02.GetComponent<Animator>();
    }

    public void EnteredChamber()
    {
        Debug.Log("Player has entered the chamber");
        chamberEnterTrigger.SetActive(false);
        isInChamber = true;
        StopAllCoroutines();
        StartCoroutine(IntroSequenceFinish());
    }

    IEnumerator IntroSequenceStart()
    {
        yield return new WaitForSeconds(timeForIntro1);
        chamberDoor.OpenDoor();
        speakerSource.clip = paClips[0];
        speakerSource.Play();
        yield return new WaitForSeconds(5);
        dBoi01Agent.SetDestination(dBoi01Target.transform.position);
        dBoi01Animator.Play("Walk");
        yield return new WaitForSeconds(1);
        dBoi02Agent.SetDestination(dBoi02Target.transform.position);
        dBoi02Animator.Play("Walk");

        yield return new WaitForSeconds(timeForIntro2);
        if (isInChamber)
            StartCoroutine(IntroSequenceFinish());
        else
            Debug.Log("Player is not in the chamber!");
    }

    IEnumerator IntroSequenceFinish()
    {
        yield return new WaitForSeconds(3);
        chamberDoor.CloseDoor();
        speakerSource.clip = paClips[1];
        speakerSource.Play();
        yield return new WaitForSeconds(5);
        Bang.Play();
        chamberDoor.OpenDoor();
        yield return new WaitForSeconds(timeForIntro3);
        speakerSource.clip = paClips[2];
        speakerSource.Play();
        yield return new WaitForSeconds(10.8f);
        Elec1.Play();
        yield return new WaitForSeconds(1);
        introLights.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        introLights.SetActive(true);
        scp173.transform.position = dBoi01.transform.position;
        Neck.Play();
        Horror3.Play();
        yield return new WaitForSeconds(2);
        introLights.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        introLights.SetActive(true);
        dBoi01.SetActive(false);
        scp173.transform.position = dBoi02.transform.position;
        Neck.Play();
        yield return new WaitForSeconds(2);
        introLights.SetActive(false);
        Bang.Play();
        yield return new WaitForSeconds(0.3f);
        dBoi02.SetActive(false);
        scp173.transform.position = scpTarget3.transform.position;
        introLights.SetActive(true);
        yield return new WaitForSeconds(3);
        introLights.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        Metal173.Play();
        scp173.SetActive(false);
        yield return new WaitForSeconds(1);
        emergencyLights.SetActive(true);
        doorButton.SetActive(true);
        TheDread.Play();
    }
}