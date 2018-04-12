using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization.Formatters;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : LivingEntity
{
    DataManager dataManager;
    Rigidbody2D rigidBody2D;

    public bool IsGrounded
    {
        get
        {
            if (rigidBody2D == null)
                return false;

            RaycastHit2D[] hits = Physics2D.LinecastAll(transform.position, transform.position + Vector3.down * 1.1f);
            return hits.Any(hit => hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"));
        }
    }
    public bool CanJump
    {
        get { return IsGrounded && Time.time - lastJumpTime >= jumpCoolTime; }
    }

    [Header("Debug")]
    [SerializeField] [Range(5f, 15f)] float jumpPower;
    [SerializeField] [Range(0.1f, 1f)] float jumpCoolTime;

    float lastJumpTime;
    
    #region PlayerStats
    
    public float HP { get { return Stats[StatsEnum.HP]; } }
    public float ATK { get { return Stats[StatsEnum.ATK]; } }
    public float DEF { get { return Stats[StatsEnum.DEF]; } }
    public float ATS { get { return Stats[StatsEnum.ATS]; } }
    public float SPD { get { return Stats[StatsEnum.SPD]; } }
    
    #endregion
    
    public override void Awake()
    {
        base.Awake();
        dataManager = FindObjectOfType<DataManager>();
        rigidBody2D = GetComponent<Rigidbody2D>();
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

    void Move(float h)
    {
        rigidBody2D.velocity = new Vector2(h * SPD, rigidBody2D.velocity.y);
    }

    void Jump()
    {
        lastJumpTime = Time.time;
        rigidBody2D.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }

}
