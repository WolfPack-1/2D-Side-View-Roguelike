using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerInventory : Inventory
{
    Player player;
    PlayerSkillSlot skillSlot;

    public override void Awake()
    {
        base.Awake();
        player = GetComponent<Player>();
        skillSlot = player.GetComponent<PlayerSkillSlot>();
    }

    public bool SetSlot(PlayerSkillSlot.PlayerSkillKeySlotEnum slotEnum, Skill skill)
    {
        return skillSlot.SetSlot(slotEnum, skill);
    }
    
}