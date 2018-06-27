using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(NPCSpawnPoint))]
public class NPCSpawnPointEditor : Editor
{

    List<NPCStruct> data;

    NPCSpawnPoint spawnPoint;
    SerializedObject serObj;
    SerializedProperty npcStructIndex;
    SerializedProperty npcDir;
    int selectNPCInt;
    string[] npcCategory;
    string[] dirCategory = {"왼쪽", "오른쪽"};

    public static GameObject Create()
    {
        GameObject spawnPointGameObject = new GameObject();
        spawnPointGameObject.AddComponent<NPCSpawnPoint>();
        return spawnPointGameObject;
    }

    void OnEnable()
    {
        data = Resources.Load<NPCData>("Data/ScriptableObject/NPC").Load();
        spawnPoint = (NPCSpawnPoint) target;
        serObj = new SerializedObject(target);
        npcStructIndex = serObj.FindProperty("npcStructIndex");
        npcDir = serObj.FindProperty("dir");
        npcCategory = data.Select(I => I.nameKor).ToArray();
        spawnPoint.tag = "NPC Spawn Point";
        selectNPCInt = npcStructIndex.intValue;
    }

    public override void OnInspectorGUI()
    {               
        serObj.Update();

        selectNPCInt = EditorGUILayout.Popup("NPC 선택", selectNPCInt, npcCategory, EditorStyles.popup);
        npcDir.intValue = EditorGUILayout.Popup("방향 선택", npcDir.intValue, dirCategory, EditorStyles.popup);

        npcStructIndex.intValue = selectNPCInt;
        spawnPoint.CurrentNpcStruct = data[selectNPCInt];
        
        foreach (FieldInfo fieldInfo in spawnPoint.CurrentNpcStruct.GetType().GetFields())
        {
            EditorGUILayout.LabelField(fieldInfo.Name, fieldInfo.GetValue(spawnPoint.CurrentNpcStruct).ToString());
        }

        if (Application.isPlaying)
        {
            if (GUILayout.Button("NPC 스폰"))
            {
                spawnPoint.Spawn();
            }   
        }
        
        StickSpawnerToGround();
        SetTransform();
        serObj.ApplyModifiedProperties();
    }
    
    void SetTransform()
    {
        serObj.targetObject.name = "NPC Spawn Point : " + npcCategory[selectNPCInt];
    }

    void StickSpawnerToGround()
    {
        spawnPoint.transform.rotation = Quaternion.identity;

        int layerMask = LayerMask.NameToLayer("Ground");
        RaycastHit2D hit = Physics2D.Raycast(spawnPoint.transform.position, Vector2.down, 10f);
        if (hit.collider && hit.collider.gameObject.layer == layerMask)
        {
            spawnPoint.transform.position = new Vector2(spawnPoint.transform.position.x, hit.point.y + 1.1f);
        }
        
    }

    [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.NotInSelectionHierarchy)]
    static void DrawHandles(NPCSpawnPoint spawnPoint, GizmoType gizmoType)
    {
        Handles.color = Color.yellow;
        Handles.DrawWireCube(spawnPoint.transform.position, new Vector2(1f, 2f));
        GUIStyle guiStyle = GUI.skin.GetStyle("Label");
        guiStyle.alignment = TextAnchor.MiddleCenter;
        Handles.Label(spawnPoint.transform.position, spawnPoint.CurrentNpcStruct.nameKor, guiStyle);
    }
    
}
