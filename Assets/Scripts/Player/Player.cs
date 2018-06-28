using System.Linq;
using UnityEngine;

public class Player : LivingEntity
{

    #region Components

    PlayerInventory playerInventory;
    DataManager dataManager;
    PlayerSkillSlot skillSlot;

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
        skillSlot = GetComponent<PlayerSkillSlot>();
    }
    
    void Start()
    {
        // Debug
        Init(dataManager.LivingEntityData.Data[0]);
        
        // Debug Tubes
        Tube styleTube = new Tube(dataManager.TubeData.StyleData[2]);
        Tube enhancerTube = new Tube(dataManager.TubeData.EnhancerData[0]);
        Tube coolerTube = new Tube(dataManager.TubeData.CoolerData[0]);
        Tube relicTube = new Tube(dataManager.TubeData.RelicData[0]);

        GetTube(styleTube);
        GetTube(enhancerTube);
        GetTube(coolerTube);
        GetTube(relicTube);

        CreateSkill(styleTube.Cid, enhancerTube.Cid, coolerTube.Cid);

        SetSlot(PlayerSkillSlot.PlayerSkillKeySlotEnum.Q, playerInventory.GetRandomSkill(true));
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

    public bool GetTube(Tube tube)
    {
        return playerInventory.GetTube(tube);
    }

    public bool DropTube(int cid)
    {
        return playerInventory.DropTube(cid);
    }

    public bool DeleteTube(int cid)
    {
        return playerInventory.DeleteTube(cid);
    }

    public bool GetSkill(Skill skill)
    {
        return playerInventory.GetSkill(skill);
    }

    public bool DeleteSkill(Skill skill)
    {
        return playerInventory.DeleteSkill(skill);
    }

    public bool CreateSkill(int styleCid, int enhancerCid, int coolerCid, int relicCid = -1)
    {
        return playerInventory.CreateSkill(styleCid, enhancerCid, coolerCid, relicCid);
    }

    public bool SetSlot(PlayerSkillSlot.PlayerSkillKeySlotEnum slotEnum, Skill skill)
    {
        return skillSlot.SetSlot(slotEnum, skill);
    }

    #endregion
}
