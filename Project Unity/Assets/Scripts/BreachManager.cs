using System.Collections;
using UnityEngine;

public class BreachManager : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] AudioClip breachSequence;
    [SerializeField] AudioClip[] ambience;

    [Header("Audio Sources")]
    [SerializeField] AudioSource breachSource;
    [SerializeField] AudioSource ambienceSource;

    private void Start()
    {
        StartBreach();
        StartCoroutine(Ambience());
    }

    public void StartBreach()
    {
        breachSource.clip = breachSequence;
        breachSource.Play();
    }

    IEnumerator Ambience()
    {
        while(true)
        {
            if (ambience != null)
            {
                int rand = Random.Range(0, ambience.Length);
                ambienceSource.panStereo = Random.Range(-1, 1);
                ambienceSource.clip = ambience[rand];
                ambienceSource.Play();
                yield return new WaitForSeconds(Random.Range(10, 30));
            }
        }
    }
}