using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization.Formatters;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : LivingEntity
{

    #region Components

    PlayerInventory playerInventory;
    DataManager dataManager;
    Rigidbody2D rigidBody2D;

    #endregion

    #region Public Variables

    public bool IsGrounded
    {
        get
        {
            if (rigidBody2D == null)
                return false;

            RaycastHit2D[] hits = Physics2D.LinecastAll(transform.position, transform.position + Vector3.down * 1.1f);
            return hits.Any(hit =>
                hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"));
        }
    }

    public bool CanJump { get { return IsGrounded && Time.time - lastJumpTime >= jumpCoolTime; } }

    #endregion

    #region Private Variables

    float lastJumpTime;

    #endregion

    #region Debug

    [Header("Debug")] [SerializeField] [Range(5f, 15f)]
    float jumpPower;

    [SerializeField] [Range(0.1f, 1f)] float jumpCoolTime;

    #endregion

    #region PlayerStats

    public float HP { get { return Stats[StatsEnum.HP]; } }
    public float ATK { get { return Stats[StatsEnum.ATK]; } }
    public float DEF { get { return Stats[StatsEnum.DEF]; } }
    public float ATS { get { return Stats[StatsEnum.ATS]; } }
    public float SPD { get { return Stats[StatsEnum.SPD]; } }

    #endregion

    //--------------------------------------------------------//

    #region Initialize

    public override void Awake()
    {
        base.Awake();
        dataManager = FindObjectOfType<DataManager>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        playerInventory = GetComponent<PlayerInventory>();
    }

    void Start()
    {
        //Debug
        Init(dataManager.LivingEntityData.Data[0]);
    }

    public void Init(LivingEntityStruct livingEntityStruct)
    {
        AddStat(StatsEnum.HP, livingEntityStruct.hp);
        AddStat(StatsEnum.ATK, livingEntityStruct.atk);
        AddStat(StatsEnum.DEF, livingEntityStruct.def);
        AddStat(StatsEnum.ATS, livingEntityStruct.ats);
        AddStat(StatsEnum.SPD, livingEntityStruct.spd);
    }

    #endregion

    #region Updates

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && CanJump)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        Move(h);
    }

    #endregion

    #region Movements

    void Move(float h)
    {
        rigidBody2D.velocity = new Vector2(h * SPD * 0.1f, rigidBody2D.velocity.y);
    }

    void Jump()
    {
        lastJumpTime = Time.time;
        rigidBody2D.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }

    #endregion
    
    #region Inventory & SkillSlot

    public bool GetSkill(SkillStruct skillStruct)
    {
        return playerInventory.GetSkill(skillStruct);
    }

    public bool DropSkill(SkillStruct skillStruct)
    {
        return playerInventory.DropSkill(skillStruct);
    }

    public bool DeleteSkill(SkillStruct skillStruct)
    {
        return playerInventory.DeleteSkill(skillStruct);
    }
    
    public bool SetSlot(PlayerSkillSlot.PlayerSkillKeySlotEnum slotEnum, Skill skill)
    {
        return playerInventory.SetSlot(slotEnum, skill);
    }
    
    #endregion


}
