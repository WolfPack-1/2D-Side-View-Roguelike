using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerController : Controller
{

    Player player;

    public override void Awake()
    {
        base.Awake();
        player = GetComponent<Player>();
    }

    void Start()
    {
        SetJumpCoolTime(player.JumpCoolTime);
    }
    
    public override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Space) && CanJump)
        {
            Jump(player.JumpPower);
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        Move(h, player.SPD);
    }

}