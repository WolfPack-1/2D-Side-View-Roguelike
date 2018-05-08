using System;

[Serializable]
public struct NPCStruct
{
    public int cid;
    public string name;
    public string nameKor;
    public string animSet;
    public string attackType;
    public int attackValue;
    public TargetEnum attackFunction;
    public float atk;
    public float coolTime;
    public string skill;
    public int hp;
    public GradeEnum grade;
    public bool recognize;
    public int recognizeValue;
    public float spd;
    public float ats;
    public string dropTable;
    public float def;


    public NPCStruct(int cid, string name, string nameKor, string animSet, string attackType, int attackValue, TargetEnum attackFunction, float atk, float coolTime, string skill, int hp, GradeEnum grade, bool recognize, int recognizeValue, float spd, float ats, string dropTable, float def)
    {
        this.cid = cid;
        this.name = name;
        this.nameKor = nameKor;
        this.animSet = animSet;
        this.attackType = attackType;
        this.attackValue = attackValue;
        this.attackFunction = attackFunction;
        this.atk = atk;
        this.coolTime = coolTime;
        this.skill = skill;
        this.hp = hp;
        this.grade = grade;
        this.recognize = recognize;
        this.recognizeValue = recognizeValue;
        this.spd = spd;
        this.ats = ats;
        this.dropTable = dropTable;
        this.def = def;
    }

    public override string ToString()
    {
        return string.Format("{0} | {1} | {2} | {3} | {4} | {5} | {6} | {7} | {8} | {9} | {10} | {11} | {12} | {13} | {14} | {15} | {16} | {17}", cid, name, nameKor, animSet, attackType, attackValue, attackFunction, atk, coolTime, skill, hp, grade, recognize, recognizeValue, spd, ats, dropTable, def);
    }
}