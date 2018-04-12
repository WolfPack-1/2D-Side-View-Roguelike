
public class PlayerInventory : Inventory
{
    Player player;
    PlayerSkillSlot skillSlot;

    public override void Awake()
    {
        base.Awake();
        player = GetComponent<Player>();
        skillSlot = GetComponent<PlayerSkillSlot>();
    }

    public bool SetSlot(PlayerSkillSlot.PlayerSkillKeySlotEnum slotEnum, Skill skill)
    {
        return skillSlot.SetSlot(slotEnum, skill);
    }
    
}