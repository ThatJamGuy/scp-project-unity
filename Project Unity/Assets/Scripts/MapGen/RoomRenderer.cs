using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public Transform player;
    public float disableRange = 10f;
    private List<GameObject> rooms = new List<GameObject>();

    void Start()
    {
        StartCoroutine(FindRooms());
    }

    IEnumerator FindRooms()
    {
        yield return new WaitForSeconds(1f);
        GameObject[] roomObjs = GameObject.FindGameObjectsWithTag("Room");
        foreach (var obj in roomObjs)
        {
            if (!rooms.Contains(obj))
            {
                rooms.Add(obj);
            }
        }
    }

    void Update()
    {
        foreach (var room in rooms)
        {
            float distance = Vector3.Distance(player.position, room.transform.position);
            if (distance > disableRange)
            {
                room.SetActive(false);
            }
            else
            {
                room.SetActive(true);
            }
        }
    }
}