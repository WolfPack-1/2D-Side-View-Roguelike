using System;
using UnityEngine;

public interface IAbnormalType
{
    
}

[Serializable]
public struct AbnormalTypeSlow : IAbnormalType
{
    int percentValue;
    int maxStack;
    int duration;
    StackDecreaseTypeEnum stackDecreseType;

    public int PercentValue { get { return percentValue; } }
    public int MaxStack { get { return maxStack; } }
    public int Duration { get { return duration; } }
    public StackDecreaseTypeEnum StackDecreaseType { get { return stackDecreseType; } }

    public AbnormalTypeSlow(string[] data)
    {
        Debug.Assert(data.Length == 4);
        percentValue = int.Parse(data[1]);
        maxStack = int.Parse(data[2]);
        duration = int.Parse(data[3]);
        stackDecreseType = (StackDecreaseTypeEnum)Enum.Parse(typeof(StackDecreaseTypeEnum),data[4]);
    }
}

[Serializable]
public struct AbnormalTypeSlience : IAbnormalType
{
    int duration;

    public int Duration { get { return duration; } }

    public AbnormalTypeSlience(string[] data)
    {
        Debug.Assert(data.Length == 2);
        duration = int.Parse(data[1]);
    }
}

[Serializable]
public struct AbnormalTypeNearsight: IAbnormalType
{
    int intValue;
    int duration;

    public int IntValue { get { return intValue; } }
    public int Duration { get { return duration; } }

    public AbnormalTypeNearsight(string[] data)
    {
        Debug.Assert(data.Length == 3);
        intValue = int.Parse(data[1]);
        duration = int.Parse(data[2]);
    }
}

[Serializable]
public struct AbnormalTypeSnare: IAbnormalType
{
    int duration;

    public int Duration { get { return duration; } }

    public AbnormalTypeSnare(string[] data)
    {
        Debug.Assert(data.Length == 2);
        duration = int.Parse(data[1]);
    }
}

[Serializable]
public struct AbnormalTypeKnockback: IAbnormalType
{
    int intValue;

    public int IntValue { get { return intValue; } }

    public AbnormalTypeKnockback(string[] data)
    {
        Debug.Assert(data.Length == 2);
        intValue = int.Parse(data[1]);
    }
}

[Serializable]
public struct AbnormalTypeTaunt: IAbnormalType
{
    int duration;

    public int Duration { get { return duration; } }

    public AbnormalTypeTaunt(string[] data)
    {
        Debug.Assert(data.Length == 2);
        duration = int.Parse(data[1]);
    }
}

[Serializable]
public struct AbnormalTypeCharm: IAbnormalType
{
    int duration;

    public int Duration { get { return duration; } }

    public AbnormalTypeCharm(string[] data)
    {
        Debug.Assert(data.Length == 2);
        duration = int.Parse(data[1]);
    }
}

[Serializable]
public struct AbnormalTypeFlee: IAbnormalType
{
    int duration;

    public int Duration { get { return duration; } }

    public AbnormalTypeFlee(string[] data)
    {
        Debug.Assert(data.Length == 2);
        duration = int.Parse(data[1]);
    }
}

[Serializable]
public struct AbnormalTypeDivine: IAbnormalType
{
    int maxStack;
    int duration;
    StackDecreaseTypeEnum stackDecreseType;

    public int MaxStack { get { return maxStack; } }
    public int Duration { get { return duration; } }
    public StackDecreaseTypeEnum StackDecreaseType { get { return stackDecreseType; } }

    public AbnormalTypeDivine(string[] data)
    {
        Debug.Assert(data.Length == 4);
        maxStack = int.Parse(data[1]);
        duration = int.Parse(data[2]);
        stackDecreseType = (StackDecreaseTypeEnum)Enum.Parse(typeof(StackDecreaseTypeEnum),data[4]);
    }
}

[Serializable]
public struct AbnormalTypePoison: IAbnormalType
{
    int tickValue;
    int maxStack;
    int duration;
    StackDecreaseTypeEnum stackDecreseType;

    public int TickValue { get { return tickValue; } }
    public int MaxStack { get { return maxStack; } }
    public int Duration { get { return duration; } }
    public StackDecreaseTypeEnum StackDecreaseType { get { return stackDecreseType; } }

    public AbnormalTypePoison(string[] data)
    {
        Debug.Assert(data.Length == 5);
        tickValue = int.Parse(data[1]);
        maxStack = int.Parse(data[2]);
        duration = int.Parse(data[3]);
        stackDecreseType = (StackDecreaseTypeEnum)Enum.Parse(typeof(StackDecreaseTypeEnum),data[4]);
    }
}

[Serializable]
public struct AbnormalTypeFlame: IAbnormalType
{
    int tickValue;
    int percentValue;
    int maxStack;
    int duration;
    StackDecreaseTypeEnum stackDecreseType;

    public int TickValue { get { return tickValue; } }
    public int PercentValue { get { return percentValue; } }
    public int MaxStack { get { return maxStack; } }
    public int Duration { get { return duration; } }
    public StackDecreaseTypeEnum StackDecreaseType { get { return stackDecreseType; } }

