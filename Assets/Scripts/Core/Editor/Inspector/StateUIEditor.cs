using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StateUIController))]
public class StateUIEditor : Editor
{
    StateUIController ui;
    SerializedObject serObj;

    void OnEnable()
    {
        ui = (StateUIController) target;
        serObj = new SerializedObject(ui);
        EditorApplication.update += UpdateUI;
    }

    void OnDisable()
    {
        EditorApplication.update -= UpdateUI;
    }

    void UpdateUI()
    {
        ui.UpdateUI();
    }
}
