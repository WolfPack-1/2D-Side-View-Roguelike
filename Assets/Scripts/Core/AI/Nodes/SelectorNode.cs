using System.Collections.Generic;

public class SelectorNode : IParentBehaviourTreeNode
{
    string name;

    List<IBehaviourTreeNode> children = new List<IBehaviourTreeNode>();

    public SelectorNode(string name)
    {
        this.name = name;
    }

    public BehaviourTreeStatus Tick(TimeData time)
    {
        foreach (var child in children)
        {
            var childStatus = child.Tick(time);
            if (childStatus != BehaviourTreeStatus.Failure)
            {
                return childStatus;
            }
        }

        return BehaviourTreeStatus.Failure;
    }

    public void AddChild(IBehaviourTreeNode child)
    {
        children.Add(child);
    }
}