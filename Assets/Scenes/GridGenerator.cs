using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridGenerator : MonoBehaviour
{
    public int rows = 5; // The number of rows in the grid
    public int columns = 5; // The number of columns in the grid
    public Tilemap tilemap; // Reference to the Tilemap component
    public TileBase tile; // The Tile to be used for the grid

    private void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        tilemap.ClearAllTiles(); // Clear any existing tiles from the Tilemap

        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                Vector3Int tilePosition = new Vector3Int(x, y, 0);
                tilemap.SetTile(tilePosition, tile);
            }
        }
    }
    
    public Vector3 GetGridCenter()
    {
        Vector3 cellSize = tilemap.cellSize;
        Vector3 gridSize = new Vector3(columns * cellSize.x, rows * cellSize.y, 0f);
        return transform.position - gridSize * 0.5f;
    }
}
