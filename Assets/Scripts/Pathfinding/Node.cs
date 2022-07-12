using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool walkable;
    public Vector2 position2D;
    public int gridX;
    public int gridY;

    public int gCost;
    public int hCost;

    public Node parent;
    public Node(bool walkable, Vector2 position, int gridX, int gridY)
    {
        this.walkable = walkable;
        this.position2D = position;
        this.gridX = gridX;
        this.gridY = gridY; 
    }

    public int fCost
    {
        get { return gCost + hCost; }
    }
}
