using System;
using UnityEngine;

[Serializable]
[Obsolete]
public class OldSkill
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

    public OldSkill(SkillStruct skillStruct, LivingEntity owner)
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

    public static OldSkill CreateSkill(SkillStruct skillStruct, LivingEntity owner)
    {
        string attackType = FunctionParser.ParsingAttackType(skillStruct.attackType);
        if (string.IsNullOrEmpty(attackType))
            return null;
        switch (attackType)
        {
            case "melee":
            {
                return new MeleeOldSkill(skillStruct, owner, FunctionParser.ParsingAttackType(skillStruct.attackType, (x) => new AttackTypeMelee(x)));
            }

            default:
            {
                return null;
            }
        }
    }
}

[Serializable]
public class Skill
{
    [SerializeField] TubeStyleStruct styleStruct;
    [SerializeField] TubeEnhancerStruct enhancerStruct;
    [SerializeField] TubeCoolerStruct coolerStruct;
    [SerializeField] TubeRelicStruct relicStruct;
    [SerializeField] float lastSkillTime;
    
    public string Name 
    {
        get
        {
            // Todo : 임시로 grade 를 한글로 직접 바꿔줌
            string grade = "";
            switch (styleStruct.grade)
            {
                case TubeGradeEnum.WEAKNESS:
                    grade = "애송이의";
                    break;
                case TubeGradeEnum.GANGSTER:
                    grade = "길거리의";
                    break;
                case TubeGradeEnum.FIGHT:
                    grade = "무술가의";
                    break;
                case TubeGradeEnum.MASTER:
                    grade = "달인의";
                    break;
            }
            return grade + " " + enhancerStruct.nameKor + " " + styleStruct.nameKor;
        } 
    }
    public float CoolTime { get { return coolerStruct.cooltime; } }
    public float LastSkillTime { get { return lastSkillTime; } }
    public bool IsCoolTimeAvailable
    {
        get
        {
            if (CoolTime < 0) return true;
            return Time.time - LastSkillTime >= CoolTime;

        }
    }
    public bool CanUseSkill { get { return IsCoolTimeAvailable; } }
    
    public Skill(TubeStyleStruct styleStruct, TubeEnhancerStruct enhancerStruct, TubeCoolerStruct coolerStruct)
    {
        this.styleStruct = styleStruct;
        this.enhancerStruct = enhancerStruct;
        this.coolerStruct = coolerStruct;
    }
    
    public Skill(TubeStyleStruct styleStruct, TubeEnhancerStruct enhancerStruct, TubeCoolerStruct coolerStruct, TubeRelicStruct relicStruct)
    {
        this.styleStruct = styleStruct;
        this.enhancerStruct = enhancerStruct;
        this.coolerStruct = coolerStruct;
        this.relicStruct = relicStruct;
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
}