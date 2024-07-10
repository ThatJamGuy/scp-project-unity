using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SCP_106 : MonoBehaviour
{
    [Header("References")]
    public BreachManager breachManager;
    public GameObject decal;
    public GameObject smalDecal;

    [Header("Audio Clips")]
    public AudioClip[] stepSFX;
    public AudioClip horror;
    public AudioClip horrorFinished;
    public AudioClip chaseMusic;
    public AudioClip killSound;

    [Header("Audio Sources")]
    public AudioSource footSource;
    public AudioSource decay;
    public AudioSource miscSFX;
    public AudioSource[] sourcesToToggle;

    [Header("Timer Settings")]
    public float spawnDelay = 10f;
    public float playerRange = 3f;

    private Animator animator;
    private NavMeshAgent agent;
    private Transform player;
    private Player playerScript;
    private bool isMovementActive;
    private bool isActive = false;

    private void Start()
    {
        InitializeComponents();
        DisableAudioSources();
        StartCoroutine(SpawnDelay());
    }

    private void Update()
    {
        if (isMovementActive)
        {
            agent.SetDestination(player.position);

            if (Vector3.Distance(transform.position, player.position) <= playerRange && !playerScript.isDead)
            {
                bool canPlaySound = true;

                if (canPlaySound)
                {
                    canPlaySound = false;
                    miscSFX.clip = killSound;
                    miscSFX.Play();
                }

                playerScript.KillPlayer();
                EndChase();

                //breachManager.ChangeMusic(breachManager.zone1Music);
                gameObject.transform.position = new Vector3(0, 0, 0);
            }
        }
    }

    private void InitializeComponents()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
        playerScript = player.GetComponent<Player>();
        breachManager = FindFirstObjectByType<BreachManager>();
        agent.enabled = false;
    }

    private void DisableAudioSources()
    {
        foreach (AudioSource source in sourcesToToggle)
        {
            source.enabled = false;
        }
    }

    public void Spawn106()
    {
        PositionCharacter();
        animator.Play("Rise");
        decay.Play();
        StartCoroutine(Scripted106());
    }

    private void PositionCharacter()
    {
        Quaternion desiredRotation = Quaternion.Euler(90f, 0f, 0f);
        transform.position = player.position;
        agent.Warp(player.position);
        Instantiate(decal, transform.position, desiredRotation);
        agent.enabled = true;
    }

    private void Step()
    {
        footSource.clip = stepSFX[Random.Range(0, stepSFX.Length)];
        footSource.Play();

        Quaternion desiredRotation = Quaternion.Euler(90f, 0f, 0f);

        var stepDecalCopy = Instantiate(smalDecal, transform.position, desiredRotation);
        Destroy(stepDecalCopy, 120f);
    }

    private IEnumerator SpawnDelay()
    {
        if (!isActive)
        {
            yield return new WaitForSeconds(spawnDelay);
            isActive = true;
            Spawn106();
        }
    }

    private IEnumerator Scripted106()
    {
        EnableAudioSources();
        yield return new WaitForSeconds(12);
        StartChase();
        yield return new WaitForSeconds(35);
        SpeedUpChase();
        yield return new WaitForSeconds(35);
        EndChase();
    }

    private void EnableAudioSources()
    {
        foreach (AudioSource source in sourcesToToggle)
        {
            source.enabled = true;
        }
    }

    private void StartChase()
    {
        miscSFX.clip = horror;
        miscSFX.Play();
        isMovementActive = true;
        MusicPlayer.instance.StartMusic(chaseMusic);
        Debug.Log("SCP-106 has started the chase");
    }

    private void SpeedUpChase()
    {
        agent.speed = 3.5f;
        animator.Play("Fast Walk");
        Debug.Log("SCP-106 has sped up the chase");
    }

    private void EndChase()
    {
        MusicPlayer.instance.ChangeMusic(breachManager.zone1Music);
        miscSFX.clip = horrorFinished;
        miscSFX.Play();
        decay.Play();
        agent.SetDestination(transform.position);
        isMovementActive = false;
        animator.Play("Lower");
        Quaternion desiredRotation = Quaternion.Euler(90f, 0f, 0f);
        Instantiate(decal, transform.position, desiredRotation);
        DisableAudioSources();
        isActive = false;
        StartCoroutine(SpawnDelay());
        Debug.Log("SCP-106 has ended the chase. Waiting to respawn...");
    }
}