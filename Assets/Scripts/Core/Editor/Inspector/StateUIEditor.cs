using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StateUIController))]
public class StateUIEditor : Editor
{
    StateUIController ui;

    void OnEnable()
    {
        ui = (StateUIController) target;
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
