using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

[Serializable]
public class Skill
{
    Coroutine updator;
    
    [SerializeField] LivingEntity owner;
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
    public bool CanUseSkill { get { return IsCoolTimeAvailable && updator == null; } }
    
    public Skill(TubeStyleStruct styleStruct, TubeEnhancerStruct enhancerStruct, TubeCoolerStruct coolerStruct)
    {
        this.styleStruct = styleStruct;
        this.enhancerStruct = enhancerStruct;
        this.coolerStruct = coolerStruct;
        lastSkillTime = float.MinValue;
    }
    
    public Skill(TubeStyleStruct styleStruct, TubeEnhancerStruct enhancerStruct, TubeCoolerStruct coolerStruct, TubeRelicStruct relicStruct) : this(styleStruct, enhancerStruct, coolerStruct)
    {
        this.relicStruct = relicStruct;
    }

    public void SetOwner(LivingEntity owner)
    {
        this.owner = owner;
    }
    
    public void Use()
    {
        if (!CanUseSkill)
            return;
        updator = owner.StartCoroutine(KillUpdator());
    }

    IEnumerator KillUpdator()
    {
        Debug.Log("스킬 사용 시작 : " + Name);
        yield return new WaitForSeconds(styleStruct.motionDelay);
        Debug.Log(styleStruct.motionDelay + "초 만큼 기다림");
        lastSkillTime = Time.time;

        // Class를 SubClass로 나누지 않고 그냥 switch 돌림
        switch (styleStruct.attackType)
        {
            case AttackTypeEnum.MELEE:
                // Todo : Style의 position을 이용하게 바꾸자
                Area area = Area.Create(owner.transform.position + owner.Dir * Vector3.right, enhancerStruct.splash, 2);
                Collider2D[] colliders = area.GetEntity(Area.AreaModeEnum.Box, "NPC");
                foreach(Collider2D col in colliders)
                {
                    LivingEntity livingEntity = col.GetComponent<LivingEntity>();
                    if (livingEntity == null)
                        continue;
                    livingEntity.GetDamaged(styleStruct.damage);
                    Debug.Log(livingEntity.name + "에게 " + styleStruct.damage + "의 데미지를 주었습니다.");
                }
                area.Delete();
                break;
            case AttackTypeEnum.RANGE:
                Projectile
                    .Create(owner.transform.position, styleStruct.damage, enhancerStruct.splash, owner)
                    .Fire(owner.transform.position + owner.Dir * Vector3.right);
                break;
            case AttackTypeEnum.BOUNCE:
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                worldPosition.z = 0;
                Debug.Log(worldPosition);
                Projectile
                    .Create(owner.transform.position, styleStruct.damage, enhancerStruct.splash, owner)
                    .Launch(owner.transform.position + owner.Dir * Vector3.right);
                break;
        }

        updator = null;
    }

    public void Stop()
    {

    }
}