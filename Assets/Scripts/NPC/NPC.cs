using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : LivingEntity
{

    [SerializeField] NPCStruct npcStruct;
    
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
        
    }

    public void Init(NPCStruct npcStruct)
    {
        this.npcStruct = npcStruct;

        transform.name = npcStruct.nameKor;
        
        AddStat(StatsEnum.HP, npcStruct.hp);
        AddStat(StatsEnum.ATK, npcStruct.attackDamage);
        AddStat(StatsEnum.DEF, npcStruct.armor);
        AddStat(StatsEnum.ATS, npcStruct.attackSpeed);
        AddStat(StatsEnum.SPD, npcStruct.speed);
        AddStat(StatsEnum.REC, npcStruct.recognizeValue);
    }    
    
    #endregion

}
