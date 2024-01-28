using UnityEngine;

public class GroundDetect : MonoBehaviour
{
    public Player player;
    public AudioClip[] normalWalkSFX;
    public AudioClip[] normalRunSFX;
    public AudioClip[] metalWalkSFX;
    public AudioClip[] metalRunSFX;
    public AudioSource footSource;

    bool canStep = true;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "RegularSurface" && canStep)
        {
            if (player.isSprinting)
            {
                int rand = Random.Range(0, normalRunSFX.Length);
                footSource.clip = normalRunSFX[rand];
                footSource.Play();

                canStep = false;
            }
            else
            {
                int rand = Random.Range(0, normalWalkSFX.Length);
                footSource.clip = normalWalkSFX[rand];
                footSource.Play();

                canStep = false;
            }
        }
        if (collision.collider.tag == "MetalSurface" && canStep)
        {
            if (player.isSprinting)
            {
                int rand = Random.Range(0, metalRunSFX.Length);
                footSource.clip = metalRunSFX[rand];
                footSource.Play();

                canStep = false;
            }
            else
            {
                int rand = Random.Range(0, metalWalkSFX.Length);
                footSource.clip = metalWalkSFX[rand];
                footSource.Play();

                canStep = false;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        canStep = true;
    }
}