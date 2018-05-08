using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization.Formatters;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : LivingEntity
{

    #region Components

    PlayerInventory playerInventory;
    DataManager dataManager;
    
    #endregion

    #region Private Variables

    float lastJumpTime;

    #endregion

    #region Debug

    [Header("Debug")] [SerializeField] [Range(5f, 15f)] float jumpPower;
    
    public float JumpPower { get { return jumpPower; } }

    [SerializeField] [Range(0.1f, 1f)] float jumpCoolTime;
    
    public float JumpCoolTime { get { return jumpCoolTime; } }

    #endregion

    #region PlayerStats

    public float HP { get { return !Stats.ContainsKey(StatsEnum.HP) ? 0 : Stats[StatsEnum.HP]; } }
    public float ATK { get { return !Stats.ContainsKey(StatsEnum.ATK) ? 0 : Stats[StatsEnum.ATK]; } }
    public float DEF { get { return !Stats.ContainsKey(StatsEnum.DEF) ? 0 : Stats[StatsEnum.DEF]; } }
    public float ATS { get { return !Stats.ContainsKey(StatsEnum.ATS) ? 0 : Stats[StatsEnum.ATS]; } }
    public float SPD { get { return !Stats.ContainsKey(StatsEnum.SPD) ? 0 : Stats[StatsEnum.SPD]; } }

    #endregion

    //--------------------------------------------------------//

    #region Initialize

    public override void Awake()
    {
        base.Awake();
        dataManager = FindObjectOfType<DataManager>();
        playerInventory = GetComponent<PlayerInventory>();
    }

    void Start()
    {
        //Debug
        Init(dataManager.LivingEntityData.Data[0]);
        //SetSlot(PlayerSkillSlot.PlayerSkillKeySlotEnum.A, dataManager.SkillData.GetSkillStruct(19101));
    }

    public void Init(LivingEntityStruct livingEntityStruct)
    {
        AddStat(StatsEnum.HP, livingEntityStruct.hp);
        AddStat(StatsEnum.ATK, livingEntityStruct.atk);
        AddStat(StatsEnum.DEF, livingEntityStruct.def);
        AddStat(StatsEnum.ATS, livingEntityStruct.ats);
        AddStat(StatsEnum.SPD, livingEntityStruct.spd);
    }

    #endregion

    
    #region Inventory & SkillSlot

    public bool GetSkill(SkillStruct skillStruct)
    {
        return playerInventory.GetSkill(skillStruct);
    }

    public bool DropSkill(SkillStruct skillStruct)
    {
        return playerInventory.DropSkill(skillStruct);
    }

    public bool DeleteSkill(SkillStruct skillStruct)
    {
        return playerInventory.DeleteSkill(skillStruct);
    }
    
    public bool SetSlot(PlayerSkillSlot.PlayerSkillKeySlotEnum slotEnum, SkillStruct skillStruct)
    {
        return playerInventory.SetSlot(slotEnum, skillStruct);
    }
    
    #endregion


}
