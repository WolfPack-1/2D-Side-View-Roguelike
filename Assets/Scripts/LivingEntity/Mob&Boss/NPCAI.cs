using System.Collections;
using UnityEngine;

public class NPCAI : NPC
{
    enum State { Idle, IdleBT, Move, Attack, Die }

    Skill currentSkill;
    StateMachine<State> fsm;

    public float ProperRange
    {
        get
        {
            Skill properSkill = FindProperSkill();
            return properSkill == null ? 0.5f : properSkill.CurrentRange - 0.5f;
        }
    }
    
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
        if(currentSkill == null)
            fsm.ChangeState(State.Move);
        
        fsm.ChangeState(currentSkill == null ? State.Move : State.Attack);
    }

    IEnumerator Move_Enter()
    {
        IsWalk = true;
        if (targetEntity == null)
            yield return Controller.MoveToRandomPosition();
        else
            yield return Controller.MoveToTarget(targetEntity, this);
        IsWalk = false;
        fsm.ChangeState(IsBattle ? State.IdleBT : State.Idle);
    }

    IEnumerator Attack_Enter()
    {
        Controller.SetDirToTarget(targetEntity);
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
