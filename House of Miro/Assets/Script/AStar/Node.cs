using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    private GridCM<Node> grid;
    public int x;
    public int y;
    public bool isWalkable;
    public int gCost;
    public int hCost;
    public int fCost;
    public Node cameFromNode;
    public Node(GridCM<Node> grid, int x, int y, bool isWalkable){
        this.grid = grid;
        this.x = x;
        this.y = y;
        this.isWalkable = isWalkable;
    }
    public void CalculateFCost(){
        fCost = gCost + hCost;
    }
}
