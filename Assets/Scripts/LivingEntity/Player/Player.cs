using System.Linq;
using UnityEngine;

public class Player : LivingEntity
{

    #region Components

    PlayerInventory playerInventory;
    PlayerSkillSlot skillSlot;
    AudioSource audioSource;

    #endregion

    #region Variables

    

    #endregion

    //--------------------------------------------------------//

    #region Initialize

    public override void Awake()
    {
        base.Awake();
        dataManager = FindObjectOfType<DataManager>();
        playerInventory = GetComponent<PlayerInventory>();
        skillSlot = GetComponent<PlayerSkillSlot>();
        audioSource = GetComponent<AudioSource>();
    }

    public override void Start()
    {
        // Debug
        base.Start();
        DebugCreateSkill();
    }

    void DebugCreateSkill()
    {
        // Debug Tubes
        Tube styleTube = new Tube(dataManager.TubeData.StyleData[0]);
        Tube enhancerTube = new Tube(dataManager.TubeData.EnhancerData[0]);
        Tube coolerTube = new Tube(dataManager.TubeData.CoolerData[0]);
        Tube relicTube = new Tube(dataManager.TubeData.RelicData[0]);

        GetTube(styleTube);
        GetTube(enhancerTube);
        GetTube(coolerTube);
        GetTube(relicTube);

        CreateSkill(styleTube.Cid, enhancerTube.Cid, coolerTube.Cid);

        SetSlot(PlayerSkillSlot.PlayerSkillKeySlotEnum.Q, playerInventory.GetRandomSkill(true));
        
        styleTube = new Tube(dataManager.TubeData.StyleData[6]);
        enhancerTube = new Tube(dataManager.TubeData.EnhancerData[8]);
        coolerTube = new Tube(dataManager.TubeData.CoolerData[0]);
        relicTube = new Tube(dataManager.TubeData.RelicData[0]);

        GetTube(styleTube);
        GetTube(enhancerTube);
        GetTube(coolerTube);
        GetTube(relicTube);

        CreateSkill(styleTube.Cid, enhancerTube.Cid, coolerTube.Cid);

        SetSlot(PlayerSkillSlot.PlayerSkillKeySlotEnum.W, playerInventory.GetRandomSkill(true));
        
        styleTube = new Tube(dataManager.TubeData.StyleData[7]);
        enhancerTube = new Tube(dataManager.TubeData.EnhancerData[5]);
        coolerTube = new Tube(dataManager.TubeData.CoolerData[0]);
        relicTube = new Tube(dataManager.TubeData.RelicData[0]);

        GetTube(styleTube);
        GetTube(enhancerTube);
        GetTube(coolerTube);
        GetTube(relicTube);

        CreateSkill(styleTube.Cid, enhancerTube.Cid, coolerTube.Cid);

        SetSlot(PlayerSkillSlot.PlayerSkillKeySlotEnum.E, playerInventory.GetRandomSkill(true));
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

    public void PlaySound(string name)
    {
        audioSource.PlayOneShot(Resources.Load<AudioClip>("Sounds/" + name));
    }
}