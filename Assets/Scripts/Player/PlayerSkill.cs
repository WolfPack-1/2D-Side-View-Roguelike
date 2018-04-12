using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerSkill : SkillSystem
{

    Player player;

    void Awake()
    {
        player = GetComponent<Player>();
    }

}