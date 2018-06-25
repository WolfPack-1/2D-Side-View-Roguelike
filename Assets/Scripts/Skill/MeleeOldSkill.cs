using System.Collections;
using UnityEngine;

public class MeleeOldSkill : OldSkill
{
    AttackTypeMelee skillData;
    
    public MeleeOldSkill(SkillStruct skillStruct, LivingEntity owner, AttackTypeMelee attackTypeMelee) : base(skillStruct, owner)
    {
        skillData = attackTypeMelee;
        SetSkillCoolTime(skillData.CoolTime);
    }

    public override void Use()
    {
        if (!CanUseSkill)
            return;
        base.Use();
        Debug.Log(Owner + " : " + SkillStruct.nameKor + ", " + skillData.Damage);
    }

    public override void Stop()
    {
        base.Stop();      
    }
    
}