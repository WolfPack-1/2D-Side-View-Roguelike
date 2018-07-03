using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngineInternal.Input;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerSkillSlot))]
public class PlayerController : Controller
{

    Player player;
    PlayerSkillSlot playerSkillSlot;
    Animator animator;
    int inputDir;

    
    public bool IsMove { get { return rb2d.velocity.sqrMagnitude > 0; } }
    public bool IsWalk
    {
        get
        {
            return IsMove && (inputDir != 0);
        }
    }
    public bool IsUsingSkill { get { return playerSkillSlot.IsUsingSkill; } }
    public bool CanWalk { get { return !IsUsingSkill; } }

    public override void Awake()
    {
        base.Awake();
        player = GetComponent<Player>();
        playerSkillSlot = GetComponent<PlayerSkillSlot>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        SetJumpCoolTime(player.JumpCoolTime);
    }

    public override void Update()
    {
        base.Update();
        KeyInput();
        animator.SetInteger("HpRatio", (int)(player.HP / player.LivingEntityStruct.hp * 100));
        animator.SetBool("IsWalk", IsWalk);
        animator.SetBool("IsJump", !IsGrounded);
    }
    
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        Move(inputDir, player.SPD);
    }

    public override void Move(int dir, float speed)
    {
        if (!CanWalk)
        {
            base.Move(dir, 0);
            return;
        }
        base.Move(dir, speed);
    }

    public override void Jump(float power)
    {
        base.Jump(power);
        animator.SetTrigger("DoJump");
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

}