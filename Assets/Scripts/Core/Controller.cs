using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(LivingEntity))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Controller : MonoBehaviour
{

    protected Rigidbody2D rb2d;
    CapsuleCollider2D col;
    float lastJumpTime;
    int dir;
    [SerializeField] float jumpCoolTime = -1f;
    Vector2 velocity;
    
    /// <summary>
    /// 왼쪽 -1 오른쪽 1
    /// </summary>
    public int Dir
    {
        get
        {
            if (dir != 0) return dir;
            Vector3 scale = transform.localScale;
            dir = scale.x > 0 ? -1 : 1;
            Flip(dir);
            return dir;
        }
    }

    /// <summary>
    /// 0 미만이면 점프 불가능
    /// </summary>
    public float JumpCoolTime { get { return jumpCoolTime; } }

    public bool IsGrounded
    {
        get
        {
            if (rb2d == null || velocity.y > 0)
                return false;
            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position - Vector3.up * (col.bounds.extents.y) + new Vector3(col.offset.x * -Dir, col.offset.y), col.bounds.extents.x * 0.9f, 1 << LayerMask.NameToLayer("Ground"));
            return cols.Any();
        }
    }

    public virtual bool CanJump
    {
        get
        {
            if (jumpCoolTime < 0)
                return false;
            return IsGrounded && Time.time - lastJumpTime >= jumpCoolTime;            
        }
    }
    
    public virtual void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
    }

    public virtual void Update()
    {
        
    }

    public virtual void FixedUpdate()
    {
        velocity = rb2d.velocity;
    }

    public void SetJumpCoolTime(float value)
    {
        jumpCoolTime = value;
    }
    
    public virtual void Move(int dir, float speed)
    {
        rb2d.velocity = new Vector2(dir * speed * 0.1f, rb2d.velocity.y);
        Flip(dir);
    }

    public virtual void Jump(float power)
    {
        if (!CanJump)
            return;
        
        lastJumpTime = Time.time;
        rb2d.AddForce(Vector2.up * power, ForceMode2D.Impulse);
    }
    
    
    public virtual void Flip(int dir)
    {
        if (!(dir == -1 || dir == 1))
            return;
        this.dir = dir;
        SetScaleByDir(dir);
    }

    public virtual void Flip()
    {
        dir = -dir;
        SetScaleByDir(dir);
    }

    void SetScaleByDir(int dir)
    {
        Vector3 scale = transform.localScale;
        float scaleX = Mathf.Abs(scale.x);
        scale.x = dir == 1 ? -scaleX : scaleX;
        transform.localScale = scale;
    }

    /// <summary>
    /// position이 자신보다 오른쪽에 있다면 1 왼쪽에 있다면 -1
    /// </summary>
    public int GetDir(Vector2 position)
    {
        Vector2 thisPosition = transform.position;
        return thisPosition.x > position.x ? -1 : 1;
    }
}