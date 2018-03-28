using System;

[Serializable]
public struct GimmickStruct
{
    public int cid;
    public string name;
    public string functionType;
    public int functionValue;

    public GimmickStruct(int cid, string name, string functionType, int functionValue)
    {
        this.cid = cid;
        this.name = name;
        this.functionType = functionType;
        this.functionValue = functionValue;
    }

    public override string ToString()
    {
        return string.Format("{0} | {1} | {2} | {3}", cid, name, functionType, functionType);
    }
}