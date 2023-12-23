using UnityEngine;

public class DoorButton : MonoBehaviour
{
    [SerializeField] AudioSource buttonSFX;
    public bool doorButton = true;
    public float interactCooldown;

    private void Update()
    {
        interactCooldown -= Time.deltaTime;
    }

    public void UseButton()
    {
        if(interactCooldown < 0)
            buttonSFX.Play(); interactCooldown = 0.1f;
    }
}