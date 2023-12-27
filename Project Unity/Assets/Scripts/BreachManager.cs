using System.Collections;
using UnityEngine;

public class BreachManager : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] AudioClip breachStart;
    [SerializeField] AudioClip alarm;
    [SerializeField] AudioClip[] bangs;
    [SerializeField] AudioClip[] ambience;

    [Header("Audio Sources")]
    [SerializeField] AudioSource announcementSource;
    [SerializeField] AudioSource alarmSource;
    [SerializeField] AudioSource ambienceSource;

    public void StartBreach()
    {
        announcementSource.clip = breachStart;
        alarmSource.clip = alarm;

        alarmSource.Play();

        StartCoroutine(BreachStarterSequence());
    }

    IEnumerator BreachStarterSequence()
    {
        ambienceSource.clip = bangs[0];
        ambienceSource.Play();
        yield return new WaitForSeconds(3);
        announcementSource.Play();
        yield return new WaitForSeconds(2);
        ambienceSource.clip = bangs[1];
        ambienceSource.Play();
        yield return new WaitForSeconds(5);
        ambienceSource.clip = bangs[3];
        ambienceSource.Play();
        yield return new WaitForSeconds(9);
        alarmSource.Stop();
        ambienceSource.clip = bangs[2];
        ambienceSource.Play();

        yield return new WaitForSeconds(5);
        StartCoroutine(Ambience());
    }

    IEnumerator Ambience()
    {
        while(true)
        {
            if (ambience != null)
            {
                int rand = Random.Range(0, ambience.Length);
                ambienceSource.clip = ambience[rand];
                ambienceSource.Play();
                yield return new WaitForSeconds(Random.Range(10, 30));
            }
        }
    }
}