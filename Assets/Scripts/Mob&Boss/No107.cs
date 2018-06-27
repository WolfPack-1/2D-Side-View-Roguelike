using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class No107 : NPC
{
    Animator animator;
   // NPCController controller;

    bool isWalk;
    bool isBattle;
    bool isDead;

    public bool IsWalk
    {
        get { return isWalk; }
        private set
        {
            if (animator == null) return;
            animator.SetBool("IsWalk", value);
            isWalk = value;
        }
    }

    public bool IsBattle
    {
        get { return isBattle; }
        private set
        {
            if (animator == null) return;
            animator.SetBool("IsBattle", value);
            isBattle = value;
        }
    }

    public bool IsDead
    {
        get { return isDead; }
        private set
        {
            if (animator == null) return;
            animator.SetBool("IsDead", value);
            isDead = value;
        }
    }
    
    public enum State { Idle, IdleBT, Walk, Attack, Skill, Die }

    [SerializeField] State state;
    [SerializeField] LivingEntity targetEntity;

    public override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        //controller = GetComponent<NPCController>();
    }

    public override void Init(NPCStruct npcStruct)
    {
        base.Init(npcStruct);
        OnGetDamaged += OnGetDamagedHandle;

        StartCoroutine("Selector");
        state = State.Idle;
    }

    void Update()
    {
        animator.SetInteger("HpRatio", (int)(HP / NPCStruct.hp * 100));
    }

    IEnumerator Selector()
    {
        while (true)
            yield return StartCoroutine(state.ToString());
    }

    IEnumerator Idle()
    {
        IsBattle = false;
        while (true)
        {
            yield return new WaitForSeconds(1f);
            
            //탐색 범위 안으로 들어오면 IdleBT로 전환
            foreach (Collider2D col in GetEntity(transform.position, (int)REC, 5, "Player"))
            {
                targetEntity = col.GetComponent<LivingEntity>();
                state = State.IdleBT;
                yield break;
            }
            
            //탐색 범위 안에 PC가 없으면 랜덤 이동
            targetEntity = null;
            state = State.Walk;
        }
    }

    IEnumerator IdleBT()
    {
        IsBattle = true;
        while (true)
        {
            
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator Walk()
    {
        IsWalk = true;
//        if (targetEntity == null)
//            yield return controller.MoveToRandomPosition();
//        else
//            yield return controller.MoveToTarget(targetEntity, 1f, REC); 
        yield return null;
        IsWalk = false;
        state = isBattle ? State.IdleBT : State.Idle;
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(1f);
    }

    IEnumerator Skill()
    {
        yield return new WaitForSeconds(1f);
    }

    IEnumerator Die()
    {
        IsDead = true;
        while (true)
        {
            //Todo : 사망 시 처리
            yield return null;
        }
    }

    void OnGetDamagedHandle(float damage)
    {
        if (HP > 0)
            return;

        StopAllCoroutines();
        StartCoroutine("Die");
    }
}