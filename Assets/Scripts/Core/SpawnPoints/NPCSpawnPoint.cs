using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawnPoint : MonoBehaviour
{

    DataManager dataManager;
    [SerializeField] int npcStructIndex;
    public NPCStruct CurrentNpcStruct;
    public Vector2 SpawnPosition
    {
        get
        {
            return transform.position;            
        }
    }

    void Awake()
    {
        dataManager = FindObjectOfType<DataManager>();
    }

    void Start()
    {
        CurrentNpcStruct = dataManager.NPCData.Data[npcStructIndex];
    }

    public void Spawn()
    {
        NPC npcPrefab = Resources.Load<NPC>("Prefabs/NPC");
        NPC npc = Instantiate(npcPrefab, SpawnPosition, Quaternion.identity);
        npc.Init(CurrentNpcStruct);
    }

}