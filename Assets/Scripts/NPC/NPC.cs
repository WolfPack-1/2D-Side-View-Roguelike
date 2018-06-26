using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : LivingEntity
{

    [SerializeField] NPCStruct npcStruct;
    public NPCStruct NPCStruct { get { return npcStruct; } }

    public delegate void NPCDelegate(NPC npc);
    public NPCDelegate OnNPCInit;
    
    #region NPCStats
    
    public float HP { get { return Stats[StatsEnum.HP]; } }
    public float ATK { get { return Stats[StatsEnum.ATK]; } }
    public float DEF { get { return Stats[StatsEnum.DEF]; } }
    public float ATS { get { return Stats[StatsEnum.ATS]; } }
    public float SPD { get { return Stats[StatsEnum.SPD]; } }
    public float REC { get { return Stats[StatsEnum.REC]; } }
    
    #endregion
    
    #region Initialize

    public override void Awake()
    {
        base.Awake();
        OnNPCInit = delegate {  };
    }

    public virtual void Init(NPCStruct npcStruct)
    {
        this.npcStruct = npcStruct;

        transform.name = npcStruct.nameKor;
        
        AddStat(StatsEnum.HP, npcStruct.hp);
//        AddStat(StatsEnum.ATK, npcStruct.ATK);
//        AddStat(StatsEnum.DEF, npcStruct.DEF);
//        AddStat(StatsEnum.ATS, npcStruct.ATS);
//        AddStat(StatsEnum.SPD, npcStruct.SPD);
        AddStat(StatsEnum.REC, npcStruct.recognizeValue);

        OnNPCInit(this);
    }    
    
    #endregion
}
