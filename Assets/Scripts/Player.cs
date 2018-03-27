using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    
/*
    Test
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
*/


    Rigidbody2D rigidBody2D;
    float h;


    [SerializeField] float speed;

    void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {        
        
    }

    void FixedUpdate()
    {
        h = Input.GetAxisRaw("Horizontal");
        Move(h);
    }

    void Move(float h)
    {
        rigidBody2D.velocity = new Vector2(h * speed, rigidBody2D.velocity.y);
    }
    
}
