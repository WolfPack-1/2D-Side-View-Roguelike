using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerInventory : Inventory
{
    Player player;

    public override void Awake()
    {
        base.Awake();
        player = GetComponent<Player>();
    }    
}