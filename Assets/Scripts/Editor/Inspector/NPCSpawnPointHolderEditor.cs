using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NPCSpawnPointHolder))]
public class NPCSpawnPointHolderEditor : Editor
{

    SerializedObject serObj;
    SerializedProperty spawnPoints;
    NPCSpawnPointHolder holder;

    void OnEnable()
    {
        serObj = new SerializedObject(target);
        holder = (NPCSpawnPointHolder)target;
        spawnPoints = serObj.FindProperty("spawnPoints");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(GUILayout.Button("NPC 스폰 생성"))
        {
            GameObject spawnPoint = NPCSpawnPointEditor.Create();
            spawnPoint.transform.SetParent(holder.transform, false);
            spawnPoint.transform.localPosition = Vector3.zero;
            //holder.SpawnPoints.Add(spawnPoint.GetComponent<NPCSpawnPoint>());
            // TODO : List 추가 관련 수정을 해야함

            EditorGUIUtility.PingObject(spawnPoint);
            Selection.activeGameObject = spawnPoint;
            SceneView.lastActiveSceneView.FrameSelected();
        }
    }

}