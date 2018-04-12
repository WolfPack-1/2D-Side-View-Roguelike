using System;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerSkillSlot : MonoBehaviour
{

    PlayerInventory playerInventory;
    public enum PlayerSkillKeySlotEnum { Q, W, E }

    public delegate void OnPlayerSkillslotDelegate(Skill skill, bool success);

    public event OnPlayerSkillslotDelegate OnSetSlot;
    public event OnPlayerSkillslotDelegate OnDropSlot;
    public event OnPlayerSkillslotDelegate OnDeleteSlot;
    
    [SerializeField] Skill[] skills = {null, null, null};

    void Awake()
    {
        playerInventory = GetComponent<PlayerInventory>();
        OnSetSlot = delegate { };
        OnDropSlot = delegate { };
        OnDeleteSlot = delegate { };
    }
    
    public Skill GetSkill(PlayerSkillKeySlotEnum slotEnum)
    {
        return skills[(int) slotEnum] != null ? skills[(int) slotEnum] : null;
    }

    public bool SetSlot(PlayerSkillKeySlotEnum slotEnum, Skill skill)
    {
        if (skills[(int) slotEnum] == null)
        {
            skills[(int) slotEnum] = skill;
            
            if(OnSetSlot != null)
                OnSetSlot(skill, false);
            return false;
        }

        if (!SlotToInventory(slotEnum))
            return false;
        
        skills[(int) slotEnum] = skill;
        if(OnSetSlot != null)
            OnSetSlot(skill, true);
        return true;
    }

    public bool SlotToInventory(PlayerSkillKeySlotEnum slotEnum)
    {
        if (skills[(int) slotEnum] == null || playerInventory.IsFull)
        {
            return false;
        }
        
        playerInventory.GetSkill(skills[(int) slotEnum]);
        return true;
    }

    public void DropSlot(PlayerSkillKeySlotEnum slotEnum)
    {
        if (skills[(int) slotEnum] == null)
            return;

        //Todo : 스킬 큐브로 드롭

        if(OnDropSlot != null)
            OnDropSlot(skills[(int) slotEnum], true);
        skills[(int) slotEnum] = null;
    }

    public void DeleteSlot(PlayerSkillKeySlotEnum slotEnum)
    {
        if (skills[(int) slotEnum] == null)
            return;

        if(OnDeleteSlot != null)
            OnDeleteSlot(skills[(int) slotEnum], true);
        skills[(int) slotEnum] = null; 
    }

}
