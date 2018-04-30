using UnityEngine;
using UnityEngine.Experimental.UIElements;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerSkillSlot))]
public class PlayerController : Controller
{

    Player player;
    PlayerSkillSlot playerSkillSlot;
    int inputDir;

    public override void Awake()
    {
        base.Awake();
        player = GetComponent<Player>();
        playerSkillSlot = GetComponent<PlayerSkillSlot>();
    }

    void Start()
    {
        SetJumpCoolTime(player.JumpCoolTime);
    }

    void KeyInput()
    {
        inputDir = 0;
        if (InputExtensions.GetKey(KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.DownArrow))
        {
            inputDir = (int)Input.GetAxisRaw("Horizontal");
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && CanJump)
        {
            Jump(player.JumpPower);
        }
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            playerSkillSlot.Use(PlayerSkillSlot.PlayerSkillKeySlotEnum.Q);
        }
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            playerSkillSlot.Use(PlayerSkillSlot.PlayerSkillKeySlotEnum.W);
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            playerSkillSlot.Use(PlayerSkillSlot.PlayerSkillKeySlotEnum.E);
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            playerSkillSlot.Use(PlayerSkillSlot.PlayerSkillKeySlotEnum.R);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            playerSkillSlot.Use(PlayerSkillSlot.PlayerSkillKeySlotEnum.A);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            playerSkillSlot.Use(PlayerSkillSlot.PlayerSkillKeySlotEnum.S);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
        }
    }
    
    public override void Update()
    {
        base.Update();
        KeyInput();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        Move(inputDir, player.SPD);
    }

}