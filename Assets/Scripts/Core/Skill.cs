using System;
using UnityEngine;

[Serializable]
public class Skill
{

    [SerializeField] SkillStruct skillStruct;
    [SerializeField] LivingEntity owner;

    public SkillStruct SkillStruct { get { return skillStruct; } }
    public LivingEntity Owner { get { return owner; } }

    public Skill(SkillStruct skillStruct, LivingEntity owner)
    {
        this.skillStruct = skillStruct;
        this.owner = owner;
    }

    public virtual void Use()
    {

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