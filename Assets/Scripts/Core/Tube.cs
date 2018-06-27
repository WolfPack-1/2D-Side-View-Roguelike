using System;
using UnityEngine;

[Serializable]
public class Tube
{
    [SerializeField] ITube tubeData;
    
    public ITube TubeData { get { return tubeData; } }
    public int Cid { get { return tubeData.Cid; } }
    public string Name { get { return tubeData.Name; } }
    public string NameKor { get { return tubeData.NameKor; } }
    public SocketEnum Socket { get { return tubeData.Socket; } }
    public TubeGradeEnum Grade { get { return tubeData.Grade; } }
    public string Company { get { return tubeData.Company; } }

    public Tube(ITube tube)
    {
        tubeData = tube;
    }
}