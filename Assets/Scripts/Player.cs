using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    
    Tilemap currentTilemap;
    
    void Awake()
    {
        currentTilemap = GameObject.Find("Grid").transform.Find("Ground").GetComponent<Tilemap>();
    }
    
    void OnCollisionEnter2D(Collision2D col)
    {
        Vector3 hitPosition = Vector3.zero;
        if (currentTilemap != null && currentTilemap.gameObject == col.gameObject)
        {
            foreach (ContactPoint2D hit in col.contacts)
            {
                hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
                hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
                Vector3Int cellPosition = currentTilemap.WorldToCell(hitPosition);
                CustomTile customTile = currentTilemap.GetTile<CustomTile>(cellPosition);
                if(customTile != null)
                    customTile.Action();
            }
        }
    }
}
