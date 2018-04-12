using System;
using UnityEngine;

public class PlayerSkillSlot : MonoBehaviour
{

    PlayerInventory playerInventory;
    public enum PlayerSkillKeySlotEnum { Q, W, E }
    
    [SerializeField] Skill[] skills = {null, null, null};

    void Awake()
    {
        playerInventory = GetComponent<PlayerInventory>();
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
            return false;
        }

        ReturnToInventory(skills[(int) slotEnum]);
        skills[(int) slotEnum] = skill;
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

    public void DropSlot(PlayerSkillKeySlotEnum slotEnum, bool cube = true)
    {
        if (skills[(int) slotEnum] == null)
            return;

        if (cube)
        {
            //Todo : 스킬 큐브로 드롭
        }
        skills[(int) slotEnum] = null;
    }

    void ReturnToInventory(Skill skill)
    {
        playerInventory.GetSkill(skill);
    }
    
}
