using System.Linq;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Controller : MonoBehaviour
{

    Rigidbody2D rb2d;
    CapsuleCollider2D col;
    float lastJumpTime;

    public bool IsGrounded
    {
        get
        {
            if (rb2d == null)
                return false;
            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position - Vector3.up * (col.bounds.extents.y), col.bounds.extents.x);
            return cols.Any(col =>col.transform.gameObject.layer == LayerMask.NameToLayer("Ground"));
        }
    }
    
    public bool CanJump
    {
        get
        {
            if (jumpCoolTime < 0)
                return false;
            return IsGrounded && Time.time - lastJumpTime >= jumpCoolTime;            
        }
    }

    [SerializeField] float jumpCoolTime = -1f;
    
    public virtual void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
    }

    public virtual void Update()
    {
        
    }

    public void SetJumpCoolTime(float value)
    {
        jumpCoolTime = value;
    }
    
    public virtual void Move(float h, float speed)
    {
        rb2d.velocity = new Vector2(h * speed * 0.1f, rb2d.velocity.y);
    }

    public virtual void Jump(float power)
    {
        if (!CanJump)
            return;
        
        lastJumpTime = Time.time;
        rb2d.AddForce(Vector2.up * power, ForceMode2D.Impulse);
    }
}