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
    int selectNPCInt;
    string[] npcCategory;

    [MenuItem("GameObject/Create Other/NPCSpawnPoint")]
    public static void Create()
    {
        GameObject spawnPointGameObject = new GameObject();
        spawnPointGameObject.AddComponent<NPCSpawnPoint>();
        EditorGUIUtility.PingObject(spawnPointGameObject);
        Selection.activeGameObject = spawnPointGameObject;
        SceneView.lastActiveSceneView.FrameSelected();
    }

    void OnEnable()
    {
        data = Resources.Load<NPCData>("Data/ScriptableObject/NPC").Load();
        spawnPoint = (NPCSpawnPoint) target;
        serObj = new SerializedObject(target);
        npcStructIndex = serObj.FindProperty("npcStructIndex");
        npcCategory = data.Select(I => I.name_kor).ToArray();
        spawnPoint.tag = "NPC Spawn Point";
        selectNPCInt = npcStructIndex.intValue;
    }

    public override void OnInspectorGUI()
    {               
        serObj.Update();
        
        selectNPCInt = EditorGUILayout.Popup("NPC 선택", selectNPCInt, npcCategory, EditorStyles.popup);

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
        GameObject holders = GameObject.Find("Holders");
        if (holders == null)
        {
            holders = new GameObject("Holders");
        }
        Transform holder = GameObject.Find("Holders").transform.Find("NPC Spawn Point Holder");
        if (holder == null)
        {
            holder = new GameObject("NPC Spawn Point Holder").transform;
            holder.transform.SetParent(holders.transform);
        }
        
        spawnPoint.transform.SetParent(holder.transform);
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
        Handles.Label(spawnPoint.transform.position, spawnPoint.CurrentNpcStruct.name_kor, guiStyle);
    }
    
}
