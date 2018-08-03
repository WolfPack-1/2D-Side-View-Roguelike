using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoVat : NPC
{
    enum State { Idle, IdleBT, Move, DeadlyAttack, Die }

    StateMachine<State> fsm;
    
    public override void Init(NPCStruct npc)
    {
        base.Init(npc);
        OnGetDamaged += OnGetDamagedHandle;
        OnNPCFoundPlayer += OnNPCFoundPlayerHandle;
        fsm = StateMachine<State>.Initialize(this);
        fsm.ChangeState(State.Idle);
        StartCoroutine("PlayerFinder");
    }

    IEnumerator Idle_Enter()
    {
        Debug.Log("Idle Enter");
        IsBattle = false;
        targetEntity = null;
        yield return new WaitForSeconds(Random.Range(1f, 2f));
        fsm.ChangeState(State.Move);
    }

    void IdleBT_Enter()
    {
        if (!targetEntity)
            targetEntity = FindObjectOfType<Player>(); //Todo : 나중에 빼야함 데미지 핸들러에 Struct를 받게 수정해야됨
        
        IsBattle = true;
        float distance = Vector2.Distance(targetEntity.transform.position, transform.position);
        if (distance <= Skills["19_DeadlyAttack"].CurrentRange + 1)
        {
            fsm.ChangeState(State.DeadlyAttack);
            return;
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

    IEnumerator DeadlyAttack_Enter()
    {
        Debug.Log("Skill Start");
        yield return Controller.UseSkill(Skills["19_DeadlyAttack"], true);
        Debug.Log("Skill End");
        fsm.ChangeState(State.IdleBT);
    }
    
    IEnumerator Die_Enter()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    void OnGetDamagedHandle(float damage)
    {
        if (IsDead)
        {
            fsm.ChangeState(State.Die);
            return;
        }

        if (!IsBattle)
            fsm.ChangeState(State.IdleBT);
    }

    void OnNPCFoundPlayerHandle(NPC npc)
    {
        fsm.ChangeState(State.IdleBT);
    }
}