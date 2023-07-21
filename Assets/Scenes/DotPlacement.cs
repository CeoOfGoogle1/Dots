using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DotPlacement : MonoBehaviour
{
    public GameObject dotPrefab;
    public Color dotColor = Color.blue;
    public Tilemap tilemap;

    private List<GameObject> dots = new List<GameObject>();
    private float tileSize;

    private void Start()
    {
        tileSize = tilemap.cellSize.x; // Assuming the tiles are square

        GenerateDots();
    }

    private void GenerateDots()
    {
        BoundsInt bounds = tilemap.cellBounds;

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                if (IsIntersection(x, y))
                {
                    Vector3 dotPosition = tilemap.GetCellCenterWorld(new Vector3Int(x, y, 0));
                    GameObject dot = Instantiate(dotPrefab, dotPosition, Quaternion.identity);
                    dot.GetComponent<SpriteRenderer>().color = dotColor;
                    dots.Add(dot);
                }
            }
        }
    }

    private bool IsIntersection(int x, int y)
    {
        TileBase tile = tilemap.GetTile(new Vector3Int(x, y, 0));

        if (tile != null)
        {
            TileBase tileAbove = tilemap.GetTile(new Vector3Int(x, y + 1, 0));
            TileBase tileBelow = tilemap.GetTile(new Vector3Int(x, y - 1, 0));
            TileBase tileLeft = tilemap.GetTile(new Vector3Int(x - 1, y, 0));
            TileBase tileRight = tilemap.GetTile(new Vector3Int(x + 1, y, 0));

            return (tileAbove != null && tileBelow != null && tileLeft != null && tileRight != null);
        }

        return false;
    }
}
