﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerSkillSlot))]
public class PlayerController : Controller2D
{
    [SerializeField] float minJumpHeight = 4;
    [SerializeField] float maxJumpHeight = 4;
    [SerializeField] float timeToJumpapex = 0.4f;
    [SerializeField] float moveSpeed = 2;
    [SerializeField] float jumpCoolTime = 1f;

    float gravity;
    float maxJumpVelocity;
    float minJumpVelocity;
    Vector2 velocity;
    Vector2 input;

    Player player;
    PlayerSkillSlot playerSkillSlot;
    Animator animator;
    
    bool isSit;
    float lastJumpTime;
    
    public bool IsWalk { get { return input.x != 0 && velocity.x != 0; } }
    public bool IsSit { get { return isSit && collisions.below && !player.IsDead; } }
    public bool IsUsingSkill { get { return playerSkillSlot.IsUsingSkill; } }
    public bool IsDoingCombo { get { return playerSkillSlot.IsDoingCombo; } }
    public bool IsGrounded { get { return collisions.below; } }
    public bool CanWalk { get { return (!IsUsingSkill || IsDoingCombo) && !IsSit && !player.IsDead; } }
    public bool CanJump { get { return IsGrounded && Time.time - lastJumpTime >= jumpCoolTime && !player.IsDead; } }

    IInteractable currentInteractable;

    IEnumerator InteractableFinder()
    {
        while (true)
        {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, 0.5f, Vector2.up);
            float distance = float.MaxValue;
            float temp;
            foreach (RaycastHit2D hit in hits)
            {
                temp = Vector2.Distance(transform.position, hit.transform.position);
                if(distance < temp)
                    continue;
                
                IInteractable interactable = hit.transform.GetComponent<IInteractable>();
                if (interactable == null)
                    continue;

                Debug.Log(interactable);
                currentInteractable = interactable;
                distance = temp;
            }
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
        float speed = playerSkillSlot.IsUsingSkill ? moveSpeed * 0.3f : moveSpeed;
        KeyInput();
        CalculateVelocity(speed);
        Move(velocity * Time.fixedDeltaTime, input);
        if (collisions.above || collisions.below)
            velocity.y = 0;
        SetAnimationParameters();
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
        velocity.x = CanWalk ? input.x * moveSpeed : 0;
        velocity.y += gravity * Time.deltaTime;
    }

    void KeyInput()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        isSit = false;

        if (Input.GetKey(KeyCode.DownArrow))
        {
            isSit = true;
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && CanJump)
        {
            OnJumpDown();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            OnJumpUp();
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
            currentInteractable.Interact();
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
    }

    void OnJumpDown()
    {
        velocity.y = maxJumpVelocity;
        animator.SetTrigger("DoJump");
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
}
