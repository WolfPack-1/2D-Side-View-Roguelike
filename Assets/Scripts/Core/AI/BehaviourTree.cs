using System;
using System.Collections.Generic;

public enum BehaviourTreeStatus
{
    Success,
    Failure,
    Running
}

public struct TimeData
{
    public TimeData(float deltaTime)
    {
        this.deltaTime = deltaTime;
    }

    public float deltaTime;
}

public class BehaviourTree
{
    IBehaviourTreeNode curNode = null;
    Stack<IParentBehaviourTreeNode> parentNodeStack = new Stack<IParentBehaviourTreeNode>();
    public BehaviourTree Do(string name, Func<TimeData, BehaviourTreeStatus> fn)
    {
        if (parentNodeStack.Count <= 0)
        {
            throw new ApplicationException("Can't create an unnested ActionNode, it must be a leaf node.");
        }

        var actionNode = new ActionNode(name, fn);
        parentNodeStack.Peek().AddChild(actionNode);
        return this;
    }
    
    public BehaviourTree Condition(string name, Func<TimeData, bool> fn)
    {
        return Do(name, t => fn(t) ? BehaviourTreeStatus.Success : BehaviourTreeStatus.Failure);
    }
    
    public BehaviourTree Inverter(string name)
    {
        var inverterNode = new InverterNode(name);

        if (parentNodeStack.Count > 0)
        {
            parentNodeStack.Peek().AddChild(inverterNode);
        }

        parentNodeStack.Push(inverterNode);
        return this;
    }
    
    public BehaviourTree Sequence(string name)
    {
        var sequenceNode = new SequenceNode(name);

        if (parentNodeStack.Count > 0)
        {
            parentNodeStack.Peek().AddChild(sequenceNode);
        }

        parentNodeStack.Push(sequenceNode);
        return this;
    }
    
    public BehaviourTree Parallel(string name, int numRequiredToFail, int numRequiredToSucceed)
    {
        var parallelNode = new ParallelNode(name, numRequiredToFail, numRequiredToSucceed);

        if (parentNodeStack.Count > 0)
        {
            parentNodeStack.Peek().AddChild(parallelNode);
        }

        parentNodeStack.Push(parallelNode);
        return this;
    }
    
    public BehaviourTree Selector(string name)
    {
        var selectorNode = new SelectorNode(name);

        if (parentNodeStack.Count > 0)
        {
            parentNodeStack.Peek().AddChild(selectorNode);
        }

        parentNodeStack.Push(selectorNode);
        return this;
    }
    
    public BehaviourTree Splice(IBehaviourTreeNode subTree)
    {
        if (subTree == null)
        {
            throw new ArgumentNullException("subTree");
        }

        if (parentNodeStack.Count <= 0)
        {
            throw new ApplicationException("Can't splice an unnested sub-tree, there must be a parent-tree.");
        }

        parentNodeStack.Peek().AddChild(subTree);
        return this;
    }
    
    public IBehaviourTreeNode Build()
    {
        if (curNode == null)
        {
            throw new ApplicationException("Can't create a behaviour tree with zero nodes");
        }
        return curNode;
    }
    
    public BehaviourTree End()
    {
        curNode = parentNodeStack.Pop();
        return this;
    }
}
