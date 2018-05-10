using System;

[Serializable]
public struct SkillStruct
{

    public int cid;
    public string name;
    public string name_kor;
    public object animset;
    public string attack_type;
    public string abnormal;

    public SkillStruct(int cid, string name, string nameKor, object animSet, string attackType, string abnormal)
    {
        this.cid = cid;
        this.name = name;
        this.name_kor = nameKor;
        this.animset = animSet;
        this.attack_type = attackType;
        this.abnormal = abnormal;
    }

    public override string ToString()
    {
        return string.Format("{0} | {1} | {2} | {3} | {4} | {5}", cid, name, name_kor, animset, attack_type, abnormal);
    }
}