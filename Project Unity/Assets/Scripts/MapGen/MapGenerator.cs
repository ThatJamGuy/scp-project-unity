using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    #region Variables

    public Vector2Int mapSize; // Size of the map, depicted as X and Y but generated on X and Z in 3D space
    public List<RoomTemplate> roomTemplates; // Room properties
    public int roomSize  = 20; // Size of the room prefabs (Globally), 20 by default

    private GameObject[,] rooms;

    public enum RoomType { ThreeWay, FourWay, Hallway, DeadEnd }

    [System.Serializable]
    public class RoomTemplate
    {
        public GameObject prefab; // Room prefab. Instantiated in the map
        public RoomType roomType; // Type of room. Determined by the script for how to place it in the map
        public List<Vector3> doorPositions; // Simply just entrances/exits for the room. Will be used to determined placement.
    }

    #endregion
}