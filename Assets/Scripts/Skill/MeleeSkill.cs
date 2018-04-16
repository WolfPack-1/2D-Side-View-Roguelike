using UnityEngine;

public class MeleeSkill : Skill
{
    AttackTypeMelee skillData;
    
    public MeleeSkill(SkillStruct skillStruct, LivingEntity owner, AttackTypeMelee attackTypeMelee) : base(skillStruct, owner)
    {
        skillData = attackTypeMelee;
    }

    public override void Use()
    {
        base.Use();
        Debug.Log(Owner + " : " + SkillStruct.nameKor + ", " + skillData.Damage);
    }

    public override void Stop()
    {
        base.Stop();      
    }
    
}