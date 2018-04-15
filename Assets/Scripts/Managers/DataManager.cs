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

    [SerializeField] NPCData npcData;
    [SerializeField] AbnormalData abnormalData;
    [SerializeField] GimmickData gimmickData;
    [SerializeField] PlaceDivisionData placeDivisionData;
    [SerializeField] SkillData skillData;
    [SerializeField] LivingEntityData livingEntityData;

    void Awake()
    {
        npcData.Load();
        abnormalData.Load();
        gimmickData.Load();
        placeDivisionData.Load();
        skillData.Load();
        livingEntityData.Load();
    }

}