using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    bool isAttackEvent;
    
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
    
    public TubeStyleStruct[] StyleStructs { get { return styleStructs; } set { styleStructs = value; } }
    public TubeEnhancerStruct EnhancerStruct { get { return enhancerStruct; } set { enhancerStruct = value; } }
    public TubeCoolerStruct CoolerStruct { get { return coolerStruct; } set { coolerStruct = value; } }
    public TubeRelicStruct RelicStruct { get { return relicStruct; } set { relicStruct = value; } }
    
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
    public bool IsAnimationFinished { get { return isAnimationFinished; } }
    public int CurrentSkillIndex { get { return currentSkillIndex; } }
    public float CurrentRange { get { return styleStructs[currentSkillIndex].range; } }

    List<Transform> onStartFXs;
    List<Transform> onHitFxs;
    List<Transform> projectileFxs;
    
    void InitFxs()
    {
        onStartFXs = new List<Transform>();
        onHitFxs = new List<Transform>();
        projectileFxs = new List<Transform>();
        
        int index = 0;
        while (true)
        {
            Transform fxTransform = Resources.Load<Transform>("FX/" + enhancerStruct.Cid+ "_" + "OnStart" + index);
            if (fxTransform == null)
                break;
            onStartFXs.Add(fxTransform);
            index++;
        }
        
        index = 0;
        while (true)
        {
            Transform fxTransform = Resources.Load<Transform>("FX/" + enhancerStruct.Cid+ "_" + "OnHit" + index);
            if (fxTransform == null)
                break;
            onHitFxs.Add(fxTransform);
            index++;
        }
        
        index = 0;
        while (true)
        {
            Transform fxTransform = Resources.Load<Transform>("FX/" + enhancerStruct.Cid+ "_" + "Projectile" + index);
            if (fxTransform == null)
                break;
            projectileFxs.Add(fxTransform);
            index++;
        }
    }
    
    public Skill(TubeStyleStruct[] styleStructs, TubeEnhancerStruct enhancerStruct, TubeCoolerStruct coolerStruct)
    {
        this.styleStructs = styleStructs;
        this.enhancerStruct = enhancerStruct;
        this.coolerStruct = coolerStruct;
        lastSkillTime = float.MinValue;
        currentSkillIndex = 0;
        InitFxs();
    }

    public Skill(string text, DataManager dataManager)
    {
        NPCSkillStruct skillStruct = dataManager.NPCData.Skill.Find(t => t.name == text);
        TubeStyleStruct style = dataManager.NPCTubeData.StyleData.Find(t => t.name == skillStruct.styleTube);
        TubeEnhancerStruct enhancer = dataManager.NPCTubeData.EnhancerData.Find(t => t.name == skillStruct.enhancerTube);
        TubeCoolerStruct cooler = dataManager.NPCTubeData.CoolerData.Find(t => t.name == skillStruct.coolerTube);
        
        styleStructs = new[] { style };
        enhancerStruct = enhancer;
        coolerStruct = cooler;
        lastSkillTime = float.MinValue;
        currentSkillIndex = 0;
        InitFxs();
    }
    
    public Skill(TubeStyleStruct[] styleStructs, TubeEnhancerStruct enhancerStruct, TubeCoolerStruct coolerStruct, TubeRelicStruct relicStruct) : this(styleStructs, enhancerStruct, coolerStruct)
    {
        this.relicStruct = relicStruct;
    }

    public void SetOwner(LivingEntity owner)
    {
        if (!owner)
            return;
        this.owner = owner;
        animator = owner.GetComponent<Animator>();
    }

    public LivingEntity GetOwner()
    {
        return owner;
    }
    
    public void Use(Action<bool> callback, bool firstSkill)
    {
        if (!IsCoolTimeAvailable || !owner || owner.IsDead)
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
        if (firstSkill)
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
            owner.Log(owner.transform.name + " : Set Skill Animation Parameters");
            animator.SetInteger("StyleNum", styleStructs[currentSkillIndex].cid);
            animator.SetTrigger("DoSkill");
        }

        // OnStart Fx 출력
        SpawnRandomSkill(SkillFxEnum.OnStart);
        
        owner.Log(owner.transform.name + " : Waiting for Attack Event");
        yield return new WaitUntil(() => isAttackEvent);
        isAttackEvent = false;
        
        Vector2 areaPosition = owner.GetPosition(styleStructs[currentSkillIndex].position);
        // AttackType을 SubClass로 나누지 않고 그냥 switch 돌림
        switch (styleStructs[currentSkillIndex].attackType)
        {
            case AttackTypeEnum.MELEE:
                Area area = Area.Create(areaPosition, enhancerStruct.range / 2f, enhancerStruct.range);
                string layerName = owner.GetType() == typeof(Player) ? "NPC" : "Player";
                Collider2D[] colliders = area.GetEntity(Area.AreaModeEnum.Box, layerName);
                foreach(Collider2D col in colliders)
                {
                    LivingEntity livingEntity = col.GetComponent<LivingEntity>();
                    if (livingEntity == null)
                        continue;
                    livingEntity.GetDamaged(new DamageInfo(owner, styleStructs[currentSkillIndex].damage, owner.transform.position, livingEntity.transform.position));
                    SpawnRandomSkill(SkillFxEnum.OnHit);
                    owner.Log(livingEntity.name + "에게 " + styleStructs[currentSkillIndex].damage + "의 데미지를 주었습니다.");
                }
                area.Delete();
                break;
            case AttackTypeEnum.RANGE:
                GameObject rangeFx = SpawnRandomSkill(SkillFxEnum.Projectile);
                if (!rangeFx)
                    break;
                Projectile
                    .Create(areaPosition, styleStructs[currentSkillIndex].damage, enhancerStruct.range * 5, owner, rangeFx)
                    .SetHitFx(onHitFxs)
                    .Fire(areaPosition + Mathf.Sign(owner.transform.localScale.x) * Vector2.left );
                break;
            case AttackTypeEnum.BOUNCE:
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                worldPosition.z = 0;
                GameObject bounceFx = SpawnRandomSkill(SkillFxEnum.Projectile);
                if (!bounceFx)
                    break;
                owner.Log(worldPosition);
                Projectile
                    .Create(areaPosition, styleStructs[currentSkillIndex].damage, enhancerStruct.range, owner, bounceFx)
                    .SetHitFx(onHitFxs)
                    .Launch(areaPosition + Mathf.Sign(owner.transform.localScale.x) * Vector2.left);
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
        isAttackEvent = false;
        lastSkillTime = Time.time;
        callback(false);
    }

    public void AnimationFinishedEvent()
    {
        owner.Log(owner.transform.name + " : Skill Finished");
        isAnimationFinished = true;
    }

    public void AttackEvent()
    {
        owner.Log(owner.transform.name + " : Skill Attack Event");
        isAttackEvent = true;
    }

    GameObject SpawnRandomSkill(SkillFxEnum skillFxEnum)
    {
        GameObject fx = null;
        switch (skillFxEnum)
        {
            case SkillFxEnum.OnStart:
                if(onStartFXs.Count > 0)
                    fx = onStartFXs[UnityEngine.Random.Range(0, onStartFXs.Count)].gameObject;
                break;
            case SkillFxEnum.OnHit:
                if(onHitFxs.Count > 0)
                    fx = onHitFxs[UnityEngine.Random.Range(0, onHitFxs.Count)].gameObject;
                break;
            case SkillFxEnum.Projectile:
                if(projectileFxs.Count > 0)
                    fx = projectileFxs[UnityEngine.Random.Range(0, projectileFxs.Count)].gameObject;
                break;
        }

        if (fx == null)
        {
            owner.Log(owner.transform.name + " : " +skillFxEnum + " Fx가 없습니다.");
            return null;
        }

        fx = GameObject.Instantiate(fx, owner.GetPosition(styleStructs[currentSkillIndex].position), Quaternion.identity);
        fx.transform.localScale = owner.transform.localScale;
        return fx;
    }
}