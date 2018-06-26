using System;

public interface ITube
{
    int Cid { get; }
    string Name { get; }
    string NameKor { get; }
    SocketEnum Socket { get; }
    TubeGradeEnum Grade { get; }
    string Company { get; }
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
    public string motion;
    public string hitValue;
    public AttackTypeEnum attackType;
    public string position;
    public float damage;
    public object combo;
    public bool hold;
    public string holdmotion;
    
    public TubeStyleStruct(int cid, string name, string nameKor, SocketEnum socket, TubeGradeEnum grade, string company, string motion, string hitValue, AttackTypeEnum attackType, string position, float damage, object combo, bool hold, string holdmotion)
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
        this.combo = combo;
        this.hold = hold;
        this.holdmotion = holdmotion;
    }

    public override string ToString()
    {
        return string.Format(" {0} | {1} | {2} | {3} | {4} | {5} | {6} | {7} | {8} | {9} | {10} | {11} | {12} | {13}" , cid, name, nameKor, socket, grade, company, motion, hitValue, attackType, position, damage, combo, hold, holdmotion);
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
    public string company;
    public string melee;
    public string range;
    public string bounce;
    public string instant;
    public int splash;
    
    public TubeEnhancerStruct(int cid, string name, string nameKor, SocketEnum socket, TubeGradeEnum grade, string company, string melee, string range, string bounce, string instant, int splash)
    {
        this.cid = cid;
        this.name = name;
        this.nameKor = nameKor;
        this.socket = socket;
        this.grade = grade;
        this.company = company;
        this.melee = melee;
        this.range = range;
        this.bounce = bounce;
        this.instant = instant;
        this.splash = splash;
    }

    public override string ToString()
    {
        return string.Format(" {0} | {1} | {2} | {3} | {4} | {5} | {6} | {7} | {8} | {9} | {10}" , cid, name, nameKor, socket, grade, company, melee, range, bounce, instant, splash);
    }
    
    public int Cid { get { return cid; } }
    public string Name { get { return name; } }
    public string NameKor { get { return nameKor; } }
    public SocketEnum Socket { get { return socket; } }
    public TubeGradeEnum Grade { get { return grade; } }
    public string Company { get { return company; } }
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