using System.Collections.Generic;
using UnityEngine;


public class DataManager : MonoBehaviour
{
    public NPCData NPCData { get { return npcData; } }
    public NPCTubeData NPCTubeData { get { return npcTubeData; } }
    public AbnormalData AbnormalData { get { return abnormalData; } }
    public GimmickData GimmickData { get { return gimmickData; } }
    public PlaceDivisionData PlaceDivisionData { get { return placeDivisionData; } }
    public TubeData TubeData { get { return tubeData; } }

    [SerializeField] NPCData npcData;
    [SerializeField] NPCTubeData npcTubeData;
    [SerializeField] AbnormalData abnormalData;
    [SerializeField] GimmickData gimmickData;
    [SerializeField] PlaceDivisionData placeDivisionData;
    [SerializeField] TubeData tubeData;

    void Awake()
    {
        npcData.LoadAll();
        npcTubeData.LoadAll();
        abnormalData.Load();
        gimmickData.Load();
        placeDivisionData.Load();
        tubeData.LoadAll();
    }

}