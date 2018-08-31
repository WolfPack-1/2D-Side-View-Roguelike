using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alliy : NPC
{
    
    enum State { IdleBT, Smoke, Shot, Die }

    Skill currentSkill;
    StateMachine<State> fsm;

    Transform leftPosition, rightPosition;
    float smokeHp;
    float smokeHPCondition;
    
    public override void Init(NPCStruct npc)
    {
        base.Init(npc);

        smokeHp = CurrentHp;
        smokeHPCondition = MaxHp * 0.1f;

        leftPosition = GameObject.Find("19_024").transform.Find("Boss").Find("LeftPosition");
        rightPosition = GameObject.Find("19_024").transform.Find("Boss").Find("RightPosition");
        
        OnGetDamaged += OnGetDamagedHandle;
        fsm = StateMachine<State>.Initialize(this);
        fsm.ChangeState(State.IdleBT);
    }

    IEnumerator IdleBT_Enter()
    {
        targetEntity = FindObjectOfType<Player>();

        while (currentSkill == null)
        {
            this.Log("IdleBT");
            if (smokeHp - CurrentHp >= smokeHPCondition)
            {
                currentSkill = Skills.Find(s => s.StyleStructs[0].cid == 5138);
                fsm.ChangeState(State.Smoke);
                break;
            }
            currentSkill = FindProperSkill();
            if(currentSkill != null)
            {
                fsm.ChangeState(State.Shot);
                break;
            }  
            yield return new WaitForSeconds(0.3f);
        }
        this.Log("IdleBT Finished");
    }

    IEnumerator Smoke_Enter()
    {
        if (currentSkill == null)
        {
            this.Error("이건 좀 아니지 않나요?");
            fsm.ChangeState(State.IdleBT);
            yield break;
        }
        this.Log("Smoke Enter");
        Controller.SetDirToTarget(targetEntity);
        yield return Controller.UseSkill(currentSkill, targetEntity, false);
        this.Log("Smoke Finished");
        float lDistance, rDistance;
        lDistance = Vector2.Distance(transform.position, leftPosition.position);
        rDistance = Vector2.Distance(transform.position, rightPosition.position);

        currentSkill = null;
        transform.position = lDistance >= rDistance ? leftPosition.position : rightPosition.position;
        Controller.SetDirToTarget(targetEntity);
        smokeHp = CurrentHp;
        yield return null;
        fsm.ChangeState(State.IdleBT);
    }

    IEnumerator Shot_Enter()
    {
        Controller.SetDirToTarget(targetEntity);
        if (currentSkill == null)
        {
            this.Error("아니 스킬을 쓰려했는데 선택된 스킬이 없다니?");
            fsm.ChangeState(State.IdleBT);
        }
        this.Log("Shot Enter");
        yield return Controller.UseSkill(currentSkill, targetEntity, false);
        this.Log("Shot Finished");
        currentSkill = null;
        fsm.ChangeState(State.IdleBT);
    }


    void OnGetDamagedHandle(DamageInfo info)
    {
        if (IsDead)
        {
            fsm.ChangeState(State.Die, StateTransition.Overwrite);
        }
    }
    
    Skill FindProperSkill()
    {
        if (targetEntity == null)
            return null;
        
        float distance = Vector2.Distance(targetEntity.transform.position, transform.position);
        float range = float.MaxValue;
        Skill properSkill = null;
        foreach (Skill skill in Skills)
        {
            if(!skill.CanUseSkill || skill.CurrentRange > range || skill.CurrentRange < distance || skill.StyleStructs[0].cid == 5138)
                continue;
            
            properSkill = skill;
            range = properSkill.CurrentRange;
        }
        return properSkill;
    }
}
