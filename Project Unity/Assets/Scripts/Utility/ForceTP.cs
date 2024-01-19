using UnityEngine;

public class ForceTP : MonoBehaviour
{
    public bool checkTheBoxToTurnOn;
    public string targetTag = "TargetTag";

    void Start()
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(targetTag);

        foreach (GameObject obj in objectsWithTag)
        {
            BringObjectToPosition(obj);
        }
    }

    void BringObjectToPosition(GameObject obj)
    {
        obj.transform.position = transform.position;
    }
}