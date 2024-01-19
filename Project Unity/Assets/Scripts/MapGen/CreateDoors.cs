using System.Collections;
using UnityEngine;

public class CreateDoors : MonoBehaviour
{
    public GameObject prefab;
    public float cellSize;
    public int gridWidth;
    public int gridHeight;

    private void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                var cellCenter = new Vector3(x * cellSize, 0, y * cellSize);
                var cell = new Cell(cellCenter, cellSize);
                PlacePrefabOnCellEdges(cell);
            }
        }
    }

    private void PlacePrefabOnCellEdges(Cell cell)
    {
        var halfSize = cell.size / 2;
        var directions = new Vector3[]
        {
           new Vector3(-1, 0, 0), // Left
           new Vector3(1, 0, 0), // Right
           new Vector3(0, 0, -1), // Front
           new Vector3(0, 0, 1)  // Back
        };

        foreach (var direction in directions)
        {
            var position = cell.center + direction * halfSize;
            var rotation = Quaternion.LookRotation(direction);

            rotation *= Quaternion.Euler(0, 90, 0);

            RaycastHit hit;
            if (!Physics.Raycast(position, direction, out hit, cell.size) && hit.collider != null && hit.collider.gameObject.layer != LayerMask.NameToLayer("NoTouch"))
            {
                Instantiate(prefab, position, rotation);
            }
        }
    }
}

public class Cell
{
    public Vector3 center;
    public float size;

    public Cell(Vector3 center, float size)
    {
        this.center = center;
        this.size = size;
    }
}