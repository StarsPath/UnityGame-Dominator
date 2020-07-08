using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public List<Node> neighbours;
    public int cost;
    public int x;
    public int y;
    public Node()
    {
        cost = 0;
        neighbours = new List<Node>();
    }
    public float distanceTo(Node n)
    {
        return Vector2Int.Distance(new Vector2Int(x, y), new Vector2Int(n.x, n.y));
    }
}
