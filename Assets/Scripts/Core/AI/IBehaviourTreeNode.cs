public interface IBehaviourTreeNode
{
    BehaviourTreeStatus Tick(TimeData time);
}

public interface IParentBehaviourTreeNode : IBehaviourTreeNode
{
    void AddChild(IBehaviourTreeNode child);
}