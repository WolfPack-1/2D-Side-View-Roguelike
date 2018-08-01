using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class No107 : NPC
{
    Animator animator;

    bool isWalk;
    bool isBattle;

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
    
    enum State { Idle, IdleBT, Walk, DeadlyAttack, Die }

    [SerializeField] State state;
    [SerializeField] LivingEntity targetEntity;

    public override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }

    public override void Init(NPCStruct npcStruct)
    {
        base.Init(npcStruct);
        OnGetDamaged += OnGetDamagedHandle;

        state = State.Idle;
        StartCoroutine("Selector");
    }

    public override void Update()
    {
        base.Update();
        animator.SetInteger("HpRatio", (int)(CurrentHp / MaxHp * 100));
    }

    void Reset(State state)
    {
        StopAllCoroutines();
        this.state = state;
        Controller.SetInput(Vector2.zero);
        StartCoroutine("Selector");
    }

    IEnumerator PlayerFinder()
    {
        while (true)
        {
            if (IsBattle)
                yield return new WaitForSeconds(0.1f);
            //탐색 범위 안으로 들어오면 IdleBT로 전환
            foreach (Collider2D col in GetEntity(transform.position, (int) Recognize, 5, "Player"))
            {
                Debug.Log("Find");
                targetEntity = col.GetComponent<LivingEntity>();
                IsBattle = true;
                Reset(State.IdleBT);
                yield break;
            }

            targetEntity = null;
            yield return new WaitForSeconds(0.3f);
        }
    }

    IEnumerator Selector()
    {
        if (!IsDead && !IsBattle)
        {
            Debug.Log("Player Finder Start");
            StartCoroutine("PlayerFinder");
        }

        while (true)
            yield return StartCoroutine(state.ToString());
    }

    IEnumerator Idle()
    {
        IsBattle = false;

        yield return new WaitForSeconds(Random.Range(1f, 2f));

        //탐색 범위 안에 PC가 없으면 랜덤 이동
        targetEntity = null;
        state = State.Walk;
    }

    IEnumerator IdleBT()
    {
        if (!targetEntity)
            targetEntity = FindObjectOfType<Player>(); //Todo : 나중에 빼야함
        
        IsBattle = true;
        float distance = Vector2.Distance(targetEntity.transform.position, transform.position);
        if (distance <= Skills["19_DeadlyAttack"].CurrentRange + 1)
        {
            Debug.Log(distance + " " + Skills["19_DeadlyAttack"].CurrentRange + 1);
            state = State.DeadlyAttack;
            yield break;
        }

        yield return null;
        state = State.Walk;
    }

    IEnumerator Walk()
    {
        IsWalk = true;
        if (targetEntity == null)
            yield return Controller.MoveToRandomPosition();
        else
            yield return Controller.MoveToTarget(targetEntity, 0.5f); 
        yield return null;
        IsWalk = false;
        state = IsBattle ? State.IdleBT : State.Idle;
    }

    IEnumerator DeadlyAttack()
    {
        Debug.Log("Skill Start");
        yield return Controller.UseSkill(Skills["19_DeadlyAttack"], true);
        state = State.IdleBT;
        Debug.Log("Skill End");
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    void OnGetDamagedHandle(float damage)
    {
        if (IsDead)
        {
            Reset(State.Die);
            return;
        }
        
        if(!IsBattle)
            Reset(State.IdleBT);
    }
}