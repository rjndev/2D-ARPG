using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGrid : MonoBehaviour
{
    [SerializeField]
    private float UNIT_SIZE_X = .6f;
    [SerializeField]
    private float UNIT_SIZE_Y = .6f;


    public LayerMask unwalkableMask;
    public Vector2 gridSize;
    private Vector2 _position2D;
    public float nodeRadius;
    Node[,] grid;

    Vector2 gridBottomLeft;
    int gridSizeX, gridSizeY;

    private void Start()
    {
        _position2D = new Vector2(transform.position.x, transform.position.y);
        gridSizeX = Mathf.RoundToInt(gridSize.x / UNIT_SIZE_X);
        gridSizeY = Mathf.RoundToInt(gridSize.y / UNIT_SIZE_Y);
        Debug.DrawLine(new Vector3(0, 0, 0), new Vector3(20f, 0, 0), Color.red);
        CreateGrid();
    }

    private void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];

        gridBottomLeft = _position2D - (Vector2.right * gridSize.x / 2) - Vector2.up * gridSize.y/2;
        Debug.Log("New Grid [" + gridSizeX + ", " + gridSizeY + "]");

        for(int x = 0; x < gridSizeX; x++)
        {
            for(int y = 0; y < gridSizeY; y++)
            {
                Vector2 worldPoint = gridBottomLeft + Vector2.right * (x * UNIT_SIZE_X + UNIT_SIZE_X/2) + Vector2.up * (y * UNIT_SIZE_Y + UNIT_SIZE_Y/2);
                bool walkable = !(Physics2D.OverlapBox(worldPoint, new Vector2(UNIT_SIZE_X, UNIT_SIZE_Y), 0, unwalkableMask));
                grid[x, y] = new Node(walkable, worldPoint, x, y);
            }
        }
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for(int x = -1; x <= 1; x++)
        {
            for(int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;    

                if(checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }

    public Node NodeFromWorldPoint(Vector2 gridPos)
    {
        float percentX = (gridPos.x + gridSize.x / 2) / gridSize.x;
        float percentY = (gridPos.y + gridSize.y / 2) / gridSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        return grid[x, y];
    }

    public List<Node> path;
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector2(gridSize.x, gridSize.y) );
       

        if(grid != null)
        {
            foreach( Node n in grid)
            {
                Gizmos.color = n.walkable ? Color.white : Color.red;
                if (path != null)
                    if (path.Contains(n))
                        Gizmos.color = Color.green;
                Gizmos.DrawWireCube(n.position2D, new Vector2(UNIT_SIZE_X, UNIT_SIZE_Y) );
            }
        }

    }

}
