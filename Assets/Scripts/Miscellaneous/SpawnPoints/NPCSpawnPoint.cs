using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawnPoint : MonoBehaviour
{

    public NPCStruct CurrentNpcStruct { get; set; }
    public Vector2 SpawnPosition
    {
        get
        {
            return transform.position;            
        }
    }

    public void Spawn()
    {
        NPC npcPrefab = Resources.Load<NPC>("Prefabs/NPC");
        NPC npc = Instantiate(npcPrefab, SpawnPosition, Quaternion.identity);
        npc.Init(CurrentNpcStruct);
    }

}