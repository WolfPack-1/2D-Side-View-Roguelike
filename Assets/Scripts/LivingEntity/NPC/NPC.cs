using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NPCController))]
public class NPC : LivingEntity
{

    NPCController controller;
    [SerializeField] NPCStruct npcStruct;
    
    public Dictionary<string, Skill> Skills { get; private set; }
    public NPCStruct NPCStruct { get { return npcStruct; } }
    public NPCController Controller { get { return controller; } }

    public delegate void NPCDelegate(NPC npc);

    public NPCDelegate OnNPCInit;
    
    public float Recognize { get { return npcStruct.recognizeValue; } }
    public bool IsAggresive { get { return npcStruct.recognize; } }
    public bool CanWalk { get { return true; } }

    #region Initialize

    public override void Awake()
    {
        base.Awake();
        OnNPCInit = delegate { };
        controller = GetComponent<NPCController>();
    }

    public virtual void Init(NPCStruct npcStruct)
    {
        this.npcStruct = npcStruct;
        transform.name = npcStruct.nameKor;
        Skills = FunctionParser.ParsingSkillTable(npcStruct.skillValue, dataManager);
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
    }

    #endregion
}