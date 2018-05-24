using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Sector))]
public class SectorEditor : Editor
{
    SerializedObject serObj;
    Sector sector;
    
    void OnEnable()
    {
        serObj = new SerializedObject(target);
        sector = (Sector) target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serObj.Update();
        CreateHolders();
        serObj.ApplyModifiedProperties();
    }

    void CreateHolders()
    {
        Transform holdersTransform = sector.transform.Find("Holders");
        if(holdersTransform == null)
        {
            holdersTransform = new GameObject("Holders").transform;
            holdersTransform.SetParent(sector.transform, false);
        }
        Transform spawnPointHolder = holdersTransform.Find("NPC Spawn Point Holder");
        if(spawnPointHolder == null)
        {
            spawnPointHolder = new GameObject("NPC Spawn Point Holder").transform;
            spawnPointHolder.SetParent(holdersTransform, false);
        }
    }
}