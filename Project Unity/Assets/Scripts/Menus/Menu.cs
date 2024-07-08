using UnityEngine;

// Customizable menu script for easy menu creation and such
public class Menu : MonoBehaviour
{
    public bool isEnabled;

    [Header("Menu Options")]
    [SerializeField] private bool pausesTheGame;
}