using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif



public class TubeData : ScriptableObject
{
    public List<TubeStyleStruct> StyleData { get; private set; }
    public List<TubeCoolerStruct> CoolerData { get; private set; }
    public List<TubeEnhancerStruct> EnhancerData { get; private set; }
    public List<TubeRelicStruct> RelicData { get; private set; }

#if UNITY_EDITOR
    [MenuItem("Assets/Data/Tube")]
    public static void CreateAsset()
    {
        ScriptableObjectExtension.CreateAsset<TubeData>();
    }
#endif

    public void LoadAll()
    {
        StyleData = CSVParser.LoadObjects<TubeStyleStruct>("TubeStyle.csv");
        CoolerData = CSVParser.LoadObjects<TubeCoolerStruct>("TubeCooler.csv");
        EnhancerData = CSVParser.LoadObjects<TubeEnhancerStruct>("TubeEnhancer.csv");
        RelicData = CSVParser.LoadObjects<TubeRelicStruct>("TubeRelic.csv");
    }

    public List<TubeStyleStruct> LoadStyle()
    {
        if (StyleData != null)
            return StyleData;
        StyleData = CSVParser.LoadObjects<TubeStyleStruct>("TubeStyle.csv");
        return StyleData;
    }

    public List<TubeCoolerStruct> LoadCooler()
    {
        if (CoolerData != null)
            return CoolerData;
        CoolerData = CSVParser.LoadObjects<TubeCoolerStruct>("TubeCooler.csv");
        return CoolerData;
    }

    public List<TubeEnhancerStruct> LoadEnhancer()
    {
        if (EnhancerData != null)
            return EnhancerData;
        EnhancerData = CSVParser.LoadObjects<TubeEnhancerStruct>("TubeEnhancer.csv");
        return EnhancerData;
    }

    public List<TubeRelicStruct> LoadRelic()
    {
        if (RelicData != null)
            return RelicData;
        RelicData = CSVParser.LoadObjects<TubeRelicStruct>("TubeRelic.csv");
        return RelicData;
    }
}