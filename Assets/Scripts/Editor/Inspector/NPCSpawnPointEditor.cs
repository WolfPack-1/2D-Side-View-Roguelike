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
        EditorGUILayout.LabelField("Grade", spawnPoint.CurrentNpcStruct.grade);
        EditorGUILayout.LabelField("Recognize", spawnPoint.CurrentNpcStruct.recognize.ToString());
        EditorGUILayout.LabelField("Recognize Value", spawnPoint.CurrentNpcStruct.recognizeValue.ToString());
                
        serObj.targetObject.name = "NPC Spawn Point : " + npcCategory[selectNPCInt];
        serObj.ApplyModifiedProperties();
    }
}
