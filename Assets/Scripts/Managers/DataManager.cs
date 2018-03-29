﻿using System.Collections.Generic;
using UnityEngine;


public class DataManager : MonoBehaviour
{
    public NPCData NPCData { get { return npcData; } }
    public GimmickData GimmickData { get { return gimmickData; } }
    public PlaceDivisionData PlaceDivisionData { get { return placeDivisionData; } }
    public SkillData SkillData { get { return skillData; } }

    [SerializeField] NPCData npcData;
    [SerializeField] GimmickData gimmickData;
    [SerializeField] PlaceDivisionData placeDivisionData;
    [SerializeField] SkillData skillData;


}