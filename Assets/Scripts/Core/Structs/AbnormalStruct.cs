using System;

[Serializable]
public struct AbnormalStruct
{
    public int cid;
    public string name;
    public string name_kor;
    public string abnormal_Type;
    public AbnormalTargetEnum target;

    public AbnormalStruct(int cid, string name, string nameKor, string abnormalType, AbnormalTargetEnum target)
    {
        this.cid = cid;
        this.name = name;
        this.name_kor = nameKor;
        this.abnormal_Type = abnormalType;
        this.target = target;
    }

    public override string ToString()
    {
        return string.Format("{0} | {1} | {2} | {3} | {4}", cid, name, name_kor, abnormal_Type, target);
    }
}