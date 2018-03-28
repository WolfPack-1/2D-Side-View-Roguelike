using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(NPCSpawnPoint))]
public class NPCSpawnPointEditor : Editor
{

    List<NPCStruct> data;

    NPCSpawnPoint spawnPoint;
    SerializedObject serObj;
    SerializedProperty npcStruct;
    int selectNPCInt;
    string[] npcCategory;
    

    void OnEnable()
    {
        data = Resources.Load<NPCData>("Data/ScriptableObject/NPC").Load();
        spawnPoint = (NPCSpawnPoint) target;
        serObj = new SerializedObject(target);
        npcStruct = serObj.FindProperty("CurrentNpcStruct");
        npcCategory = data.Select(I => I.nameKor).ToArray();
        spawnPoint.tag = "NPC Spawn Point";
    }

    public override void OnInspectorGUI()
    {               
        serObj.Update();
        
        selectNPCInt = EditorGUILayout.Popup("NPC 선택", selectNPCInt, npcCategory, EditorStyles.popup);

        spawnPoint.CurrentNpcStruct = data[selectNPCInt];
        
        EditorGUILayout.LabelField("ID", spawnPoint.CurrentNpcStruct.cid.ToString());
        EditorGUILayout.LabelField("Name", spawnPoint.CurrentNpcStruct.name);
        EditorGUILayout.LabelField("Name Kor", spawnPoint.CurrentNpcStruct.nameKor);
        EditorGUILayout.LabelField("AnimSet", spawnPoint.CurrentNpcStruct.animSet.ToString());
        EditorGUILayout.LabelField("AttackType", spawnPoint.CurrentNpcStruct.attackType);
        EditorGUILayout.LabelField("AttackValue", spawnPoint.CurrentNpcStruct.attackValue.ToString());
        EditorGUILayout.LabelField("AttackFunction", spawnPoint.CurrentNpcStruct.attackFunction);
        EditorGUILayout.LabelField("AttackDamage", spawnPoint.CurrentNpcStruct.attackDamage.ToString());
        EditorGUILayout.LabelField("CoolTime", spawnPoint.CurrentNpcStruct.coolTime.ToString());
        EditorGUILayout.LabelField("Skill", spawnPoint.CurrentNpcStruct.skill.ToString());
        EditorGUILayout.LabelField("HP", spawnPoint.CurrentNpcStruct.hp.ToString());
        EditorGUILayout.LabelField("Grade", spawnPoint.CurrentNpcStruct.grade.ToString());
        EditorGUILayout.LabelField("Recognize", spawnPoint.CurrentNpcStruct.recognize.ToString());
        EditorGUILayout.LabelField("Recognize Value", spawnPoint.CurrentNpcStruct.recognizeValue.ToString());

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
    
    void OnSceneGUI()
    {
        DrawHandle();
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

    void DrawHandle()
    {
        Handles.DrawWireCube(spawnPoint.transform.position, new Vector2(1f, 2f));
    }
    
}
