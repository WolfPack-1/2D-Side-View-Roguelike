using System;

public interface ITube
{
    int Cid { get; }
    string Name { get; }
    string NameKor { get; }
    SocketEnum Socket { get; }
    TubeGradeEnum Grade { get; }
}

[Serializable]
public struct TubeStyleStruct : ITube
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
    
    
    public TubeStyleStruct(int cid, string name, string nameKor, SocketEnum socket, TubeGradeEnum grade, string company, float range, AttackTypeEnum attackType, string position, float damage, int combo, bool hold, string holdmotion)
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
public struct TubeCoolerStruct : ITube
{
    public int cid;
    public string name;
    public string nameKor;
    public SocketEnum socket;
    public TubeGradeEnum grade;
    public string company;
    public float cooltime;
    
    public TubeCoolerStruct(int cid, string name, string nameKor, SocketEnum socket, TubeGradeEnum grade, string company, float cooltime)
    {
        this.cid = cid;
        this.name = name;
        this.nameKor = nameKor;
        this.socket = socket;
        this.grade = grade;
        this.company = company;
        this.cooltime = cooltime;
    }

    public override string ToString()
    {
        return string.Format(" {0} | {1} | {2} | {3} | {4} | {5} | {6}" , cid, name, nameKor, socket, grade, company, cooltime);
    }
    
    public int Cid { get { return cid; } }
    public string Name { get { return name; } }
    public string NameKor { get { return nameKor; } }
    public SocketEnum Socket { get { return socket; } }
    public TubeGradeEnum Grade { get { return grade; } }
    public string Company { get { return company; } }
}

[Serializable]
public struct TubeEnhancerStruct : ITube
{
    public int cid;
    public string name;
    public string nameKor;
    public SocketEnum socket;
    public TubeGradeEnum grade;
    public bool meleeSocket;
    public bool rangeSocket;
    public bool bounceSocket;
    public bool dashSocket;
    public float range;
    public int abnormalValue;
    
    public TubeEnhancerStruct(int cid, string name, string nameKor, SocketEnum socket, TubeGradeEnum grade, bool meleeSocket, bool rangeSocket, bool bounceSocket, bool dashSocket, float range, int abnormalValue)
    {
        this.cid = cid;
        this.name = name;
        this.nameKor = nameKor;
        this.socket = socket;
        this.grade = grade;
        this.meleeSocket = meleeSocket;
        this.rangeSocket = rangeSocket;
        this.bounceSocket = bounceSocket;
        this.dashSocket = dashSocket;
        this.range = range;
        this.abnormalValue = abnormalValue;
    }

    public override string ToString()
    {
        return string.Format(" {0} | {1} | {2} | {3} | {4} | {5} | {6} | {7} | {8} | {9}" , cid, name, nameKor, socket, meleeSocket, rangeSocket, bounceSocket, dashSocket, range, abnormalValue);
    }

    public int Cid { get { return cid; } set { cid = value; } }
    public string Name { get { return name; } set { name = value; } }
    public string NameKor { get { return nameKor; } set { nameKor = value; } }
    public SocketEnum Socket { get { return socket; } set { socket = value; } }
    public bool MeleeSocket { get { return meleeSocket; } set { meleeSocket = value; } }
    public bool RangeSocket { get { return rangeSocket; } set { rangeSocket = value; } }
    public bool BounceSocket { get { return bounceSocket; } set { bounceSocket = value; } }
    public bool DashSocket { get { return dashSocket; } set { dashSocket = value; } }
    public float Range { get { return range; } set { range = value; } }
    public int AbnormalValue { get { return abnormalValue; } set { abnormalValue = value; } }
    public TubeGradeEnum Grade { get { return grade; } }
}

[Serializable]
public struct TubeRelicStruct : ITube
{
    public int cid;
    public string name;
    public string nameKor;
    public SocketEnum socket;
    public TubeGradeEnum grade;
    public string company;
    public string abnormalValue;
    public float distance;
    
    public TubeRelicStruct(int cid, string name, string nameKor, SocketEnum socket, TubeGradeEnum grade, string company, string abnormalValue, float distance)
    {
        this.cid = cid;
        this.name = name;
        this.nameKor = nameKor;
        this.socket = socket;
        this.grade = grade;
        this.company = company;
        this.abnormalValue = abnormalValue;
        this.distance = distance;
    }

    public override string ToString()
    {
        return string.Format(" {0} | {1} | {2} | {3} | {4} | {5} | {6} | {7}" , cid, name, nameKor, socket, grade, company, abnormalValue, distance);
    }
    
    public int Cid { get { return cid; } }
    public string Name { get { return name; } }
    public string NameKor { get { return nameKor; } }
    public SocketEnum Socket { get { return socket; } }
    public TubeGradeEnum Grade { get { return grade; } }
    public string Company { get { return company; } }
}