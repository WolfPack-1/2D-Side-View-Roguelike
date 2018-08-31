using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doncina : NPC
{
    
    enum State { Idle, IdleBT, Move, Chain, Pull, Jump, Die}

    StateMachine<State> fsm;
    Skill currentSkill;

    public override void Init(NPCStruct npc)
    {
        base.Init(npc);
        OnGetDamaged += OnGetDamagedHandle;
        OnNPCFoundPlayer += OnNPCFoundPlayerHandle;
        fsm = StateMachine<State>.Initialize(this, State.Idle);
        StartCoroutine("PlayerFinder");
    }

    IEnumerator Idle_Enter()
    {
        this.Log("Idle Enter");
        IsBattle = false;
        targetEntity = null;
        yield return new WaitForSeconds(Random.Range(1f, 2f));
        fsm.ChangeState(State.Move);
    }
    
    void IdleBT_Enter()
    {      
        IsBattle = true;
        currentSkill = FindProperSkill();
        if (currentSkill == null)
        {
            fsm.ChangeState(State.Move);
            return;
        }

        switch (currentSkill.StyleStructs[0].cid)
        {
            case 5104:
                fsm.ChangeState(State.Chain);
                return;
            case 5105:
                fsm.ChangeState(State.Pull);
                return;
        }
    }

    IEnumerator Chain_Enter()
    {
        Controller.SetDirToTarget(targetEntity);
        if (currentSkill == null)
        {
            this.Error("아니 스킬을 쓰려했는데 선택된 스킬이 없다니?");
            fsm.ChangeState(State.IdleBT);
        }
        this.Log("Skill Start");
        yield return Controller.UseSkill(currentSkill, targetEntity, true);
        this.Log("Skill End");
        currentSkill = null;
        fsm.ChangeState(State.IdleBT);
    }

    IEnumerator Pull_Enter()
    {
        Controller.SetDirToTarget(targetEntity);
        if (currentSkill == null)
        {
            this.Error("아니 스킬을 쓰려했는데 선택된 스킬이 없다니?");
            fsm.ChangeState(State.IdleBT);
        }
        // Todo : Projectile 관련한거 먼저 손봐야함
        yield return null;
    }
    
    void OnGetDamagedHandle(DamageInfo info)
    {
        if (IsDead)
        {
            fsm.ChangeState(State.Die, StateTransition.Overwrite);
            return;
        }

        if (!IsBattle)
        {
            fsm.ChangeState(State.IdleBT);
            targetEntity = info.From;
        }
    }

    void OnNPCFoundPlayerHandle(NPC npc)
    {
        fsm.ChangeState(State.IdleBT);
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
            if(!skill.CanUseSkill || skill.CurrentRange > range || skill.CurrentRange < distance)
                continue;

            properSkill = skill;
            range = properSkill.CurrentRange;
        }
        return properSkill;
    }
}
