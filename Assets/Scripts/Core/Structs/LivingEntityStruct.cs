using System;

[Serializable]
public struct LivingEntityStruct
{
    public int cid;
    public string name;
    public string nameKor;
    public float hp;
    public float atk;
    public float def;
    public float ats;
    public float spd;

    public LivingEntityStruct(int cid, string name, string nameKor, float hp, float atk, float def, float ats, float spd)
    {
        this.cid = cid;
        this.name = name;
        this.nameKor = nameKor;
        this.hp = hp;
        this.atk = atk;
        this.def = def;
        this.ats = ats;
        this.spd = spd;
    }

    public override string ToString()
    {
        return string.Format("{0} | {1} | {2} | {3} | {4} | {5} | {6} | {7}", cid, name, nameKor, hp, atk, def, ats, spd);
    }
}