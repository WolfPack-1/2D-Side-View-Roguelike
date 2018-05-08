using System;

[Serializable]
public struct GimmickStruct
{
    public int cid;
    public string name;
    public string function_type;
    public int function_value;

    public GimmickStruct(int cid, string name, string functionType, int functionValue)
    {
        this.cid = cid;
        this.name = name;
        this.function_type = functionType;
        this.function_value = functionValue;
    }

    public override string ToString()
    {
        return string.Format("{0} | {1} | {2} | {3}", cid, name, function_type, function_type);
    }
}