using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public bool IsGrounded
    {
        get
        {
            if (rigidBody2D == null)
                return false;

            RaycastHit2D[] hits = Physics2D.LinecastAll(transform.position, transform.position + Vector3.down * 1.1f);
            return hits.Any(hit => hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"));
        }
    }

    public bool CanJump
    {
        get { return IsGrounded && Time.time - lastJumpTime >= jumpCoolTime; }
    }

    [SerializeField] [Range(1f, 5f)] float speed;
    [SerializeField] [Range(5f, 15f)] float jumpPower;
    [SerializeField] [Range(0.1f, 1f)] float jumpCoolTime;


    float lastJumpTime;
    
    void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && CanJump)
        {
            Jump();
        }
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

    void Jump()
    {
        lastJumpTime = Time.time;
        rigidBody2D.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }

}
