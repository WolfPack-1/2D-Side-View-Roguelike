﻿using System;
using System.Linq;
using UnityEngine;

public class Player : LivingEntity
{
    #region Components

    GameManager gameManager;
    PlayerInventory playerInventory;
    PlayerSkillSlot skillSlot;
    PlayerController playerController;
    AudioSource audioSource;

    public PlayerInventory Inventory { get { return playerInventory; }}
    public PlayerSkillSlot SkillSlot { get { return skillSlot; } }
    public GameManager GameManager { get { return gameManager; }}

    #endregion

    //--------------------------------------------------------//

    #region Initialize

    public override void Awake()
    {
        base.Awake();
        gameManager = FindObjectOfType<GameManager>();
        dataManager = FindObjectOfType<DataManager>();
        playerInventory = GetComponent<PlayerInventory>();
        skillSlot = GetComponent<PlayerSkillSlot>();
        audioSource = GetComponent<AudioSource>();
        playerController = GetComponent<PlayerController>();
    }

    public override void Start()
    {
        // Debug
        base.Start();
        //DebugCreateSkill();
    }

    void DebugCreateSkill()
    {
        // Debug Tubes
        Tube styleTube = new Tube(dataManager.TubeData.StyleData[0]);
        Tube enhancerTube = new Tube(dataManager.TubeData.EnhancerData[0]);
        Tube coolerTube = new Tube(dataManager.TubeData.CoolerData[0]);

        GetTube(styleTube);
        GetTube(enhancerTube);
        GetTube(coolerTube);
        
        styleTube = new Tube(dataManager.TubeData.StyleData[6]);
        enhancerTube = new Tube(dataManager.TubeData.EnhancerData[1]);
        coolerTube = new Tube(dataManager.TubeData.CoolerData[0]);

        GetTube(styleTube);
        GetTube(enhancerTube);
        GetTube(coolerTube);
        
        styleTube = new Tube(dataManager.TubeData.StyleData[3]);
        enhancerTube = new Tube(dataManager.TubeData.EnhancerData[22]);
        coolerTube = new Tube(dataManager.TubeData.CoolerData[0]);

        GetTube(styleTube);
        GetTube(enhancerTube);
        GetTube(coolerTube);
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

    #region Event
    
    /// <summary>
    /// Unity Animation Event
    /// </summary>
    public void PlaySound(string name)
    {
        audioSource.PlayOneShot(Resources.Load<AudioClip>("Sounds/" + name));
    }

    public override bool GetDamaged(DamageInfo info)
    {
        playerController.GetDamaged(info);
        return base.GetDamaged(info);
    }

    #endregion
    
    #region UI

    public void OpenUI(UIEnum uiEnum)
    {
        switch (uiEnum)
        {
            case UIEnum.State:
                
                break;
            case UIEnum.Inventory:
                gameManager.InventoryController.Open();
                break;
            case UIEnum.Crafting:
                gameManager.CraftingController.Open();
                break;
        }
    }

    public void CloseAllUI()
    {
        gameManager.InventoryController.Close();
        gameManager.CraftingController.Close();
    }
    
    #endregion
    
}