using System;

[Serializable]
public struct PlaceDivisionStruct
{
    public int id;
    public string place;
    public string name;

    public PlaceDivisionStruct(int id, string place, string name)
    {
        this.id = id;
        this.place = place;
        this.name = name;
    }

    public override string ToString()
    {
        return string.Format("{0} | {1} | {2}", id, place, name);
    }
}