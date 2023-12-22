using UnityEngine;

public class GroundDetect : MonoBehaviour
{
    public AudioClip[] normalWalkSFX;
    public AudioSource footSource;

    bool canStep = true;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "RegularSurface" && canStep)
        {
            int rand = Random.Range(0, normalWalkSFX.Length);
            footSource.clip = normalWalkSFX[rand];
            footSource.Play();

            canStep = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        canStep = true;
    }
}