using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


public class Node
{
    public Vector2Int position;
    public float gCost, hCost;

    public float fCost { get { return gCost + hCost; } }

    public List<Vector2Int> neighbours;

    public Node parent;
    public bool IsWalkable;

    public Node(Vector2Int position, bool IsWalkable)
    {
        this.position = position;
        this.IsWalkable = IsWalkable;
    }
}


public class AStarPathfinding
{
    GridManager grid;
    Node currentNode;
    Node StartNode, EndNode;

    List<Node> openSet;
    HashSet<Node> closedSet;

    bool success = false;


    public AStarPathfinding(GridManager grid) 
    {
        this.grid = grid;
        openSet = new List<Node>();
        closedSet = new HashSet<Node>();
    }

    public List<Node> FindPath(Vector2Int srcPos , Vector2Int desPos)
    {
        StartNode = grid.ConvertToNodeFromWorldPos(srcPos);
        EndNode = grid.ConvertToNodeFromWorldPos(desPos);

        List<Node> path = new List<Node>();

        if (StartNode == null || EndNode == null)
            return null;

        openSet.Add(StartNode);
        openSet[0].gCost = 0;
        openSet[0].hCost = CalculateHeuristicCost(StartNode, EndNode);

        Debug.Log("openSet[0].hCost===" + openSet[0].hCost);

        while(openSet.Count > 0)
        {
            currentNode = openSet[0];
            for(int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost <= currentNode.fCost)
                    currentNode = openSet[i];
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == EndNode)
            {
                success = true;
                break;
            }

            foreach(Node n in grid.GenerateNeighbours(currentNode))
            {
                if (!n.IsWalkable)
                    continue;

                float cost = currentNode.gCost + CalculateHeuristicCost(currentNode, n);

                Debug.Log("Current cost====" + cost);

                if (openSet.Contains(n) && cost < n.gCost)
                    openSet.Remove(n);
                if (closedSet.Contains(n) && cost < n.gCost)
                    closedSet.Remove(n);

                if(!closedSet.Contains(n) && !openSet.Contains(n))
                {
                    n.gCost = cost;
                    n.hCost = CalculateHeuristicCost(n, EndNode);
                    n.parent = currentNode;
                    openSet.Add(n);
                }
            }
        }
        if (success)
        {
            path = RetracePath(StartNode,EndNode);
        }

        return path;

    }

    public List<Node> RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Reverse();

        return path;

    }

    float CalculateHeuristicCost(Node startNode, Node endNode)
    {
        int dstX = Mathf.Abs(startNode.position.x - endNode.position.x);
        int dstY = Mathf.Abs(startNode.position.y - endNode.position.y);


        //if (dstX < dstY)
        //    return 14 * dstX + 10 * (dstY - dstX);
        //return 14 * dstY + 10 * (dstX - dstY);

        return Mathf.Sqrt((dstX * dstX) + (dstY * dstY));
    }
}
