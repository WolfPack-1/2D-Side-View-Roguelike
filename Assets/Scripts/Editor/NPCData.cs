using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class NPCData : ScriptableObject
{

    public List<NPCStruct> Data { get; private set; }

    [MenuItem("Assets/Data/NPC")]
    public static void CreateAsset()
    {
        ScriptableObjectExtension.CreateAsset<NPCData>();
    }

    public List<NPCStruct> Load()
    {
        Data = new List<NPCStruct>();
        List<Dictionary<string, object>> csv = CSVParser.Read(Resources.Load<TextAsset>("Data/CSV/NPC"));
        foreach (Dictionary<string, object> line in csv)
        {
            NPCStruct npcStruct = new NPCStruct(
                int.Parse(line["cid"].ToString()),
                line["name"].ToString(),
                line["name_kor"].ToString(),
                line["animset"],
                line["attack_type"].ToString(),
                int.Parse(line["attack_value"].ToString()),
                line["attack_function"].ToString(),
                float.Parse(line["attack_damage"].ToString()),
                float.Parse(line["cooltime"].ToString()),
                line["skill"],
                int.Parse(line["hp"].ToString()),
                line["grade"].ToString(),
                bool.Parse(line["recognize"].ToString()),
                int.Parse(line["recognize_value"].ToString())
            );
            Data.Add(npcStruct);
        }

        return Data;
    }

}

[Serializable]
public struct NPCStruct
{
    public int cid;
    public string name;
    public string nameKor;
    public object animSet;
    public string attackType;
    public int attackValue;
    public string attackFunction;
    public float attackDamage;
    public float coolTime;
    public object skill;
    public int hp;
    public string grade;
    public bool recognize;
    public int recognizeValue;


    public NPCStruct(int cid, string name, string nameKor, object animSet, string attackType, int attackValue, string attackFunction, float attackDamage, float coolTime, object skill, int hp, string grade, bool recognize, int recognizeValue)
    {
        this.cid = cid;
        this.name = name;
        this.nameKor = nameKor;
        this.animSet = animSet;
        this.attackType = attackType;
        this.attackValue = attackValue;
        this.attackFunction = attackFunction;
        this.attackDamage = attackDamage;
        this.coolTime = coolTime;
        this.skill = skill;
        this.hp = hp;
        this.grade = grade;
        this.recognize = recognize;
        this.recognizeValue = recognizeValue;
    }

    public override string ToString()
    {
        return string.Format("{0} | {1} | {2} | {3} | {4} | {5} | {6} | {7} | {8} | {9} | {10} | {11} | {12} | {13}", cid, name, nameKor, animSet, attackType, attackValue, attackFunction, attackDamage, coolTime, skill, hp, grade, recognize, recognizeValue);
    }
}