using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    DataManager dataManager;
    [SerializeField] StateUIController stateController;
    [SerializeField] InventoryUiController inventoryController;
    [SerializeField] CraftingUIController craftingController;
    Player player;

    public DataManager DataManager { get { return dataManager; } }
    public StateUIController StateController { get { return stateController; } }
    public InventoryUiController InventoryController { get { return inventoryController; } }
    public CraftingUIController CraftingController { get { return craftingController; } }
    public Player Player { get { return player; } set { player = value; } }

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

}