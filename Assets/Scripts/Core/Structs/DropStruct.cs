using System;
using UnityEngine;
using UnityEngine.Assertions;


[Serializable]
public struct DropStruct
{
    public int cid;
    public int prob;

    public DropStruct(string text)
    {
        Debug.Log(text);
        string[] data = text.Split(',');
        Assert.AreEqual(data.Length, 2);
        cid = int.Parse(data[0]);
        prob = int.Parse(data[1]);
    }
}