using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCM<TGridObject>
{
    private int width;
    private int height;
    private float cellSize;
    private TGridObject[,] gridArray;
    private Vector3 originPosition;

    public GridCM(int width, int height, float cellSize, Vector3 originPosition, Func<GridCM<TGridObject>, int, int, bool, TGridObject> createGridObject){
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        gridArray = new TGridObject[width, height];

        int layerId = 9;
        int layerMask = 1 << layerId;
        for (int x = 0; x < gridArray.GetLength(0); x++){
            for (int y = 0; y < gridArray.GetLength(1); y++){
                Vector3 center = GetWorldPosition(x, y);

                var ground = Physics2D.OverlapBox(new Vector2(center.x + 0.5f, center.y + 0.5f), new Vector2(0.9f, 0.9f), 0f, layerMask);
                bool walkable = !ground;
                gridArray[x, y] = createGridObject(this, x, y, walkable);
                if(!walkable){
                    Debug.Log("Node " + x + " " + y + " is unwalkable");
                }
                else{
                    Debug.Log("Node " + x + " " + y + " is walkable");
                }
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y+1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x+1, y),Color.white, 100f);
            }
        }
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height),Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height),Color.white, 100f);
    }

    public int GetWidth(){
        return width;
    }
    public int GetHeight(){
        return height;
    }
    public float GetCellSize(){
        return cellSize;
    }

    private Vector3 GetWorldPosition(int x, int y){
        return new Vector3(x, y) * cellSize + originPosition;
    }

    public void GetXY(Vector3 worldPosition, out int x, out int y){
        x = Mathf.FloorToInt((worldPosition - originPosition).x/ cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y/ cellSize);
    }

    public TGridObject GetValue(int x, int y){
        if(x >= 0 && y >= 0 && x < width && y < height){
            return gridArray[x, y];
        }
        else{
            return default(TGridObject);
        }
    }
}
