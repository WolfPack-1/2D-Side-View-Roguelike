using System.Collections.Generic;

public class SequenceNode : IParentBehaviourTreeNode
{
    string name;

    List<IBehaviourTreeNode> children = new List<IBehaviourTreeNode>();

    public SequenceNode(string name)
    {
        this.name = name;
    }

    public BehaviourTreeStatus Tick(TimeData time)
    {
        foreach (var child in children)
        {
            var childStatus = child.Tick(time);
            if (childStatus != BehaviourTreeStatus.Success)
            {
                return childStatus;
            }
        }

        return BehaviourTreeStatus.Success;
    }

    public void AddChild(IBehaviourTreeNode child)
    {
        children.Add(child);
    }
}