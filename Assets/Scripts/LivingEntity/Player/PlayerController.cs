﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerSkillSlot))]
public class PlayerController : Controller2D
{
    [SerializeField] float minJumpHeight;
    [SerializeField] float maxJumpHeight;
    [SerializeField] float timeToJumpapex;
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpCoolTime;

    float gravity;
    float maxJumpVelocity;
    float minJumpVelocity;
    Vector2 velocity;
    Vector2 input;

    Player player;
    PlayerSkillSlot playerSkillSlot;
    Animator animator;
    
    bool isSit;
    bool isDamaged;
    bool isDashing;
    bool downJump;
    int jumpCount;
    float lastJumpTime;
    
    public bool IsWalk { get { return input.x != 0 && velocity.x != 0; } }
    public bool IsSit { get { return isSit; } }
    public bool IsUsingSkill { get { return playerSkillSlot.IsUsingSkill; } }
    public bool IsDoingCombo { get { return playerSkillSlot.IsDoingCombo; } }
    public bool IsGrounded { get { return collisions.below; } }
    public bool IsDashing { get { return isDashing; } }
    public bool CanUseSkill { get { return !IsDashing && !IsSit && !player.IsDead; } }
    public bool CanWalk { get { return (!IsUsingSkill || IsDoingCombo) && !IsSit && !player.IsDead && !isDamaged && !player.IsUIOpen; } }
    public bool CanJump { get { return (IsGrounded || CanDoubleJump) && Time.time - lastJumpTime >= jumpCoolTime && !IsSit && !player.IsDead && !isDamaged && !player.IsUIOpen; } }
    public bool CanDash { get { return player.CurrentSteam >= 20f && !player.IsUIOpen; } }
    public bool CanSit { get { return collisions.below && !player.IsDead && !player.IsUIOpen; } }
    public bool CanDoubleJump { get { return jumpCount == 1 && player.CurrentSteam >= 20f && !player.IsUIOpen; } }
    
    IInteractable currentInteractable;

    IEnumerator InteractableFinder()
    {
        while (true)
        {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, 0.5f, Vector2.up);
            float distance = float.MaxValue;
            float temp;
            IInteractable before = currentInteractable;
            currentInteractable = null;
            foreach (RaycastHit2D hit in hits)
            {
                temp = Vector2.Distance(transform.position, hit.transform.position);
                if(distance < temp)
                    continue;
                
                IInteractable interactable = hit.transform.GetComponent<IInteractable>();
                if (interactable == null)
                    continue;

                if (currentInteractable != null && currentInteractable != interactable)
                {
                    currentInteractable.Reset();
                }
                currentInteractable = interactable;
                distance = temp;
            }
            if(currentInteractable == null && before != null)
                before.Reset();
            yield return new WaitForSeconds(0.1f);
        }
    }
    
    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
        playerSkillSlot = GetComponent<PlayerSkillSlot>();
    }

    protected override void Start()
    {
        base.Start();

        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpapex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpapex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
        StartCoroutine("InteractableFinder");
    }

    protected override void Update()
    {
        base.Update();

        if (IsGrounded)
            jumpCount = 0;
        
        float speed = playerSkillSlot.IsUsingSkill ? moveSpeed * 0.3f : moveSpeed;
        speed = isDashing ? moveSpeed * 3f : moveSpeed;
        if (isDashing)
            velocity.y = 0;
        KeyInput();
        CalculateVelocity(speed);
        Move(velocity * Time.deltaTime, input, downJump);
        downJump = false;
        if (collisions.above || collisions.below)
            velocity.y = 0;
        SetAnimationParameters();
        ContactCurrentInteractable();
    }

    void SetAnimationParameters()
    {
        animator.SetInteger("HpRatio", (int)(player.CurrentHp / player.MaxHp * 100));
        animator.SetBool("IsWalk", IsWalk);
        animator.SetBool("IsJump", !IsGrounded);
        animator.SetBool("IsSit", IsSit);
    }

    void CalculateVelocity(float speed)
    {
        velocity.x = CanWalk ? input.x * speed : 0;
        velocity.y += gravity * Time.deltaTime;
    }

    void KeyInput()
    {
        if(!isDashing)
            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        isSit = false;

        if (Input.GetKey(KeyCode.DownArrow) && CanSit)
        {
            isSit = true;
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(CanJump)
                OnJumpDown();
            if(IsSit)
                downJump = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            OnJumpUp();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && CanDash)
        {
            Dash();
        }

        if (Input.GetKeyDown(KeyCode.Q) && CanUseSkill)
        {
            playerSkillSlot.Use(PlayerSkillSlot.PlayerSkillKeySlotEnum.Q);
        }
        
        if (Input.GetKeyDown(KeyCode.W) && CanUseSkill)
        {
            playerSkillSlot.Use(PlayerSkillSlot.PlayerSkillKeySlotEnum.W);
        }
        
        if (Input.GetKeyDown(KeyCode.E) && CanUseSkill)
        {
            playerSkillSlot.Use(PlayerSkillSlot.PlayerSkillKeySlotEnum.E);
        }
        
        if (Input.GetKeyDown(KeyCode.R) && CanUseSkill)
        {
            playerSkillSlot.Use(PlayerSkillSlot.PlayerSkillKeySlotEnum.R);
        }

        if (Input.GetKeyDown(KeyCode.F) && currentInteractable != null)
        {
            currentInteractable.Interact(player);
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            player.OpenUI(UIEnum.Crafting);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            player.OpenUI(UIEnum.Inventory);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            player.CloseAllUI();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            Area area = Area.Create(transform.position, 3, 1);
            Collider2D[] cols = area.GetEntity(Area.AreaModeEnum.Box, "NPC");
            foreach (Collider2D col in cols)
            {
                LivingEntity livingEntity = col.GetComponent<LivingEntity>();
                if (livingEntity == null)
                    continue;

                livingEntity.GetDamaged(new DamageInfo(player, 10, transform.position, livingEntity.transform.position));
                this.Log(livingEntity.name + "에게 " + "10의 데미지를 주었습니다.");
            }
            area.Delete();
        }
    }

    void ContactCurrentInteractable()
    {
        if (currentInteractable == null)
            return;
        
        currentInteractable.Contact();
    }

    void OnJumpDown()
    {
        jumpCount++;
        velocity.y = maxJumpVelocity;
        if (jumpCount == 1)
        {
            animator.SetTrigger("DoJump");
        }
        else if (jumpCount == 2 && player.UseSteam(20))
        {
            animator.SetTrigger("DoDoubleJump");
        }
    }

    void OnJumpUp()
    {
        if(velocity.y > minJumpVelocity)
            velocity.y = minJumpVelocity;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }

    public void GetDamaged(DamageInfo info)
    {
        StopCoroutine("DamagedUpdator");
        StartCoroutine("DamagedUpdator");
    }

    IEnumerator DamagedUpdator()
    {
        if(!IsUsingSkill)
            animator.SetTrigger("IsDamaged");
        isDamaged = true;
        yield return new WaitForSeconds(0.1f);
        isDamaged = false;
    }

    void Dash()
    {
        if (player.UseSteam(20))
        {
            animator.SetTrigger("DoDash");
            StopCoroutine("DashUpdator");
            StartCoroutine("DashUpdator");   
        }
    }

    IEnumerator DashUpdator()
    {
        isDashing = true;
        while (true)
        {
            if (isDashing == false)
                break;
            input = new Vector2(Mathf.Sign(transform.localScale.x) * -1f, 0);
            yield return null;
        }
    }

    public void OnDashEnd()
    {
        isDashing = false;
    }
}
