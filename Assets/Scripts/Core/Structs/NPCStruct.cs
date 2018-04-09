using System;

[Serializable]
public struct NPCStruct
{
    public int cid;
    public string name;
    public string nameKor;
    public object animSet;
    public string attackType;
    public int attackValue;
    public TargetEnum attackFunction;
    public float attackDamage;
    public float coolTime;
    public object skill;
    public int hp;
    public GradeEnum grade;
    public bool recognize;
    public int recognizeValue;
    public float speed;
    public float attackSpeed;
    public string dropTable;


    public NPCStruct(int cid, string name, string nameKor, object animSet, string attackType, int attackValue, TargetEnum attackFunction, float attackDamage, float coolTime, object skill, int hp, GradeEnum grade, bool recognize, int recognizeValue, float speed, float attackSpeed, string dropTable)
    {
        this.cid = cid;
        this.name = name;
        this.nameKor = nameKor;
        this.animSet = animSet;
        this.attackType = attackType;
        this.attackValue = attackValue;
        this.attackFunction = attackFunction;
        this.attackDamage = attackDamage;
        this.coolTime = coolTime;
        this.skill = skill;
        this.hp = hp;
        this.grade = grade;
        this.recognize = recognize;
        this.recognizeValue = recognizeValue;
        this.speed = speed;
        this.attackSpeed = attackSpeed;
        this.dropTable = dropTable;
    }

    public override string ToString()
    {
        return string.Format("{0} | {1} | {2} | {3} | {4} | {5} | {6} | {7} | {8} | {9} | {10} | {11} | {12} | {13} | {14} | {15} | {16}", cid, name, nameKor, animSet, attackType, attackValue, attackFunction, attackDamage, coolTime, skill, hp, grade, recognize, recognizeValue, speed, attackSpeed, dropTable);
    }
}