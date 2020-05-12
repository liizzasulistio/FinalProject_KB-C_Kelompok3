using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding
{
    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;
    private GridCM<Node> grid;
    private List<Node> openList;
    private List<Node> closedList;
    private Vector3 originPos;

    public PathFinding(int width, int height){
        originPos = GameObject.Find("originPos").transform.position;
        grid = new GridCM<Node>(width, height, 1f, originPos, (GridCM<Node> grid, int x, int y, bool isWalkable) => new Node(grid, x, y, isWalkable)); 
    }
    public List<Vector3> FindPath(Vector3 startWorldPosition, Vector3 endWorldPosition){
        grid.GetXY(startWorldPosition, out int startX, out int startY);
        grid.GetXY(endWorldPosition, out int endX, out int endY);

        Node startNode = GetNode(startX, startY);
        Node endNode = GetNode(endX, endY);

        if(startNode == null || endNode == null){
            return null;
        }

        openList = new List<Node> {startNode};
        closedList = new List<Node>();

        for (int x = 0; x < grid.GetWidth(); x++){
            for (int y = 0; y < grid.GetHeight();y++){
                Node node = GetNode(x, y);
                node.gCost = int.MaxValue;
                node.CalculateFCost();
                node.cameFromNode = null;
            }
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistance(startNode, endNode);
        startNode.CalculateFCost();

        while(openList.Count > 0){
            Node currentNode = GetLowestFCostNode(openList);
            if (currentNode == endNode){
                List<Node> path = CalculatePath(endNode);
                List<Vector3> vectorPath = new List<Vector3>();
                //int idx = 0;
                foreach(Node node in path){
                    vectorPath.Add(new Vector3(node.x, node.y) * grid.GetCellSize() + Vector3.one * grid.GetCellSize() * .5f + originPos);
                //Debug.Log("Node " + idx + " " + vectorPath[idx].x + " " + vectorPath[idx].y);
                //idx++;
                }
                return vectorPath;
            }
            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (Node neighbourNode in GetNeighbourList(currentNode)){
                if(closedList.Contains(neighbourNode)) continue;
                if(!neighbourNode.isWalkable){
                    closedList.Add(neighbourNode);
                    continue;
                }

                int tentativeGCost = currentNode.gCost + CalculateDistance(currentNode, neighbourNode);
                if(tentativeGCost < neighbourNode.gCost){
                    neighbourNode.cameFromNode = currentNode;
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.hCost = CalculateDistance(neighbourNode, endNode);
                    neighbourNode.CalculateFCost();

                    if(!openList.Contains(neighbourNode)){
                        openList.Add(neighbourNode);
                    }
                }
            }
        }
        // out of nodes on the open list
        return null;
    }

    private List<Node> GetNeighbourList(Node currentNode){
        List<Node> neighbourList = new List<Node>();

        if(currentNode.x - 1 >= 0){
            //left
            neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y));
            //left down
            if(currentNode.y - 1 >= 0)
                neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y - 1));
            // left up
             if(currentNode.y + 1 < grid.GetHeight())
                neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y + 1));
        }
        if(currentNode.x + 1 < grid.GetWidth()){
            //right
            neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y));
            //right down
            if(currentNode.y - 1 >= 0)
                neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y - 1));
            // right up
             if(currentNode.y + 1 < grid.GetHeight())
                neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y + 1));
        }
        // down
        if(currentNode.y - 1 >= 0)
            neighbourList.Add(GetNode(currentNode.x, currentNode.y - 1));
        // up
         if(currentNode.y + 1 < grid.GetHeight())
                neighbourList.Add(GetNode(currentNode.x, currentNode.y + 1));

        return neighbourList;
    }

    private Node GetNode(int x, int y){
        return grid.GetValue(x, y);
    }

    private List<Node> CalculatePath(Node endNode){
        List<Node> path = new List<Node>();
        path.Add(endNode);
        Node currentNode = endNode;
        while(currentNode.cameFromNode != null){
            path.Add(currentNode.cameFromNode);
            currentNode = currentNode.cameFromNode;
        }
        path.Reverse();

        // int idx = 0;
        // foreach (Node i in path){
        //     Debug.Log("node " + idx + " pos " + i.x + " " + i.y + " with fCost " + i.fCost + " gCost " + i.gCost + " hCost " + i.hCost);
        //     idx++;
        // }
        return path;
    }

    private int CalculateDistance(Node a, Node b){
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDistance - yDistance);
        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
    }

    private Node GetLowestFCostNode(List<Node> nodeList){
        Node lowestFCostNode = nodeList[0];
        for(int i = 1; i < nodeList.Count; i++){
            if(nodeList[i].fCost < lowestFCostNode.fCost){
                lowestFCostNode = nodeList[i];
            }
        }
        return lowestFCostNode;
    }
}
