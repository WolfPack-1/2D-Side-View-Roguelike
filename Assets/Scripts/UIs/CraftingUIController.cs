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
        if(Input.GetKeyDown(KeyCode.LeftAlt))
        {
            CurrentSlot.Left();
        }
        if(Input.GetKeyDown(KeyCode.RightAlt))
        {
            CurrentSlot.Right();
        }
    }

    void UpdateUI()
    {
        foreach (TubeSlotUI slot in tubeSlots)
        {
            slot.UpdateUI();
            slot.Disalbe();
        }
        tubeSlots[0].Init(inventory.Tubes.FindAll(t => t.Socket == SocketEnum.STYLE), SocketEnum.STYLE);
        tubeSlots[1].Init(inventory.Tubes.FindAll(t => t.Socket == SocketEnum.ENHANCER), SocketEnum.ENHANCER);
        tubeSlots[2].Init(inventory.Tubes.FindAll(t => t.Socket == SocketEnum.COOLER), SocketEnum.COOLER);        
        tubeSlots[3].Init(inventory.Tubes.FindAll(t => t.Socket == SocketEnum.RELIC), SocketEnum.RELIC);
        CurrentSlot.Enable();   
    }

    public void Open()
    {
        if (canTransition && inventoryController.IsOpen && inventoryController.CanTransition)
        {
            if (isOpen)
            {
                CurrentSlot.Disalbe();
                canTransition = false;
                animator.SetTrigger("DoClose");
            }
            else
            {
                activeSlot = 0;
                canTransition = false;
                animator.SetTrigger("DoOpen");	
            }	
        }
        UpdateUI();
    }

    public void Close()
    {
        if (canTransition && isOpen && inventoryController.IsOpen && inventoryController.CanTransition)
        {
            CurrentSlot.Disalbe();
            canTransition = false;
            animator.SetTrigger("DoClose");	
        }
    }

    public void FinishAnimation()
    {
        isOpen =! isOpen;
        canTransition = true;
    }
}