﻿using System.Collections;
using UnityEngine;

public class NPCAI : NPC
{
    enum State { Idle, IdleBT, Move, Attack, Die }

    Skill currentSkill;
    StateMachine<State> fsm;
    
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
        float distance = Vector2.Distance(targetEntity.transform.position, transform.position);
        foreach (Skill skill in Skills)
        {
            if (distance <= skill.CurrentRange + 1)
            {
                currentSkill = skill;
                fsm.ChangeState(State.Attack);
                return;
            }   
        }
        fsm.ChangeState(State.Move);
    }

    IEnumerator Move_Enter()
    {
        IsWalk = true;
        if (targetEntity == null)
            yield return Controller.MoveToRandomPosition();
        else
            yield return Controller.MoveToTarget(targetEntity, 0.5f);
        IsWalk = false;
        fsm.ChangeState(IsBattle ? State.IdleBT : State.Idle);
    }

    IEnumerator Attack_Enter()
    {
        if (currentSkill == null)
        {
            this.Error("아니 스킬을 쓰려했는데 선택된 스킬이 없다니?");
            fsm.ChangeState(State.IdleBT);
        }
        this.Log("Skill Start");
        yield return Controller.UseSkill(currentSkill, true);
        this.Log("Skill End");
        currentSkill = null;
        fsm.ChangeState(State.IdleBT);
    }
    
    IEnumerator Die_Enter()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
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
}