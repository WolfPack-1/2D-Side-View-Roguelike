using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Configuration;
using UnityEngine;

[RequireComponent(typeof(NPC))]
public class NPCInventory : Inventory
{
    // Todo : 나중에 하기

    NPC npc;
    DropStruct[] drops;
    GameManager gameManager;

    public override void Awake()
    {
        base.Awake();
        gameManager = FindObjectOfType<GameManager>();
        npc = GetComponent<NPC>();
        npc.OnNPCInit += InitInventory;
    }

    void InitInventory(NPC npc)
    {
        Debug.Log(npc.NPCStruct.nameKor + " : Initialize Inventory");
        drops = FunctionParser.ParsingDropTable(npc.NPCStruct.dropTable);
        foreach (DropStruct drop in drops)
        {
            Tube tube = gameManager.FindTubeByCid(drop.cid);
            if (tube == null)
            {
                Debug.LogWarning(npc.NPCStruct.nameKor + " : " + drop.cid + "이 드롭 테이블에 있는데 실제 튜브 데이터에서 찾을 수 없어요.");
                continue;
            }
            GetTube(tube);
            Debug.Log(npc.NPCStruct.nameKor + " : Add to Inventory " + tube.NameKor);
        }
    }

    public bool DropRandomTube()
    {
        foreach (Tube tube in Tubes)
        {
            int prob = Array.Find(drops, t => t.cid == tube.Cid).prob;
            int random = UnityEngine.Random.Range(1, 101);
            Debug.Log(npc.NPCStruct.nameKor + " : 튜브 드랍 확률 계산 " + prob + " " + random);
            if (prob >= random)
            {
                Debug.Log(npc.NPCStruct.nameKor + " : 튜브 드랍 " + tube.NameKor);
                return DropTube(tube.Cid);
            }
        }
        return DropTube(-1);
    }
}