using System;

[Serializable]
public struct AbnormalStruct
{
    public int cid;
    public string name;
    public string nameKor;
    public string abnormalValue;
    public AbnormalTargetEnum target;

    public AbnormalStruct(int cid, string name, string nameKor, string abnormalValue, AbnormalTargetEnum target)
    {
        this.cid = cid;
        this.name = name;
        this.nameKor = nameKor;
        this.abnormalValue = abnormalValue;
        this.target = target;
    }

    public override string ToString()
    {
        return string.Format("{0} | {1} | {2} | {3} | {4}", cid, name, nameKor, abnormalValue, target);
    }
}