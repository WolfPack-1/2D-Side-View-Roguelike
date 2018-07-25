using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class NPCData : ScriptableObject
{

    public List<NPCStruct> NPC { get; private set; }
    public List<NPCSkillStruct> Skill { get; private set; }

#if UNITY_EDITOR
    [MenuItem("Assets/Data/NPC")]
    public static void CreateAsset()
    {
        ScriptableObjectExtension.CreateAsset<NPCData>();
    }
#endif

    public List<NPCStruct> LoadNpc()
    {
        NPC = CSVParser.LoadObjects<NPCStruct>("NPC.csv");
        return NPC;
    }

    public List<NPCSkillStruct> LoadSkill()
    {
        Skill = CSVParser.LoadObjects<NPCSkillStruct>("NpcSkill.csv");
        return Skill;
    }

    public NPCData LoadAll()
    {
        NPC = LoadNpc();
        Skill = LoadSkill();
        return this;
    }

}