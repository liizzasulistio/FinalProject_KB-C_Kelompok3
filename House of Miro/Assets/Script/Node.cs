using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int gridX;
    public int gridY;
    public bool isWalkable;
    public Vector3 nodeWorldPos;

    public Node parentNode;

    public int gCost;
    public int hCost;

    public int FCost{
        get {
            return gCost + hCost;
        }
    }

    public Node(bool walkable, Vector3 pos, int gridx, int gridy){
        isWalkable = walkable;
        nodeWorldPos = pos;
        gridX = gridx;
        gridY = gridy;
    }
}
