﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    DataManager dataManager;
    [SerializeField] StateUIController stateController;
    [SerializeField] InventoryUiController inventoryController;
    [SerializeField] CraftingUIController craftingController;
    [SerializeField] IconUIController iconUIController;
    Player player;

    public DataManager DataManager { get { return dataManager; } }
    public StateUIController StateController { get { return stateController; } }
    public InventoryUiController InventoryController { get { return inventoryController; } }
    public CraftingUIController CraftingController { get { return craftingController; } }
    public Player Player { get { return player; } set { player = value; } }
    
    float deltaTime = 0.0f;

    void Awake()
    {
        player = FindObjectOfType<Player>();
        dataManager = FindObjectOfType<DataManager>();
    }

    void Start()
    {
        stateController.Init(player);
        inventoryController.Init(player);
        craftingController.Init(player);
    }
    
    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }
    
    void OnGUI()
    {
        int w = Screen.width, h = Screen.height;
 
        GUIStyle style = new GUIStyle();
 
        Rect rect = new Rect(0, 0, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 100;
        style.normal.textColor = Color.yellow;
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("Build 0.03 {0:0.0} ms ({1:0.} fps)", msec, fps);
        GUI.Label(rect, text, style);
    }

    public Tube FindTubeByCid(int cid)
    {
        int index;
        
        index = dataManager.TubeData.StyleData.FindIndex(t => t.cid == cid);
        if (index != -1)
        {
            return new Tube(dataManager.TubeData.StyleData[index]);
        }

        index = dataManager.TubeData.EnhancerData.FindIndex(t => t.cid == cid);
        if (index != -1)
        {
            return new Tube(dataManager.TubeData.EnhancerData[index]);
        }
        
        index = dataManager.TubeData.CoolerData.FindIndex(t => t.cid == cid);
        if (index != -1)
        {
            return new Tube(dataManager.TubeData.CoolerData[index]);
        }
        
        index = dataManager.TubeData.RelicData.FindIndex(t => t.cid == cid);
        if (index != -1)
        {
            return new Tube(dataManager.TubeData.RelicData[index]);
        }
        
        index = dataManager.NPCTubeData.StyleData.FindIndex(t => t.cid == cid);
        if (index != -1)
        {
            return new Tube(dataManager.NPCTubeData.StyleData[index]);
        }

        index = dataManager.NPCTubeData.EnhancerData.FindIndex(t => t.cid == cid);
        if (index != -1)
        {
            return new Tube(dataManager.NPCTubeData.EnhancerData[index]);
        }
        
        index = dataManager.NPCTubeData.CoolerData.FindIndex(t => t.cid == cid);
        if (index != -1)
        {
            return new Tube(dataManager.NPCTubeData.CoolerData[index]);
        }
        
//        index = dataManager.NPCTubeData.RelicData.FindIndex(t => t.cid == cid);
//        if (index != -1)
//        {
//            return new Tube(dataManager.NPCTubeData.RelicData[index]);
//        }

        return null;
    }

    public void SetInteractableIcon(Vector2 position)
    {
        iconUIController.SetIcon(position);
    }

    public void ResetInteractableIcon()
    {
        iconUIController.DisableIcon();
    }
}