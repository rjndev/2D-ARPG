using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{

    public Transform seeker, target;


    WorldGrid grid;


    private void Update()
    {
        FindPath(new Vector2(seeker.position.x, seeker.position.y), new Vector2(target.position.x, target.position.y));
    }

    private void Awake()
    {
        grid = GetComponent<WorldGrid>();
    }
    void FindPath(Vector2 start, Vector2 target)
    {
        Node startNode = grid.NodeFromWorldPoint(start);
        Node targetNode = grid.NodeFromWorldPoint(target);

        List<Node> openSet = new List<Node> ();
        HashSet<Node> closedSet = new HashSet<Node> ();  
        openSet.Add(startNode);

        while(openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++) 
            { 
                if(openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
            }
            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if(currentNode == targetNode)
            {
                RetracePath(startNode, targetNode);
                return;
            }

            foreach(Node neighbour in grid.GetNeighbours(currentNode))
            {
                if (!neighbour.walkable || closedSet.Contains(neighbour))
                    continue;

                int newMovementCostToNeighbour = currentNode.gCost + GetDistanceNode(currentNode, neighbour);
                if(newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = GetDistanceNode(neighbour, targetNode);
                    neighbour.parent = currentNode;

                    if(!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }
    }

    void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while(currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Reverse();
        grid.path = path;
    }

    int GetDistanceNode(Node nodeA, Node nodeB)
    {
        int distance = 0;
        int y, x = 0;

        y = Mathf.Abs(nodeB.gridY - nodeA.gridY);
        x = Mathf.Abs(nodeB.gridX - nodeA.gridX);

        if (x > y)
        
            distance = 14 * y + 10 * (x - y);
        else
            distance = 14 * x + 10 * (y - x);

        return distance;
    }
}
