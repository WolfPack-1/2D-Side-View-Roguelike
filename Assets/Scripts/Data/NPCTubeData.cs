﻿using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif



public class NPCTubeData : ScriptableObject
{
    public List<TubeNPCStyleStruct> StyleData { get; private set; }
    public List<TubeNPCCoolerStruct> CoolerData { get; private set; }
    public List<TubeNPCEnhancerStruct> EnhancerData { get; private set; }

#if UNITY_EDITOR
    [MenuItem("Assets/Data/NPCTube")]
    public static void CreateAsset()
    {
        ScriptableObjectExtension.CreateAsset<NPCTubeData>();
    }
#endif

    public void LoadAll()
    {
        StyleData = LoadStyle();
        CoolerData = LoadCooler();
        EnhancerData = LoadEnhancer();
    }

    public List<TubeNPCStyleStruct> LoadStyle()
    {
        if (StyleData != null)
            return StyleData;
        StyleData = CSVParser.LoadObjects<TubeNPCStyleStruct>("NPCTubeStyle.csv");
        return StyleData;
    }

    public List<TubeNPCCoolerStruct> LoadCooler()
    {
        if (CoolerData != null)
            return CoolerData;
        CoolerData = CSVParser.LoadObjects<TubeNPCCoolerStruct>("NPCTubeCooler.csv");
        return CoolerData;
    }

    public List<TubeNPCEnhancerStruct> LoadEnhancer()
    {
        if (EnhancerData != null)
            return EnhancerData;
        EnhancerData = CSVParser.LoadObjects<TubeNPCEnhancerStruct>("NPCTubeEnhancer.csv");
        return EnhancerData;
    }


    public TubeNPCStyleStruct FindStyleStruct(int cid)
    {
        return StyleData.Find(t => t.cid == cid);
    }
}