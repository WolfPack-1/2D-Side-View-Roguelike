using System.Collections.Generic;
using UnityEngine;


public class DataManager : MonoBehaviour
{
    public NPCData NPCData { get { return npcData; } }
    public AbnormalData AbnormalData { get { return abnormalData; } }
    public GimmickData GimmickData { get { return gimmickData; } }
    public PlaceDivisionData PlaceDivisionData { get { return placeDivisionData; } }
    public SkillData SkillData { get { return skillData; } }
    public LivingEntityData LivingEntityData { get { return livingEntityData; } }
    public TubeData TubeData { get { return tubeData; } }

    [SerializeField] NPCData npcData;
    [SerializeField] AbnormalData abnormalData;
    [SerializeField] GimmickData gimmickData;
    [SerializeField] PlaceDivisionData placeDivisionData;
    [SerializeField] SkillData skillData;
    [SerializeField] LivingEntityData livingEntityData;
    [SerializeField] TubeData tubeData;

    void Awake()
    {
        npcData.Load();
        abnormalData.Load();
        gimmickData.Load();
        placeDivisionData.Load();
        livingEntityData.Load();
        tubeData.LoadAll();
    }

}