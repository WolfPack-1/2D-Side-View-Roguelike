using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCSpawnPointHolder : MonoBehaviour
{

    [SerializeField] List<NPCSpawnPoint> spawnPoints;
    public List<NPCSpawnPoint> SpawnPoints
    {
        get { return spawnPoints; }
        set { spawnPoints = value; }
    }

}
