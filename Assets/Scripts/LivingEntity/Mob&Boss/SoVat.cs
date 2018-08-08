using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[Obsolete]
public class SoVat : NPC
{
//    enum State { Idle, IdleBT, Move, DeadlyAttack, Die }
//
//    StateMachine<State> fsm;
//    
//    public override void Init(NPCStruct npc)
//    {
//        base.Init(npc);
//        OnGetDamaged += OnGetDamagedHandle;
//        OnNPCFoundPlayer += OnNPCFoundPlayerHandle;
//        fsm = StateMachine<State>.Initialize(this, State.Idle);
//        StartCoroutine("PlayerFinder");
//    }
//
//    IEnumerator Idle_Enter()
//    {
//        this.Log("Idle Enter");
//        IsBattle = false;
//        targetEntity = null;
//        yield return new WaitForSeconds(Random.Range(1f, 2f));
//        fsm.ChangeState(State.Move);
//    }
//
//    void IdleBT_Enter()
//    {      
//        IsBattle = true;
//        float distance = Vector2.Distance(targetEntity.transform.position, transform.position);
//        if (distance <= Skills["19_DeadlyAttack"].CurrentRange + 1)
//        {
//            fsm.ChangeState(State.DeadlyAttack);
//            return;
//        }
//        fsm.ChangeState(State.Move);
//    }
//
//    IEnumerator Move_Enter()
//    {
//        IsWalk = true;
//        if (targetEntity == null)
//            yield return Controller.MoveToRandomPosition();
//        else
//            yield return Controller.MoveToTarget(targetEntity, 0.5f);
//        IsWalk = false;
//        fsm.ChangeState(IsBattle ? State.IdleBT : State.Idle);
//    }
//
//    IEnumerator DeadlyAttack_Enter()
//    {
//        this.Log("Skill Start");
//        yield return Controller.UseSkill(Skills["19_DeadlyAttack"], true);
//        this.Log("Skill End");
//        fsm.ChangeState(State.IdleBT);
//    }
//    
//    IEnumerator Die_Enter()
//    {
//        yield return new WaitForSeconds(3f);
//        Destroy(gameObject);
//    }
//
//    void OnGetDamagedHandle(DamageInfo info)
//    {
//        if (IsDead)
//        {
//            fsm.ChangeState(State.Die, StateTransition.Overwrite);
//            return;
//        }
//
//        if (!IsBattle)
//        {
//            fsm.ChangeState(State.IdleBT);
//            targetEntity = info.From;
//        }
//    }
//
//    void OnNPCFoundPlayerHandle(NPC npc)
//    {
//        fsm.ChangeState(State.IdleBT);
//    }
}