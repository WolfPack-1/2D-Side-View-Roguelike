using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rigid;
    CircleCollider2D circleCollider;
    SpriteRenderer spriteRenderer;
    
    [SerializeField] float speed;
    [SerializeField] float damage;
    [SerializeField] LivingEntity owner;
    
    public float Speed { get { return speed; } }
    public float Damage { get { return damage; } }
    public LivingEntity Owner { get { return owner; } }

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        if (rigid == null)
            rigid = gameObject.AddComponent<Rigidbody2D>();
        
        circleCollider = GetComponent<CircleCollider2D>();
        if (circleCollider == null)
        {
            circleCollider = gameObject.AddComponent<CircleCollider2D>();
            circleCollider.isTrigger = true;
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            // 임시로 Knob 사용
            spriteRenderer.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Knob.psd");
            spriteRenderer.sortingOrder = 500;
        }

        rigid.bodyType = RigidbodyType2D.Dynamic;
        rigid.gravityScale = 0;
    }

    public void Init(float damage, float speed, LivingEntity owner)
    {
        this.damage = damage;
        this.speed = speed;
        this.owner = owner;
    }

    public void Fire(Vector3 target)
    {
        Vector2 diriection = (target - transform.position).normalized;
        rigid.AddForce(diriection * speed, ForceMode2D.Impulse);
    }

    public static Projectile Create(Vector2 position, float damage, float speed, LivingEntity owner)
    {
        // Todo : Sprite를 인자로 받아서 처리, 필요하다면 오브젝트풀 사용
        Projectile projectile = new GameObject("Projectile").AddComponent<Projectile>();
        projectile.Init(damage,speed,owner);
        projectile.transform.position = position;
        return projectile;
    }
    
    public static Projectile Create()
    {
        return Create(Vector2.zero, 0, 0, null);
    }
}
