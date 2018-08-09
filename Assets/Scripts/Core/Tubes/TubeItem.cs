using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class TubeItem : MonoBehaviour, IInteractable
{
    static Sprite tubeItemSprite;
    static GameManager gameManager;
    [SerializeField] int cid = 0; // Todo : Editor로 빼기
    Tube tube;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;

    bool canInteractable;
    public bool CanInteractable { get { return canInteractable;} private set { canInteractable = value; } }

    void Awake()
    {
        if (tubeItemSprite == null)
        {
            tubeItemSprite = Resources.Load<Sprite>("Textures/ItemTube");
        }

        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = tubeItemSprite;
        spriteRenderer.sortingOrder = 15;
        
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.size = spriteRenderer.bounds.size;
    }

    void Start()
    {
        if (cid != 0)
        {
            Tube tube = gameManager.FindTubeByCid(cid);
            if (tube != null)
                Init(tube).Drop(transform.position);
        }
    }
    
    public TubeItem Init(Tube tube)
    {
        this.Log(tube.NameKor + " : Init Tube");
        gameManager.name = tube.NameKor;
        this.tube = tube;
        CanInteractable = true;
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
            this.Log("아이템으로 드롭된 " + tube.NameKor + " 튜브가 땅을 못찾았어요");
        }
        return this;
    }

    public void Interact(Player player)
    {
        if(!CanInteractable)
            return;
        
        player.GetTube(tube);
        Destroy(gameObject);
        CanInteractable = false;
    }

    public void Contact()
    {
        if(!CanInteractable)
            return;
        gameManager.SetInteractableIcon(transform.position);
    }

    public void Reset()
    {
        gameManager.ResetInteractableIcon();
    }
}
