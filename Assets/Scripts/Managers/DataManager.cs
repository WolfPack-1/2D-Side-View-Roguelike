using System.Collections.Generic;
using UnityEngine;


public class DataManager : MonoBehaviour
{

    public Dictionary<CSVEnum, List<Dictionary<string, object>>> CSVData { get; private set; }
    public enum CSVEnum {Gimmick, Npc, Place_Division, Skill}

    void Awake()
    {
        CSVData = new Dictionary<CSVEnum, List<Dictionary<string, object>>>();
    }
    
    public void LoadData()
    {
        foreach (string csvFileName in System.Enum.GetNames(typeof(CSVEnum)))
        {
            CSVData.Add((CSVEnum)System.Enum.Parse(typeof(CSVEnum), csvFileName), CSVParser.Read(Resources.Load<TextAsset>("Data/CSV/"+csvFileName)));
        }
    }
    
}