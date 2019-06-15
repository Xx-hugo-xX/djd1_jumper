using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InvertTilemap : MonoBehaviour
{
    [ContextMenu("Invert")]
    void Invert()
    {
        Debug.Log("Inverting");
        Tilemap tilemap = GetComponent<Tilemap>();

        //Debug.Log("P = " + tilemap.WorldToCell(pos.position));

        for (int y = 0; y < tilemap.size.y; y++)
        {
            for (int x = 0; x < tilemap.size.x / 2; x++)
            {
                var tile1 = tilemap.GetTile(new Vector3Int(tilemap.origin.x + x, tilemap.origin.y + y, 0));
                var tile2 = tilemap.GetTile(new Vector3Int(tilemap.origin.x + tilemap.size.x - x - 1, tilemap.origin.y + y, 0));

                tilemap.SetTile(new Vector3Int(tilemap.origin.x + x, tilemap.origin.y + y, 0), tile2);
                tilemap.SetTile(new Vector3Int(tilemap.origin.x + tilemap.size.x - x - 1, tilemap.origin.y + y, 0), tile1);
            }
        }

        tilemap.RefreshAllTiles();
    }
}
