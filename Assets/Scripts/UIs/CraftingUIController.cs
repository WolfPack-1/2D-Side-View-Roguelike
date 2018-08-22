using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using TMPro;
using UnityEngine;

public class CraftingUIController : MonoBehaviour
{
    PlayerInventory inventory;
    TubeSlotUI[] tubeSlots;

    Animator animator;
    bool isOpen, canTransition;
    InventoryUiController inventoryController;

    int activeSlot;
    
    public bool IsOpen { get { return isOpen; } }
    public bool CanTransition { get { return canTransition; } }
    public TubeSlotUI CurrentSlot { get { return tubeSlots[activeSlot]; } }

    void Awake()
    {
        animator = GetComponent<Animator>();
        inventoryController = FindObjectOfType<InventoryUiController>();
        tubeSlots = new[]
        {
            transform.Find("TubeSlots").GetChild(0).GetComponent<TubeSlotUI>(),
            transform.Find("TubeSlots").GetChild(1).GetComponent<TubeSlotUI>(),
            transform.Find("TubeSlots").GetChild(2).GetComponent<TubeSlotUI>(),
            transform.Find("TubeSlots").GetChild(3).GetComponent<TubeSlotUI>()
        };
    }
    
    public void Init(Player player)
    {
        activeSlot = 0;
        canTransition = true;
        inventory = player.Inventory;
        UpdateUI();
    }

    void Update()
    {
        if (!isOpen)
            return;
        
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            activeSlot = Mathf.Clamp(activeSlot-1, 0, 3);
            UpdateUI();
        }
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            activeSlot = Mathf.Clamp(activeSlot+1, 0, 3);
            UpdateUI();
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            CurrentSlot.Left();
        }
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            CurrentSlot.Right();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            CreateSkill();
        }
    }

    void UpdateUI()
    {
        tubeSlots[0].Init(inventory.Tubes.FindAll(t => t.Socket == SocketEnum.STYLE), SocketEnum.STYLE);
        tubeSlots[1].Init(inventory.Tubes.FindAll(t => t.Socket == SocketEnum.ENHANCER), SocketEnum.ENHANCER);
        tubeSlots[2].Init(inventory.Tubes.FindAll(t => t.Socket == SocketEnum.COOLER), SocketEnum.COOLER);        
        tubeSlots[3].Init(inventory.Tubes.FindAll(t => t.Socket == SocketEnum.RELIC), SocketEnum.RELIC);
        foreach (TubeSlotUI slot in tubeSlots)
        {
            slot.UpdateUI();
            slot.Disalbe();
        }
        CurrentSlot.Enable();   
    }

    public void Open()
    {
        if (canTransition && inventoryController.IsOpen && inventoryController.CanTransition)
        {
            if (isOpen)
            {
                inventoryController.EnableCurrentSkill();
                CurrentSlot.Disalbe();
                canTransition = false;
                animator.SetTrigger("DoClose");
            }
            else
            {
                inventoryController.DisalbeCurrentSkill();
                UpdateUI();
                activeSlot = 0;
                canTransition = false;
                animator.SetTrigger("DoOpen");	
            }	
        }
    }

    public void Close()
    {
        if (canTransition && isOpen && inventoryController.IsOpen && inventoryController.CanTransition)
        {
            inventoryController.EnableCurrentSkill();
            CurrentSlot.Disalbe();
            canTransition = false;
            animator.SetTrigger("DoClose");	
        }
    }

    void CreateSkill()
    {
        Tube styleTube = tubeSlots[0].CurrentTube;
        Tube enhancerTube = tubeSlots[1].CurrentTube;
        Tube coolerTube = tubeSlots[2].CurrentTube;

        if (styleTube == null || enhancerTube == null || coolerTube == null)
            return;
        
        inventory.CreateSkill(styleTube.Cid, enhancerTube.Cid, coolerTube.Cid);
        UpdateUI();
        inventoryController.UpdateUI();
    }

    public void FinishAnimation()
    {
        isOpen =! isOpen;
        canTransition = true;
    }
}