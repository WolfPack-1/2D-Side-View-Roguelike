﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NPCController))]
public class NPC : LivingEntity
{

    NPCController controller;
    Animator animator;
    [SerializeField] NPCStruct npcStruct;
    
    public Dictionary<string, Skill> Skills { get; private set; }
    public NPCStruct NPCStruct { get { return npcStruct; } }
    public NPCController Controller { get { return controller; } }

    public delegate void NPCDelegate(NPC npc);

    public NPCDelegate OnNPCInit;
    public NPCDelegate OnNPCFoundPlayer;
    
    protected LivingEntity targetEntity;
    protected Coroutine currentState;

    bool isWalk;
    bool isBattle;
    
    public float Recognize { get { return npcStruct.recognizeValue; } }
    public bool IsAggresive { get { return npcStruct.recognize; } }
    public bool CanWalk { get { return true; } }
    public bool IsWalk
    {
        get { return isWalk; }
        protected set
        {
            if (animator == null) return;
            animator.SetBool("IsWalk", value);
            isWalk = value;
        }
    }
    public bool IsBattle
    {
        get { return isBattle; }
        protected set
        {
            if (animator == null) return;
            animator.SetBool("IsBattle", value);
            isBattle = value;
        }
    }

    public LivingEntity TargetEntity { get { return targetEntity; } }

    #region Initialize

    public override void Awake()
    {
        base.Awake();
        OnNPCInit = delegate { };
        OnNPCFoundPlayer = delegate {  };
        controller = GetComponent<NPCController>();
        animator = GetComponent<Animator>();
    }

    public virtual void Init(NPCStruct npc)
    {
        this.npcStruct = npc;
        transform.name = npc.nameKor;
        Skills = FunctionParser.ParsingSkillTable(npc.skillValue, dataManager);
        foreach (Skill skill in Skills.Values)
        {
            skill.SetOwner(this);
        }
        OnNPCInit(this);
    }

    public virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            GetComponent<NPCInventory>().DropRandomTube();
        animator.SetInteger("HpRatio", (int)(CurrentHp / MaxHp * 100));
    }

    #endregion
    
    protected IEnumerator PlayerFinder()
    {
        while (true)
        {
            if (IsBattle)
                yield return new WaitForSeconds(0.1f);
            //탐색 범위 안으로 들어오면 OnNPCFoundPlayer 호출
            foreach (Collider2D col in GetEntity(transform.position, (int) Recognize, 5, "Player"))
            {
                Debug.Log("Find");
                targetEntity = col.GetComponent<LivingEntity>();
                IsBattle = true;
                OnNPCFoundPlayer(this);
                yield break;
            }

            targetEntity = null;
            yield return new WaitForSeconds(0.3f);
        }
    }
}