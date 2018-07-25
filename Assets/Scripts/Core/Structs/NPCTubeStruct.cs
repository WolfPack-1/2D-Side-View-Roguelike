using System;

[Serializable]
public struct TubeNPCStyleStruct : ITube
{
    public int cid;
    public string name;
    public string nameKor;
    public SocketEnum socket;
    public TubeGradeEnum grade;
    public string company;
    public float range;
    public AttackTypeEnum attackType;
    public string position;
    public float damage;
    public int combo;
    public bool hold;
    public string holdmotion;
    public float coolTime;
    
    
    public TubeNPCStyleStruct(int cid, string name, string nameKor, SocketEnum socket, TubeGradeEnum grade, string company, float range, AttackTypeEnum attackType, string position, float damage, int combo, bool hold, string holdmotion, float coolTime)
    {
        this.cid = cid;
        this.name = name;
        this.nameKor = nameKor;
        this.socket = socket;
        this.grade = grade;
        this.company = company;
        this.range = range;
        this.attackType = attackType;
        this.position = position;
        this.damage = damage;
        this.combo = combo;
        this.hold = hold;
        this.holdmotion = holdmotion;
        this.coolTime = coolTime;
    }

    public override string ToString()
    {
        return string.Format(" {0} | {1} | {2} | {3} | {4} | {5} | {6} | {7} | {8} | {9} | {10} | {11}" , cid, name, nameKor, socket, grade, company, attackType, position, damage, combo, hold, holdmotion);
    }

    public int Cid { get { return cid; } }
    public string Name { get { return name; } }
    public string NameKor { get { return nameKor; } }
    public SocketEnum Socket { get { return socket; } }
    public TubeGradeEnum Grade { get { return grade; } }
    public string Company { get { return company; } }
}

[Serializable]
public struct TubeNPCCoolerStruct : ITube
{
    public int cid;
    public string name;
    public string nameKor;
    public SocketEnum socket;
    public TubeGradeEnum grade;
    public float cooltime;
    public float hp;
    public float steam;
    
    public TubeNPCCoolerStruct(int cid, string name, string nameKor, SocketEnum socket, TubeGradeEnum grade, float cooltime, float hp, float steam)
    {
        this.cid = cid;
        this.name = name;
        this.nameKor = nameKor;
        this.socket = socket;
        this.grade = grade;
        this.cooltime = cooltime;
        this.hp = hp;
        this.steam = steam;
    }
    
    public int Cid { get { return cid; } }
    public string Name { get { return name; } }
    public string NameKor { get { return nameKor; } }
    public SocketEnum Socket { get { return socket; } }
    public TubeGradeEnum Grade { get { return grade; } }
}

[Serializable]
public struct TubeNPCEnhancerStruct : ITube
{
    public int cid;
    public string name;
    public string nameKor;
    public SocketEnum socket;
    public TubeGradeEnum grade;
    public string company;
    public float range;
    public float coolTime;

    public TubeNPCEnhancerStruct(int cid, string name, string nameKor, SocketEnum socket, TubeGradeEnum grade, string company, float range, float coolTime)
    {
        this.cid = cid;
        this.name = name;
        this.nameKor = nameKor;
        this.socket = socket;
        this.grade = grade;
        this.company = company;
        this.range = range;
        this.coolTime = coolTime;
    }

    public int Cid { get { return cid; } set { cid = value; } }
    public string Name { get { return name; } set { name = value; } }
    public string NameKor { get { return nameKor; } set { nameKor = value; } }
    public SocketEnum Socket { get { return socket; } set { socket = value; } }
    public TubeGradeEnum Grade { get { return grade; } }
}