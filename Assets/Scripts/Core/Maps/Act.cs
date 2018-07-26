using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Act : MonoBehaviour
{

    [SerializeField] List<Sector> actList = new List<Sector>();
    public List<Sector> ActList { get { return actList; } }
    
}