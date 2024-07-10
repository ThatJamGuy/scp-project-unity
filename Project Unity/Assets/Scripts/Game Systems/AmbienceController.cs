using System.Collections;
using UnityEngine;

public class AmbienceController : MonoBehaviour
{
    [Header("Ambience Clips")]
    [SerializeField] AudioClip[] ambience;
    [SerializeField] AudioClip[] commotion;

    [Header("Ambiance Sources")]
    [SerializeField] AudioSource ambienceSource;
    [SerializeField] AudioSource commotionSource;

    private int currentCommotionIndex = 0;

    private void Start()
    {
        PlayNextCommotion();
        StartCoroutine(Ambience());
    }

    private void PlayNextCommotion()
    {
        if (currentCommotionIndex < commotion.Length)
        {
            commotionSource.clip = commotion[currentCommotionIndex];
            commotionSource.Play();
            currentCommotionIndex++;
            Invoke(nameof(PlayNextCommotion), 15f);
        }
        else
        {
            commotionSource.Stop();
        }
    }

    IEnumerator Ambience()
    {
        while (true)
        {
            if (ambience != null && ambience.Length > 0)
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
