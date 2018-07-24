using System;

public class InverterNode : IParentBehaviourTreeNode
{
    string name;
    
    IBehaviourTreeNode childNode;

    public InverterNode(string name)
    {
        this.name = name;
    }

    public BehaviourTreeStatus Tick(TimeData time)
    {
        if (childNode == null)
        {
            throw new ApplicationException("InverterNode must have a child node!");
        }

        var result = childNode.Tick(time);
        if (result == BehaviourTreeStatus.Failure)
        {
            return BehaviourTreeStatus.Success;
        }

        if (result == BehaviourTreeStatus.Success)
        {
            return BehaviourTreeStatus.Failure;
        }

        return result;
    }


    public void AddChild(IBehaviourTreeNode child)
    {
        if (childNode != null)
        {
            throw new ApplicationException("Can't add more than a single child to InverterNode!");
        }

        childNode = child;
    }
}