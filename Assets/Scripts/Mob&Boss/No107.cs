using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class No107 : NPC
{
    Animator animator;

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

    public override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }

    public override void Init(NPCStruct npcStruct)
    {
        base.Init(npcStruct);
        OnGetDamaged += OnGetDamagedHandle;

        StartCoroutine("Selector");
        state = State.Idle;
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
            foreach (Collider2D col in GetEntity(transform.position, (int)REC, 5, "Player"))
            {
                state = State.IdleBT;
                yield break;
            }
            yield return new WaitForSeconds(1f);
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
        while (true)
        {

            yield return null;
        }
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