    public AbnormalTypeFlame(string[] data)
    {
        Debug.Assert(data.Length == 6);
        tickValue = int.Parse(data[1]);
        percentValue = int.Parse(data[2]);
        maxStack = int.Parse(data[2]);
        duration = int.Parse(data[3]);
        stackDecreseType = (StackDecreaseTypeEnum)Enum.Parse(typeof(StackDecreaseTypeEnum),data[4]);
    }
}

[Serializable]
public struct AbnormalTypeUnweapon: IAbnormalType
{
    int percentValue;
    int maxStack;
    int duration;
    StackDecreaseTypeEnum stackDecreseType;

    public int PercentValue { get { return percentValue; } }
    public int MaxStack { get { return maxStack; } }
    public int Duration { get { return duration; } }
    public StackDecreaseTypeEnum StackDecreaseType { get { return stackDecreseType; } }

    public AbnormalTypeUnweapon(string[] data)
    {
        Debug.Assert(data.Length == 5);
        percentValue = int.Parse(data[1]);
        maxStack = int.Parse(data[2]);
        duration = int.Parse(data[3]);
        stackDecreseType = (StackDecreaseTypeEnum)Enum.Parse(typeof(StackDecreaseTypeEnum),data[4]);
    }
}

[Serializable]
public struct AbnormalTypeUnarmor: IAbnormalType
{
    int percentValue;
    int maxStack;
    int duration;
    StackDecreaseTypeEnum stackDecreseType;

    public int PercentValue { get { return percentValue; } }
    public int MaxStack { get { return maxStack; } }
    public int Duration { get { return duration; } }
    public StackDecreaseTypeEnum StackDecreaseType { get { return stackDecreseType; } }

    public AbnormalTypeUnarmor(string[] data)
    {
        Debug.Assert(data.Length == 5);
        percentValue = int.Parse(data[1]);
        maxStack = int.Parse(data[2]);
        duration = int.Parse(data[3]);
        stackDecreseType = (StackDecreaseTypeEnum)Enum.Parse(typeof(StackDecreaseTypeEnum),data[4]);
    }
}

[Serializable]
public struct AbnormalTypeUpweapon: IAbnormalType
{
    int percentValue;
    int maxStack;
    int duration;
    StackDecreaseTypeEnum stackDecreseType;

    public int PercentValue { get { return percentValue; } }
    public int MaxStack { get { return maxStack; } }
    public int Duration { get { return duration; } }
    public StackDecreaseTypeEnum StackDecreaseType { get { return stackDecreseType; } }

    public AbnormalTypeUpweapon(string[] data)
    {
        Debug.Assert(data.Length == 5);
        percentValue = int.Parse(data[1]);
        maxStack = int.Parse(data[2]);
        duration = int.Parse(data[3]);
        stackDecreseType = (StackDecreaseTypeEnum)Enum.Parse(typeof(StackDecreaseTypeEnum),data[4]);
    }
}

[Serializable]
public struct AbnormalTypeUparmor: IAbnormalType
{
    int percentValue;
    int maxStack;
    int duration;
    StackDecreaseTypeEnum stackDecreseType;

    public int PercentValue { get { return percentValue; } }
    public int MaxStack { get { return maxStack; } }
    public int Duration { get { return duration; } }
    public StackDecreaseTypeEnum StackDecreaseType { get { return stackDecreseType; } }

    public AbnormalTypeUparmor(string[] data)
    {
        Debug.Assert(data.Length == 5);
        percentValue = int.Parse(data[1]);
        maxStack = int.Parse(data[2]);
        duration = int.Parse(data[3]);
        stackDecreseType = (StackDecreaseTypeEnum)Enum.Parse(typeof(StackDecreaseTypeEnum),data[4]);
    }
}

[Serializable]
public struct AbnormalTypeCure: IAbnormalType
{
    int tickValue;
    int maxStack;
    int duration;
    StackDecreaseTypeEnum stackDecreseType;

    public int TickValue { get { return tickValue; } }
    public int MaxStack { get { return maxStack; } }
    public int Duration { get { return duration; } }
    public StackDecreaseTypeEnum StackDecreaseType { get { return stackDecreseType; } }

    public AbnormalTypeCure(string[] data)
    {
        Debug.Assert(data.Length == 5);
        tickValue = int.Parse(data[1]);
        maxStack = int.Parse(data[2]);
        duration = int.Parse(data[3]);
        stackDecreseType = (StackDecreaseTypeEnum)Enum.Parse(typeof(StackDecreaseTypeEnum),data[4]);
    }
}

[Serializable]
public struct AbnormalTypeHaste: IAbnormalType
{
    int percentValue;
    int maxStack;
    int duration;
    StackDecreaseTypeEnum stackDecreseType;

    public int PercentValue { get { return percentValue; } }
    public int MaxStack { get { return maxStack; } }
    public int Duration { get { return duration; } }
    public StackDecreaseTypeEnum StackDecreaseType { get { return stackDecreseType; } }

    public AbnormalTypeHaste(string[] data)
    {
        Debug.Assert(data.Length == 5);
        percentValue = int.Parse(data[1]);
        maxStack = int.Parse(data[2]);
        duration = int.Parse(data[3]);
        stackDecreseType = (StackDecreaseTypeEnum)Enum.Parse(typeof(StackDecreaseTypeEnum),data[4]);
    }
}