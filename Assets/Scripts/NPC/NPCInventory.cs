using System.Collections;
using System.Collections.Generic;
using System.Net.Configuration;
using NUnit.Framework.Api;
using UnityEngine;

[RequireComponent(typeof(NPC))]
public class NPCInventory : Inventory
{
    // Todo : 나중에 하기
//    NPC npc;
//    Dictionary<Tube, int> dropDic;
//    DataManager dataManager;
//    
//    public override void Awake()
//    {
//        base.Awake();
//        dataManager = FindObjectOfType<DataManager>();
//        npc = GetComponent<NPC>();
//        dropDic = new Dictionary<Tube, int>();
//        npc.OnNPCInit += InitInventory;
//    }
//
//    void InitInventory(NPC npc)
//    {
//        string rawDropTable = npc.NPCStruct.dropTable;
//        DropStruct[] dropStructs = FunctionParser.ParsingDropTable(rawDropTable);
//        foreach (DropStruct dropStruct in dropStructs)
//        {
//            Tube tube = dataManager.SkillData.GetStructByID(dropStruct.cid);
//            GetTube(tube, dropStruct.prob);
//        }
//    }
//
//    public override bool GetTube(Tube tube)
//    {
//        dropDic.Add(tube, 100);
//        return base.GetTube(tube);
//    }
//
//    public bool GetTube(Tube tube, int prob)
//    {
//        if (dropDic.ContainsKey(tube))
//            return false;
//        dropDic.Add(tube, prob);
//        return base.GetTube(tube);
//    }
//
//    public bool DropRandomTube()
//    {
//        if (dropDic.Count <= 0)
//            return false;
//
//        Tube randomTube = RandomIndex();
//        return DropTube(randomTube);
//    }
//
//    SkillStruct RandomIndex()
//    {
//        int range = 0;
//
//        foreach (int probs in dropDic.Values)
//        {
//            range += probs;
//        }
//
//        int rand = Random.Range(0, range);
//        int top = 0;
//
//        foreach (KeyValuePair<SkillStruct, int> data in dropDic)
//        {
//            top += data.Value;
//            if (rand < top)
//                return data.Key;
//        }
//        Debug.Assert(false);
//        return default(SkillStruct);
//    }
//    
//    public override bool DropSkill(SkillStruct skillStruct)
//    {
//        dropDic.Remove(skillStruct);
//        return base.DropSkill(skillStruct);
//    }
//
//    public override bool DeleteSkill(SkillStruct skillStruct)
//    {
//        dropDic.Remove(skillStruct);
//        return base.DeleteSkill(skillStruct);
//    }
}