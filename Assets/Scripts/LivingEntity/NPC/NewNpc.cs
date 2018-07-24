using System.Collections;
using UnityEngine;

public class NewNpc : MonoBehaviour
{
    IBehaviourTreeNode tree;
    Player targetPlayer;
    float lastAttackTime;
    
    void Start()
    {
        InitTree();
    }

    void InitTree()
    {
        BehaviourTree builder = new BehaviourTree();
        tree =
            builder
                .Selector("Root")
                    .Sequence("Search And Attack")
                        .Condition
                        (
                            "Check Attack CoolTime", t =>
                            {
                                return Time.time - lastAttackTime > 3;
                            }
                        )
                        .Condition
                        (
                            "Search Player", t =>
                            {
                                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * Mathf.Sign(transform.localScale.x), 1000, 1 << LayerMask.NameToLayer("Player"));
                                if (!hit)
                                {
                                    targetPlayer = null;
                                    return false;
                                }
    
                                targetPlayer = hit.transform.GetComponent<Player>();
                                Debug.Log("Find Player!");
                                return true;
                            }
                        )
                        .Condition
                        (
                            "Determine Player Distance", t =>
                            {
                                float distance = Vector2.Distance(transform.position, targetPlayer.transform.position);
                                Debug.Log(distance);
                                return distance <= 3.0f;
                            }
                        )
                        .Sequence("Attack Player")
                            .Condition
                            (
                                "Check Attack CoolTime", t =>
                                {
                                    return Time.time - lastAttackTime > 3;
                                }
                            )
                            .Do
                            (
                                "Attack",
                                t =>
                                {
                                    Debug.Log("Attack");
                                    lastAttackTime = Time.time;
                                    return BehaviourTreeStatus.Success;
                                }
                            )
                            .End()
                        .End()
                .Do
                (
                    "Idle", t =>
                    {
                        Debug.Log("Idle");
                        return BehaviourTreeStatus.Success;
                    }
                )
                .End()
                .Build();
            
    }

    void Update()
    {
        tree.Tick(new TimeData(Time.deltaTime));
    }
}