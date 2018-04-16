using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerInventory : Inventory
{
    Player player;
    PlayerSkillSlot skillSlot;
    DataManager dataManager;

    public override void Awake()
    {
        base.Awake();
        player = GetComponent<Player>();
        skillSlot = GetComponent<PlayerSkillSlot>();
        dataManager = FindObjectOfType<DataManager>();
    }

    public bool SetSlot(PlayerSkillSlot.PlayerSkillKeySlotEnum slotEnum, SkillStruct skillStruct)
    {
        return skillSlot.SetSlot(slotEnum, Skill.CreateSkill(skillStruct, player));
    }
    
}