using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

[Serializable]
public class Skill
{
    const float globalComboWaitTime = 0.2f;
    
    Coroutine skillUpdator;
    Coroutine comboUpdator;
    
    [SerializeField] LivingEntity owner;
    [SerializeField] Animator animator;
    [SerializeField] TubeStyleStruct[] styleStructs;
    [SerializeField] TubeEnhancerStruct enhancerStruct;
    [SerializeField] TubeCoolerStruct coolerStruct;
    [SerializeField] TubeRelicStruct relicStruct;
    [SerializeField] float lastSkillTime;

    int currentSkillIndex;
    bool isAnimationFinished;
    
//    public string Name 
//    {
//        get
//        {
//            // Todo : 임시로 grade 를 한글로 직접 바꿔줌
//            string grade = "";
//            switch (styleStruct.grade)
//            {
//                case TubeGradeEnum.WEAKNESS:
//                    grade = "애송이의";
//                    break;
//                case TubeGradeEnum.GANGSTER:
//                    grade = "길거리의";
//                    break;
//                case TubeGradeEnum.FIGHT:
//                    grade = "무술가의";
//                    break;
//                case TubeGradeEnum.MASTER:
//                    grade = "달인의";
//                    break;
//            }
//            return grade + " " + enhancerStruct.nameKor + " " + styleStruct.nameKor;
//        } 
//    }
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
    public bool CanUseSkill { get { return IsCoolTimeAvailable && skillUpdator == null; } }
    public bool IsFirstSkill { get { return currentSkillIndex == 0; } }
    public bool IsLastSkill { get { return currentSkillIndex == styleStructs.Length; } }
    public bool CanCombo { get { return comboUpdator != null; } }
    public bool IsDoingCombo { get { return currentSkillIndex < styleStructs.Length; } }
    public int CurrentSkillIndex { get { return currentSkillIndex; } }
    
    
    public Skill(TubeStyleStruct[] styleStructs, TubeEnhancerStruct enhancerStruct, TubeCoolerStruct coolerStruct)
    {
        this.styleStructs = styleStructs;
        this.enhancerStruct = enhancerStruct;
        this.coolerStruct = coolerStruct;
        lastSkillTime = float.MinValue;
        currentSkillIndex = 0;
    }
    
    public Skill(TubeStyleStruct[] styleStructs, TubeEnhancerStruct enhancerStruct, TubeCoolerStruct coolerStruct, TubeRelicStruct relicStruct) : this(styleStructs, enhancerStruct, coolerStruct)
    {
        this.relicStruct = relicStruct;
    }

    public void SetOwner(LivingEntity owner)
    {
        Debug.Assert(owner);
        this.owner = owner;
        animator = owner.GetComponent<Animator>();
    }
    
    public void Use(Action<bool> callback, bool firstSkill)
    {
        if (!IsCoolTimeAvailable)
        {
            callback(false);
            return;
        }

        if (skillUpdator != null)
        {
            callback(true);
            return;
        }

        if (comboUpdator != null)
        {
            owner.StopCoroutine(comboUpdator);
        }
        else if (firstSkill)
            currentSkillIndex = 0;

        if (IsLastSkill)
        {
            Stop(callback);
            return;
        }

        skillUpdator = owner.StartCoroutine(SkillUpdator(callback));
    }

    IEnumerator SkillUpdator(Action<bool> callback)
    {
        callback(true);
        if (animator != null)
        {
            animator.SetInteger("StyleNum", styleStructs[currentSkillIndex].cid);
            animator.SetTrigger("DoSkill");
        }

        Vector2 areaPosition = owner.GetPosition(styleStructs[currentSkillIndex].position);
        // Class를 SubClass로 나누지 않고 그냥 switch 돌림
        switch (styleStructs[currentSkillIndex].attackType)
        {
            case AttackTypeEnum.MELEE:
                Area area = Area.Create(areaPosition, enhancerStruct.range / 2f, enhancerStruct.range);
                Collider2D[] colliders = area.GetEntity(Area.AreaModeEnum.Box, "NPC");
                foreach(Collider2D col in colliders)
                {
                    LivingEntity livingEntity = col.GetComponent<LivingEntity>();
                    if (livingEntity == null)
                        continue;
                    livingEntity.GetDamaged(styleStructs[currentSkillIndex].damage);
                    Debug.Log(livingEntity.name + "에게 " + styleStructs[currentSkillIndex].damage + "의 데미지를 주었습니다.");
                }
                area.Delete();
                break;
            case AttackTypeEnum.RANGE:
                Projectile
                    .Create(areaPosition, styleStructs[currentSkillIndex].damage, enhancerStruct.range, owner)
                    .Fire(areaPosition);
                break;
            case AttackTypeEnum.BOUNCE:
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                worldPosition.z = 0;
                Debug.Log(worldPosition);
                Projectile
                    .Create(areaPosition, styleStructs[currentSkillIndex].damage, enhancerStruct.range, owner)
                    .Launch(areaPosition);
                break;
        }
        if(comboUpdator != null)
            owner.StopCoroutine(comboUpdator);
        comboUpdator = owner.StartCoroutine(ComboUpdator(callback));
        yield return new WaitUntil(() => isAnimationFinished);
        currentSkillIndex++;
        if (IsLastSkill)
        {
            Stop(callback);
            yield break;
        }
        isAnimationFinished = false;
        skillUpdator = null;
    }

    IEnumerator ComboUpdator(Action<bool> callback)
    {
        while (true)
        {
            if (!isAnimationFinished && !IsLastSkill)
            {
                yield return null;
            }
            else
                break;
        }
        float timer = 0;
        while (true)
        {
            if (timer > globalComboWaitTime)
                break;
            timer += Time.deltaTime;
            yield return null;
        }
        Stop(callback);
    }

    public void Stop(Action<bool> callback)
    {
        currentSkillIndex = 0;
        if(skillUpdator != null)
            owner.StopCoroutine(skillUpdator);
        if(comboUpdator != null)
            owner.StopCoroutine(comboUpdator);

        skillUpdator = null;
        comboUpdator = null;
        isAnimationFinished = false;
        lastSkillTime = Time.time;
        callback(false);
    }

    public void AnimationFinished()
    {
        isAnimationFinished = true;
    }
}