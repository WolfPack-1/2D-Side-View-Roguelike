using System;

public class ActionNode : IBehaviourTreeNode
{
    string name;

    Func<TimeData, BehaviourTreeStatus> fn;
        

    public ActionNode(string name, Func<TimeData, BehaviourTreeStatus> fn)
    {
        this.name=name;
        this.fn=fn;
    }

    public BehaviourTreeStatus Tick(TimeData time)
    {
        return fn(time);
    }
}