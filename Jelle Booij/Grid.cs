using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{

    [SerializeField]
    private bool DrawGrid;


    private Node[,] grid;

    public float tileSize { get; private set; }
    public int numTilesSqrt { get; private set; }
    public float gridSize { get; private set; }
    public float toGridSizeMult
    {

        get
        {
            return gridSize / numTilesSqrt;
        }

        private set { }

    }

    public Vector2Int mouseGridPosition {get; private set;}
    
    private Vector3 startPos;


    private void Start(){
        GenerateGrid(50, 35);
    }

    private void Update(){
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float enter = 0.0f;

        if (plane.Raycast(ray, out enter)){
            Vector3 hitPoint = ray.GetPoint(enter);
            mouseGridPosition = WorldToGridPosition(hitPoint);
        }
    }

    void GenerateGrid(float gridSize, int numTilesSqrt){

        this.gridSize = gridSize;
        this.numTilesSqrt = numTilesSqrt;
        this.tileSize = gridSize / numTilesSqrt;

        grid = new Node[numTilesSqrt, numTilesSqrt];

        startPos = new Vector3(-(gridSize / 2) + (tileSize / 2), 0, -(gridSize / 2) + (tileSize / 2));

        for (int x = 0; x < numTilesSqrt; x++){
            for (int y = 0; y < numTilesSqrt; y++){
                grid[x, y] = new Node();
                grid[x, y].position = startPos + (Vector3.forward * tileSize * y) + (Vector3.right * tileSize * x);
                grid[x, y].gridPosition = new Vector2Int(x, y);
            }
        }

    }


    #region CONVERSION METHODS
    //==============================================================
    public Vector2Int WorldToGridPosition(Vector3 worldPosition){
        Vector3 relativePos = worldPosition - startPos;

        float pX = relativePos.x / gridSize;
        float pY = relativePos.z / gridSize;

        float x = Mathf.Clamp(Mathf.RoundToInt(pX * numTilesSqrt), 0, numTilesSqrt - 1);
        float y = Mathf.Clamp(Mathf.RoundToInt(pY * numTilesSqrt), 0, numTilesSqrt - 1);

        return new Vector2Int((int)x, (int)y);
    }


    public Vector3 GridToWorldPosition(Vector2Int gridPosition){
        Vector3 pos = new Vector3();

        pos.x = startPos.x + (gridPosition.x * tileSize);
        pos.z = startPos.z + (gridPosition.y * tileSize);

        return pos;
    }


    public float WorldToGridSize(float worldSize){
        return worldSize * (gridSize / numTilesSqrt);
    }
    //==============================================================
    #endregion

    public Node GetNode(Vector2Int gridPosition){
        return grid[gridPosition.x, gridPosition.y];
    }


    public Node[,] GetNeighbours(Vector2Int nodePos, Vector2Int expansion){

        expansion = new Vector2Int(Mathf.Abs(expansion.x), Mathf.Abs(expansion.y));

        Vector2Int origin = nodePos - new Vector2Int((int)(expansion.x - 1) / 2, (int)(expansion.y - 1) / 2);

        int sx = ((origin.x + expansion.x > numTilesSqrt) ? (numTilesSqrt - origin.x) : expansion.x) - ((origin.x < 0) ? origin.x : 0);
        int sy = (origin.y + expansion.y > numTilesSqrt) ? (numTilesSqrt - origin.y) : expansion.y;

        Node[,] temp = new Node[sx, sy];

        for (int x = 0; x < sx; x++){
            for (int y = 0; y < sy; y++){
                int nx = Mathf.Clamp(origin.x + x, 0, numTilesSqrt - 1);
                int ny = Mathf.Clamp(origin.y + y, 0, numTilesSqrt - 1);
                temp[x, y] = grid[nx, ny];
            }
        }

        return temp;
    }


    public bool Overlap(Node[,] nodes){

        for (int x = 0; x < nodes.GetLength(0); x++){
            for (int y = 0; y < nodes.GetLength(1); y++){
                if (nodes[x, y].Taken){
                    return true;
                }
            }
        }

        return false;
    }


    public void SetTaken(Node[,] nodes){

        for (int x = 0; x < nodes.GetLength(0); x++){
            for (int y = 0; y < nodes.GetLength(1); y++){
                nodes[x, y].Taken = true;
            }
        }
    }


    private void OnDrawGizmos(){

        if (grid == null || DrawGrid == false) return;

        for (int x = 0; x < grid.GetLength(0); x++){
            for (int y = 0; y < grid.GetLength(1); y++){
                Gizmos.color = Color.black;
                Gizmos.DrawWireCube(grid[x, y].position, tileSize * new Vector3(1, 0, 1));
                Gizmos.color = (grid[x, y].Taken) ? Color.red : Color.white;
                Gizmos.DrawCube(grid[x, y].position, tileSize * new Vector3(1, 0.07f, 1));
            }
        }
    }

}
