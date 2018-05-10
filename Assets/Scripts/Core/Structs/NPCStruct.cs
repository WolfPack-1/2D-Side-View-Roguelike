using System;

[Serializable]
public struct NPCStruct
{
    public int cid;
    public string name;
    public string name_kor;
    public string animset;
    public string attack_type;
    public int attack_value;
    public TargetEnum attack_function;
    public float ATK;
    public float cooltime;
    public string skill;
    public int hp;
    public GradeEnum grade;
    public bool recognize;
    public int recognize_value;
    public float SPD;
    public float ATS;
    public string drop_table;
    public float DEF;

    public NPCStruct(int cid, string name, string nameKor, string animset, string attackType, int attackValue, TargetEnum attackFunction, float atk, float coolTime, string skill, int hp, GradeEnum grade, bool recognize, int recognizeValue, float spd, float ats, string dropTable, float def)
    {
        this.cid = cid;
        this.name = name;
        this.name_kor = nameKor;
        this.animset = animset;
        this.attack_type = attackType;
        this.attack_value = attackValue;
        this.attack_function = attackFunction;
        this.ATK = atk;
        this.cooltime = coolTime;
        this.skill = skill;
        this.hp = hp;
        this.grade = grade;
        this.recognize = recognize;
        this.recognize_value = recognizeValue;
        this.SPD = spd;
        this.ATS = ats;
        this.drop_table = dropTable;
        this.DEF = def;
    }

    public override string ToString()
    {
        return string.Format("{0} | {1} | {2} | {3} | {4} | {5} | {6} | {7} | {8} | {9} | {10} | {11} | {12} | {13} | {14} | {15} | {16} | {17}", cid, name, name_kor, animset, attack_type, attack_value, attack_function, ATK, cooltime, skill, hp, grade, recognize, recognize_value, SPD, ATS, drop_table, DEF);
    }
}