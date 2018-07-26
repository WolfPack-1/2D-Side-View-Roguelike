using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawnPoint : MonoBehaviour
{

    DataManager dataManager;
    [SerializeField] int npcStructIndex;
    [SerializeField] int dir;
    [SerializeField] bool spawnOnAwake;
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
        CurrentNpcStruct = dataManager.NPCData.NPC[npcStructIndex];
        if(spawnOnAwake)
            Spawn();
    }

    public void Spawn()
    {
        NPC npcPrefab = Resources.Load<NPC>("Prefabs/" + CurrentNpcStruct.name);
        if (npcPrefab == null)
        {
            Debug.LogError("스폰할 NPC Prefab을 찾을 수 없습니다");
            return;
        }
        NPC npc = Instantiate(npcPrefab, SpawnPosition, Quaternion.identity);
        npc.Init(CurrentNpcStruct);
        
        Vector3 scale = npc.transform.localScale;
        float scaleX = Mathf.Abs(scale.x);
        scale.x = dir == 1 ? -scaleX : scaleX;
        npc.transform.localScale = scale;
    }
}