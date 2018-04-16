using System;
using UnityEngine;


[Serializable]
public struct DropStruct
{
    public int cid;
    public int prob;

    public DropStruct(string[] data)
    {
        cid = int.Parse(data[0]);
        prob = int.Parse(data[1]);
    }
}