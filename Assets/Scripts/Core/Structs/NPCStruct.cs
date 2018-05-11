using System;

[Serializable]
public struct NPCStruct
{

    public int cid;
    public string name;
    public string nameKor;
    public int hp;
    public EntityGradeEnum EntityGrade;
    public bool recognize;
    public int recognizeValue;
    public string skillValue;
    public string dropTable;
    
    public NPCStruct(int cid, string name, string nameKor, int hp, EntityGradeEnum entityGrade, bool recognize, int recognizeValue, string skillValue, string dropTable)
    {
        this.cid = cid;
        this.name = name;
        this.nameKor = nameKor;
        this.hp = hp;
        this.EntityGrade = entityGrade;
        this.recognize = recognize;
        this.recognizeValue = recognizeValue;
        this.skillValue = skillValue;
        this.dropTable = dropTable;
    }

    public override string ToString()
    {
        return string.Format("{0} | {1} | {2} | {3} | {4} | {5} | {6} | {7} | {8}", cid, name, nameKor, hp, EntityGrade, recognize, recognizeValue, skillValue, dropTable);
    }
}