using System;
using System.Diagnostics;

#region Interface

public interface IAttackType
{
    
}

#endregion


#region Structs

[Serializable]
public struct AttackTypeMelee : IAttackType
{
    int attackRange;
    int damage;
    float coolTime;

    public int AttackRange { get { return attackRange; } }
    public int Damage { get { return damage; } }
    public float CoolTime { get { return coolTime; } }

    public AttackTypeMelee(string[] data)
    {
        Debug.Assert(data.Length == 4);
        attackRange = int.Parse(data[1]);
        damage = int.Parse(data[2]);
        coolTime = float.Parse(data[3]);
    }
}

[Serializable]
public struct AttackTypeRange : IAttackType
{
    int minAttackRange;
    int maxAttackRange;
    int damage;
    float coolTime;
    TargetEnum target;

    public int MinAttackRange { get { return minAttackRange; } }
    public int MaxAttackRange { get { return maxAttackRange; } }
    public int Damage { get { return damage; } }
    public float CoolTime { get { return coolTime; } }
    public TargetEnum Target { get { return target; } }
    
    public AttackTypeRange(string[] data)
    {
        Debug.Assert(data.Length == 6);
        minAttackRange = int.Parse(data[2]);
        maxAttackRange = int.Parse(data[3]);
        damage = int.Parse(data[4]);
        coolTime = float.Parse(data[5]);
        target = (TargetEnum)Enum.Parse(typeof(TargetEnum),data[6]);
    }
}

[Serializable]
public struct AttackTypeAngle : IAttackType
{
    int minAttackRange;
    int maxAttackRange;
    int attackAngle;
    int damage;
    float coolTime;
    TargetEnum target;

    public int MinAttackRange { get { return minAttackRange; } }
    public int MaxAttackRange { get { return maxAttackRange; } }
    public int AttackAngle { get { return attackAngle; } }
    public int Damage { get { return damage; } }
    public float CoolTime { get { return coolTime; } }
    public TargetEnum Target { get { return target; } }

    public AttackTypeAngle(string[] data)
    {
        Debug.Assert(data.Length == 7);
        minAttackRange = int.Parse(data[1]);
        maxAttackRange = int.Parse(data[2]);
        attackAngle = int.Parse(data[3]);
        damage = int.Parse(data[4]);
        coolTime = float.Parse(data[5]);
        target = (TargetEnum)Enum.Parse(typeof(TargetEnum),data[6]);
    }
}

[Serializable]
public struct AttackTypeSprite : IAttackType
{
    int minAttackRange;
    int maxAttackRange;
    int attackWidth;
    int attackHeight;
    int damage;
    float coolTime;
    TargetEnum target;

    public int MinAttackRange { get { return minAttackRange; } }
    public int MaxAttackRange { get { return maxAttackRange; } }
    public int AttackWidth { get { return attackWidth; } }
    public int AttackHeight { get { return attackHeight; } }
    public int Damage { get { return damage; } }
    public float CoolTime { get { return coolTime; } }
    public TargetEnum Target { get { return target; } }

    public AttackTypeSprite(string[] data)
    {
        Debug.Assert(data.Length == 8);
        minAttackRange = int.Parse(data[1]);
        maxAttackRange = int.Parse(data[2]);
        attackWidth = int.Parse(data[3]);
        attackHeight = int.Parse(data[4]);
        damage = int.Parse(data[5]);
        coolTime = float.Parse(data[6]);
        target = (TargetEnum)Enum.Parse(typeof(TargetEnum),data[7]);
    }
}

[Serializable]
public struct AttackTypeCasting : IAttackType
{
    int minAttackRange;
    int maxAttackRange;
    int tickDamage;
    float castingTime;
    float coolTime;

    public int MinAttackRange { get { return minAttackRange; } }
    public int MaxAttackRange { get { return maxAttackRange; } }
    public int TickDamage { get { return tickDamage; } }
    public float CastingTime { get { return castingTime; } }
    public float CoolTime { get { return coolTime; } }

    public AttackTypeCasting(string[] data)
    {
        Debug.Assert(data.Length == 6);
        minAttackRange = int.Parse(data[1]);
        maxAttackRange = int.Parse(data[2]);
        tickDamage = int.Parse(data[3]);
        castingTime = float.Parse(data[4]);
        coolTime = float.Parse(data[5]);
    }
}

[Serializable]
public struct AttackTypeInstant : IAttackType
{
    int minAttackRange;
    int maxAttackRange;
    int damage;
    float coolTime;

    public int MinAttackRange { get { return minAttackRange; } }
    public int MaxAttackRange { get { return maxAttackRange; } }
    public int Damage { get { return damage; } }
    public float CoolTime { get { return coolTime; } }

    public AttackTypeInstant(string[] data)
    {
        Debug.Assert(data.Length == 5);
        minAttackRange = int.Parse(data[1]);
        maxAttackRange = int.Parse(data[2]);
        damage = int.Parse(data[3]);
        coolTime = float.Parse(data[4]);
    }
}

#endregion