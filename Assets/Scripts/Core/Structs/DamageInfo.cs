using UnityEngine;

public struct DamageInfo
{
    LivingEntity from;
    float damage;
    Vector2 dir;
    
    public LivingEntity From { get { return from; } }
    public float Damage { get { return damage; } }
    public Vector2 Dir { get { return dir; } }
    
    public DamageInfo(LivingEntity from, float damage, Vector2 origin, Vector2 hit)
    {
        this.from = from;
        this.damage = damage;
        dir = (hit - origin).normalized;
    }
}
