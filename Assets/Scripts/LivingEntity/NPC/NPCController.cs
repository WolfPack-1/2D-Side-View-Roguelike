using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : Controller2D
{
    [SerializeField] float jumpHeight = 4;
    [SerializeField] float timeToJumpapex = 0.4f;
    
    NPC npc;
    
    float gravity;
    float jumpVelocity;
    Vector2 velocity;
    Vector2 input;

    Skill currentSkill;
    bool isUsingSkill;
    
    public bool IsUsingSkill { get { return isUsingSkill; } }

    protected override void Awake()
    {
        base.Awake();
        npc = GetComponent<NPC>();
    }
    
    protected override void Start()
    {
        base.Start();
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpapex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpapex;
    }

    protected override void Update()
    {
        base.Update();
        CalculateVelocity();
        Move(velocity * Time.deltaTime, input);
        if (collisions.above || collisions.below)
            velocity.y = 0;
    }

    void CalculateVelocity()
    {
        velocity.x = npc.CanWalk ? input.x * npc.MoveSpeed : 0;
        velocity.y += gravity * Time.deltaTime;
    }

    public Vector2 SetInput(Vector2 input)
    {
        this.input = input;
        return input;
    }

    public IEnumerator UseSkill(Skill skill, bool canStop)
    {
        currentSkill = skill;
        skill.Use(b => isUsingSkill = b, skill.IsFirstSkill);
        yield return new WaitUntil(() => !isUsingSkill);
        currentSkill = null;
    }

    public IEnumerator MoveToRandomPosition()
    {
        float time = Random.Range(0.5f, 1f);
        float timer = 0;
        input = Random.Range(0, 2) == 0 ? Vector2.left : Vector2.right;
        while (true)
        {
            if (timer >= time)
                break;
            timer += Time.deltaTime;
            yield return null;
        }
        input = Vector2.zero;
    }

    public IEnumerator MoveToTarget(LivingEntity target, float distance)
    {
        while (true)
        {
            float targetDistance = Vector2.Distance(transform.position, target.transform.position);
            if (targetDistance <= distance)
                break;
            float directionX = Mathf.Sign((target.transform.position - transform.position).x);

            input = new Vector2(directionX, 0);
            yield return null;
        }

        input = Vector2.zero;
    }

    public void AnimationFinished()
    {
        currentSkill.AnimationFinishedEvent();
    }

    public void AnimationAttackEvent()
    {
        currentSkill.AttackEvent();
    }
}
