using System;

[Serializable]
public struct TubeStruct
{
    public int cid;
    public string name;
    public string nameKor;
    public SocketEnum socket;
    public TubeGradeEnum grade;
    public string company;
    public string motion;
    public string hitValue;
    public string attackType;
    public string position;
    public float damage;
    public string melee;
    public string range;
    public string bounce;
    public string instant;
    public string splash;
    public float cooltime;
    public string abnormalValue;
    public float distance;

    public TubeStruct(int cid, string name, string nameKor, SocketEnum socket, TubeGradeEnum grade, string company, string motion, string hitValue, string attackType, string position, float damage, string melee, string range, string bounce, string instant, string splash, float cooltime, string abnormalValue, float distance)
    {
        this.cid = cid;
        this.name = name;
        this.nameKor = nameKor;
        this.socket = socket;
        this.grade = grade;
        this.company = company;
        this.motion = motion;
        this.hitValue = hitValue;
        this.attackType = attackType;
        this.position = position;
        this.damage = damage;
        this.melee = melee;
        this.range = range;
        this.bounce = bounce;
        this.instant = instant;
        this.splash = splash;
        this.cooltime = cooltime;
        this.abnormalValue = abnormalValue;
        this.distance = distance;
    }

    public override string ToString()
    {
        return string.Format(" {0} | {1} | {2} | {3} | {4} | {5} | {6} | {7} | {8} | {9} | {10} | {11} | {12} | {13} | {14} | {15} | {16} | {17} | {18}" , cid, name, nameKor, socket, grade, company, motion, hitValue, attackType, position, damage, melee, range, bounce, instant, splash, cooltime, abnormalValue, distance);
    }
}