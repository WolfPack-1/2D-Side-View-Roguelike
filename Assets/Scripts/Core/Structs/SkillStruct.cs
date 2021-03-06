﻿using System;

[Serializable]
[Obsolete]
public struct SkillStruct
{

    public int cid;
    public string name;
    public string nameKor;
    public string animset;
    public AttackTypeEnum attackType;
    public string abnormal;

    public SkillStruct(int cid, string name, string nameKor, string animSet, AttackTypeEnum attackType, string abnormal)
    {
        this.cid = cid;
        this.name = name;
        this.nameKor = nameKor;
        this.animset = animSet;
        this.attackType = attackType;
        this.abnormal = abnormal;
    }

    public override string ToString()
    {
        return string.Format("{0} | {1} | {2} | {3} | {4} | {5}", cid, name, nameKor, animset, attackType, abnormal);
    }
}