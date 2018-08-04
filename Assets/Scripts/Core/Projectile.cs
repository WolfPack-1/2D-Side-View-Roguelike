using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    static PhysicsMaterial2D projectileMaterial;
    
    Rigidbody2D rigid;
    CircleCollider2D circleCollider;
    SpriteRenderer spriteRenderer;
    
    [SerializeField] float speed;
    [SerializeField] float damage;
    [SerializeField] LivingEntity owner;
    
    public float Speed { get { return speed; } }
    public float Damage { get { return damage; } }
    public LivingEntity Owner { get { return owner; } }

    List<Transform> onHitFxs;

    void Awake()
    {
        if (projectileMaterial == null)
            projectileMaterial = Resources.Load<PhysicsMaterial2D>("Materials/Projectile");
        rigid = GetComponent<Rigidbody2D>();
        if (rigid == null)
        {
            rigid = gameObject.AddComponent<Rigidbody2D>();
            rigid.sharedMaterial = projectileMaterial;
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
//            // 임시로 Knob 사용
//            spriteRenderer.sprite = Resources.GetBuiltinResource<Sprite>("UI/Skin/Knob.psd");
            spriteRenderer.sortingOrder = 500;
        }
        
        circleCollider = GetComponent<CircleCollider2D>();
        if (circleCollider == null)
        {
            circleCollider = gameObject.AddComponent<CircleCollider2D>();
        }

        rigid.bodyType = RigidbodyType2D.Dynamic;
    }

    public void Init(float damage, float speed, LivingEntity owner)
    {
        this.damage = damage;
        this.speed = speed;
        this.owner = owner;
    }

    public void Fire(Vector3 target)
    {
        rigid.gravityScale = 0;
        Vector2 diriection = (target - transform.position).normalized;
        rigid.AddForce(diriection * speed, ForceMode2D.Impulse);
    }

    public void Launch(Vector3 target)
    {
        GameObject bounceHelper = new GameObject("Bounce Helper");
        bounceHelper.AddComponent<CircleCollider2D>().radius = circleCollider.radius;
        bounceHelper.layer = LayerMask.NameToLayer("Bounce");
        bounceHelper.transform.SetParent(transform, false);
        
        rigid.gravityScale = 1;
        Vector3 velocity = CalculateThrowVelocity(transform.position, target);
        rigid.AddForce(velocity, ForceMode2D.Impulse);
    }

    public static Projectile Create(Vector2 position, float damage, float speed, LivingEntity owner, GameObject fx)
    {
        // Todo : Sprite를 인자로 받아서 처리, 필요하다면 오브젝트풀 사용
        Projectile projectile = fx.AddComponent<Projectile>();
        projectile.Init(damage, speed, owner);
        projectile.transform.position = position;
        projectile.CalculateCircleCollider();
        return projectile;
    }

    public Projectile SetHitFx(List<Transform> fxs)
    {
        onHitFxs = fxs;
        return this;
    }

    public static Projectile Create()
    {
        return Create(Vector2.zero, 0, 0, null, new GameObject("Proejctile"));
    }

    void CalculateCircleCollider()
    {
        circleCollider.isTrigger = true;
        Vector3 spriteHalfSize = spriteRenderer.sprite.bounds.extents;
        circleCollider.radius = spriteHalfSize.x > spriteHalfSize.y ? spriteHalfSize.x : spriteHalfSize.y;
    }
    
    Vector3 CalculateThrowVelocity(Vector3 origin, Vector3 target)
    {
        Vector3 toTarget = target - origin;
        float distance = toTarget.magnitude + 10;
        float gSquared = Physics.gravity.sqrMagnitude;
        float b = distance * distance + Vector3.Dot(toTarget, Physics.gravity);    
        float discriminant = b * b - gSquared * toTarget.sqrMagnitude;
        
        if (discriminant < 0)
            return Vector2.zero;

        float discRoot = Mathf.Sqrt(discriminant);
        float T_max = Mathf.Sqrt((b + discRoot) * 2f / gSquared);
        float T_min = Mathf.Sqrt((b - discRoot) * 2f / gSquared);
        float T_lowEnergy = Mathf.Sqrt(Mathf.Sqrt(toTarget.sqrMagnitude * 4f/gSquared));

        float t = T_lowEnergy;
        return toTarget / t - Physics.gravity * t / 2f;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Transform fx;
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            if (onHitFxs != null)
            {
                fx = Instantiate(onHitFxs[Random.Range(0, onHitFxs.Count)], transform.position, Quaternion.identity);
                fx.localScale = owner.transform.localScale;   
            }
            Destroy(gameObject);
        }

        LivingEntity livingEntity = other.GetComponent<LivingEntity>();
        if(!livingEntity)
            return;

        if (owner.GetType() == typeof(Player) && livingEntity.GetType() != typeof(Player))
        {
            livingEntity.GetDamaged(new DamageInfo(owner, damage, transform.position, livingEntity.transform.position));
            Destroy(gameObject);
        }
        else if (owner.GetType() != typeof(Player) && livingEntity.GetType() == typeof(Player))
        {
            livingEntity.GetDamaged(new DamageInfo(owner, damage, transform.position, livingEntity.transform.position));
            Destroy(gameObject);
        }

        if (onHitFxs == null)
            return;
        fx = Instantiate(onHitFxs[Random.Range(0, onHitFxs.Count)], transform.position, Quaternion.identity);
        fx.localScale = owner.transform.localScale;
    }
}
