using System;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerSkillSlot : MonoBehaviour
{

    PlayerInventory playerInventory;
    public enum PlayerSkillKeySlotEnum { Q, W, E, R, A, S }

    public delegate void OnPlayerSkillslotDelegate(Skill skill, bool success);

    public event OnPlayerSkillslotDelegate OnSetSlot;
    public event OnPlayerSkillslotDelegate OnDropSlot;
    public event OnPlayerSkillslotDelegate OnDeleteSlot;
    
    [SerializeField] Skill[] skillSlots;

    void Awake()
    {
        playerInventory = GetComponent<PlayerInventory>();
        skillSlots = new Skill[6];
        OnSetSlot = delegate { };
        OnDropSlot = delegate { };
        OnDeleteSlot = delegate { };
    }
    
    public Skill GetSkill(PlayerSkillKeySlotEnum slotEnum)
    {
        return skillSlots[(int) slotEnum] != null ? skillSlots[(int) slotEnum] : null;
    }

    public void Use(PlayerSkillKeySlotEnum slotEnum)
    {
        Skill skill = GetSkill(slotEnum);
        if (skill == null)
            return;
        skill.Use();
    }

    public bool SetSlot(PlayerSkillKeySlotEnum slotEnum, Skill skill)
    {
        if (skillSlots[(int) slotEnum] == null)
        {
            skillSlots[(int) slotEnum] = skill;
            if(OnSetSlot != null)
                OnSetSlot(skill, false);
            return false;
        }
        if (!SlotToInventory(slotEnum))
            return false;
        
        skillSlots[(int) slotEnum] = skill;
        if(OnSetSlot != null)
            OnSetSlot(skill, true);
        return true;
    }

    public bool SlotToInventory(PlayerSkillKeySlotEnum slotEnum)
    {
        if (skillSlots[(int) slotEnum] == null || playerInventory.IsFull)
        {
            return false;
        }
        
        playerInventory.GetSkill(skillSlots[(int) slotEnum].SkillStruct);
        return true;
    }

    public void DropSlot(PlayerSkillKeySlotEnum slotEnum)
    {
        if (skillSlots[(int) slotEnum] == null)
            return;

        //Todo : 스킬 큐브로 드롭

        if(OnDropSlot != null)
            OnDropSlot(skillSlots[(int) slotEnum], true);
        skillSlots[(int) slotEnum] = null;
    }

    public void DeleteSlot(PlayerSkillKeySlotEnum slotEnum)
    {
        if (skillSlots[(int) slotEnum] == null)
            return;

        if(OnDeleteSlot != null)
            OnDeleteSlot(skillSlots[(int) slotEnum], true);
        skillSlots[(int) slotEnum] = null; 
    }

}
