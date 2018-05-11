using System;
using UnityEngine;

[Serializable]
public class Skill
{

    [SerializeField] SkillStruct skillStruct;
    [SerializeField] LivingEntity owner;

    public SkillStruct SkillStruct { get { return skillStruct; } }
    public LivingEntity Owner { get { return owner; } }

    [SerializeField] float lastSkillTime;
    public float LastSkillTime { get { return lastSkillTime; } }

    [SerializeField] float skillCoolTime = -1f;

    public bool IsCoolTimeAvailable
    {
        get
        {
            if (skillCoolTime < 0) return true;
            return Time.time - lastSkillTime >= skillCoolTime;

        }
    }
    
    public bool CanUseSkill { get { return IsCoolTimeAvailable; } }

    public Skill(SkillStruct skillStruct, LivingEntity owner)
    {
        this.skillStruct = skillStruct;
        this.owner = owner;
    }

    public void SetSkillCoolTime(float value)
    {
        skillCoolTime = value;
    }

    public virtual void Use()
    {
        if (!CanUseSkill)
            return;
        lastSkillTime = Time.time;
    }

    public virtual void Stop()
    {

    }

    public static Skill CreateSkill(SkillStruct skillStruct, LivingEntity owner)
    {
        string attackType = FunctionParser.ParsingAttackType(skillStruct.attackType);
        if (string.IsNullOrEmpty(attackType))
            return null;
        switch (attackType)
        {
            case "melee":
            {
                return new MeleeSkill(skillStruct, owner, FunctionParser.ParsingAttackType(skillStruct.attackType, (x) => new AttackTypeMelee(x)));
            }

            default:
            {
                return null;
            }
        }
    }
}