using System.Collections;
using System.Collections.Generic;
using System.Net.Configuration;
using NUnit.Framework.Api;
using UnityEngine;

[RequireComponent(typeof(NPC))]
public class NPCInventory : Inventory
{
    NPC npc;
    Dictionary<SkillStruct, int> dropDic;
    DataManager dataManager;
    
    public override void Awake()
    {
        base.Awake();
        dataManager = FindObjectOfType<DataManager>();
        npc = GetComponent<NPC>();
        dropDic = new Dictionary<SkillStruct, int>();
        npc.OnNPCInit += InitInventory;
    }

    void InitInventory(NPC npc)
    {
        string rawDropTable = npc.NPCStruct.dropTable;
        DropStruct[] dropStructs = FunctionParser.ParsingDropTable(rawDropTable);
        foreach (DropStruct dropStruct in dropStructs)
        {
            SkillStruct skill = dataManager.SkillData.GetStructByID(dropStruct.cid);
            GetSkill(skill, dropStruct.prob);
        }
    }

    public override bool GetSkill(SkillStruct skillStruct)
    {
        dropDic.Add(skillStruct, 100);
        return base.GetSkill(skillStruct);
    }

    public bool GetSkill(SkillStruct skillStruct, int prob)
    {
        if (dropDic.ContainsKey(skillStruct))
            return false;
        dropDic.Add(skillStruct, prob);
        return base.GetSkill(skillStruct);
    }

    public bool DropRandomSkill()
    {
        if (dropDic.Count <= 0)
            return false;

        SkillStruct randomSkillStruct = RandomIndex();
        return DropSkill(randomSkillStruct);
    }

    SkillStruct RandomIndex()
    {
        int range = 0;

        foreach (int probs in dropDic.Values)
        {
            range += probs;
        }

        int rand = Random.Range(0, range);
        int top = 0;

        foreach (KeyValuePair<SkillStruct, int> data in dropDic)
        {
            top += data.Value;
            if (rand < top)
                return data.Key;
        }
        Debug.Assert(false);
        return default(SkillStruct);
    }
    
    public override bool DropSkill(SkillStruct skillStruct)
    {
        dropDic.Remove(skillStruct);
        return base.DropSkill(skillStruct);
    }

    public override bool DeleteSkill(SkillStruct skillStruct)
    {
        dropDic.Remove(skillStruct);
        return base.DeleteSkill(skillStruct);
    }
}