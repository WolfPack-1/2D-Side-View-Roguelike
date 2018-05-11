using System;

[Serializable]
public struct FxStruct
{
    public int cid;
    public string name;
    public string nameKor;
    public string spriteName;
    public bool upper;
    public bool middleUpper;
    public bool middle;
    public bool middleDown;
    public bool backSide;
    
    public FxStruct(int cid, string name, string nameKor, string spriteName, bool upper, bool middleUpper, bool middle, bool middleDown, bool backSide)
    {
        this.cid = cid;
        this.name = name;
        this.nameKor = nameKor;
        this.spriteName = spriteName;
        this.upper = upper;
        this.upper = upper;
        this.middleUpper = middleUpper;
        this.middle = middle;
        this.middleDown = middleDown;
        this.backSide = backSide;
    }

    public override string ToString()
    {
        return string.Format("{0} | {1} | {2} | {3} | {4} | {5} | {6} | {7} | {8}", cid, name, nameKor, spriteName, upper, middleUpper, middle, middleDown, backSide);
    }
}