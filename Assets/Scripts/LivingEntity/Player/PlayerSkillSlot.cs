using System;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerSkillSlot : MonoBehaviour
{
    Player player;
    PlayerInventory playerInventory;
    public enum PlayerSkillKeySlotEnum { Q, W, E, R}
    bool isUsingSkill;

    public delegate void OnPlayerSkillslotDelegate(Skill skill, bool success);

    public event OnPlayerSkillslotDelegate OnSetSlot;
    public event OnPlayerSkillslotDelegate OnDropSlot;
    public event OnPlayerSkillslotDelegate OnDeleteSlot;
    
    public bool IsUsingSkill { get { return isUsingSkill; } }
    public bool IsDoingCombo
    {
        get
        {
            return currentSkill != null && currentSkill.IsDoingCombo;
        }
    }

    Skill currentSkill;
    Skill[] skillSlots;

    void Awake()
    {
        player = GetComponent<Player>();
        playerInventory = GetComponent<PlayerInventory>();
        skillSlots = new Skill[4];
        OnSetSlot = delegate { };
        OnDropSlot = delegate { };
        OnDeleteSlot = delegate { };
    }

    public bool IsSlotEmpty(PlayerSkillKeySlotEnum slotEnum)
    {
        return skillSlots[(int) slotEnum] == null;
    }
    
    public Skill GetSkill(PlayerSkillKeySlotEnum slotEnum)
    {
        return IsSlotEmpty(slotEnum) ? null : skillSlots[(int) slotEnum];
    }

    public void Use(PlayerSkillKeySlotEnum slotEnum)
    {
        Skill skill = GetSkill(slotEnum);
        if (skill == null || (isUsingSkill && !IsDoingCombo))
            return;
        if (currentSkill != null && currentSkill != skill)
            currentSkill.Stop(b => isUsingSkill = b);
        currentSkill = skill;
        skill.Use(b => isUsingSkill = b, skill.IsFirstSkill);
    }

    public bool SetSlot(PlayerSkillKeySlotEnum slotEnum, Skill skill)
    {
        if (IsSlotEmpty(slotEnum))
        {
            // 슬롯이 비어있음
            skillSlots[(int) slotEnum] = skill;
            skillSlots[(int)slotEnum].SetOwner(player);
            if (OnSetSlot != null)
                OnSetSlot(skillSlots[(int) slotEnum], false);
            return false;
        }
        
        // 슬롯에 이미 스킬이 있음
        DeleteSlot(slotEnum);
        skillSlots[(int) slotEnum] = skill;
        skillSlots[(int)slotEnum].SetOwner(player);
        if(OnSetSlot != null)
            OnSetSlot(skill, true);
        return true;
    }

    public bool DeleteSlot(PlayerSkillKeySlotEnum slotEnum)
    {
        if (IsSlotEmpty(slotEnum))
            return false;
        
        playerInventory.GetSkill(skillSlots[(int) slotEnum]);
        if(OnDeleteSlot != null)
            OnDeleteSlot(skillSlots[(int) slotEnum], true);
        skillSlots[(int)slotEnum].SetOwner(null);
        skillSlots[(int) slotEnum] = null;
        return true;
    }

    public void AnimationFinished()
    {
        currentSkill.AnimationFinished();
    }
}
