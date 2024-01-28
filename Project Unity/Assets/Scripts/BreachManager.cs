using System.Collections;
using UnityEngine;

public class BreachManager : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] AudioClip breachSequence;
    [SerializeField] AudioClip[] ambience;
    [SerializeField] AudioClip[] commotion;

    [Header("Audio Sources")]
    [SerializeField] AudioSource breachSource;
    [SerializeField] AudioSource ambienceSource;
    [SerializeField] AudioSource commotionSource;

    private int currentCommotionIndex = 0;

    private void Start()
    {
        //StartBreach();
        PlayNextCommotion();
        StartCoroutine(Ambience());
    }

    public void StartBreach()
    {
        breachSource.clip = breachSequence;
        breachSource.Play();
    }

    private void PlayNextCommotion()
    {
        if (currentCommotionIndex < commotion.Length)
        {
            commotionSource.clip = commotion[currentCommotionIndex];
            commotionSource.Play();
            currentCommotionIndex++;
            Invoke("PlayNextCommotion", 10f);
        }
        else
        {
            commotionSource.Stop();
        }
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