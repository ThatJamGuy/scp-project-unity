using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCP_EventHandler : MonoBehaviour
{
    [Header("Simple Events")]
    public bool rotator;
    public float rotationSpeed = 30f;

    private void Update()
    {
        if(rotator)
        {
            transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
        }
    }
}