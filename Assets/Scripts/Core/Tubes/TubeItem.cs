using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class TubeItem : MonoBehaviour, IInteractable
{
    static Sprite tubeItemSprite = null;
    Tube tube;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        if (tubeItemSprite == null)
        {
            tubeItemSprite = Resources.Load<Sprite>("Textures/ItemTube");
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    public TubeItem Init(Tube tube)
    {
        this.tube = tube;
        spriteRenderer.sprite = tubeItemSprite;
        spriteRenderer.sortingOrder = 15;
        return this;
    }

    public TubeItem Drop(Vector2 position)
    {
        position.y += 0.1f;
        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.down, 5000f, 1 << LayerMask.NameToLayer("Ground"));
        if (hit)
        {
            transform.position = hit.point;
        }
        else
        {
            Debug.LogWarning("아이템으로 드롭된 " + tube.NameKor + " 튜브가 땅을 못찾았어요");
        }
        return this;
    }

    public void Interact()
    {
        
    }

    public void Contact()
    {
        
    }
}
