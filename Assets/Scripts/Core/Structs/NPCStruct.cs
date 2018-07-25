using System;

[Serializable]
public struct NPCStruct
{

    public int cid;
    public string name;
    public string nameKor;
    public EntityGradeEnum grade;
    public bool recognize;
    public int recognizeValue;
    public string skillValue;
    public string dropTable;
    
    public NPCStruct(int cid, string name, string nameKor, EntityGradeEnum grade, bool recognize, int recognizeValue, string skillValue, string dropTable)
    {
        this.cid = cid;
        this.name = name;
        this.nameKor = nameKor;
        this.grade = grade;
        this.recognize = recognize;
        this.recognizeValue = recognizeValue;
        this.skillValue = skillValue;
        this.dropTable = dropTable;
    }

    public override string ToString()
    {
        return string.Format("{0} | {1} | {2} | {3} | {4} | {5} | {6} | {7}", cid, name, nameKor, grade, recognize, recognizeValue, skillValue, dropTable);
    }
}