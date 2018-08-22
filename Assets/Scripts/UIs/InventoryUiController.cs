using System.Collections.Generic;
using UnityEngine;

public class InventoryUiController : MonoBehaviour
{
	PlayerInventory inventory;
	PlayerSkillSlot skillSlot;
	Animator animator;
	bool isOpen;
	bool canTransition;
	CraftingUIController craftringController;

	[SerializeField] Transform SkillHolder;
	[SerializeField] InventoryComponent _referenceComponent;
	
	public bool IsOpen { get { return isOpen; } }
	public bool CanTransition { get { return canTransition; } }

	void Awake()
	{
		animator = GetComponent<Animator>();
		craftringController = FindObjectOfType<CraftingUIController>();
	}
	
	public void Init(Player player)
	{
	    canTransition = true;
		inventory = player.Inventory;
		skillSlot = player.SkillSlot;
	}

	void UpdateUI()
	{
		foreach (Transform holder in SkillHolder)
		{
			holder.gameObject.SetActive(false);
		}

		List<Skill> skills = inventory.Skills;
		for (int i = 0; i < skills.Count; i++)
		{
			InventoryComponent inventoryComponent;
			if (SkillHolder.childCount > i)
			{
				inventoryComponent = SkillHolder.GetChild(i).GetComponent<InventoryComponent>();
			}
			else
			{
				inventoryComponent = Instantiate(_referenceComponent, SkillHolder);
			}
			inventoryComponent.gameObject.SetActive(true);
			inventoryComponent.Init(skills[i]);
			inventoryComponent.UpdateUI();
		}
	}

	public void Open()
	{
		if (canTransition)
		{
			if (isOpen)
			{
				if (!craftringController.IsOpen && craftringController.CanTransition)
				{
					canTransition = false;
					animator.SetTrigger("DoClose");	
				}
			}
			else
			{
				canTransition = false;
				animator.SetTrigger("DoOpen");	
			}	
		}
		UpdateUI();
	}

	public void Close()
	{
		if (canTransition && isOpen && !craftringController.IsOpen && craftringController.CanTransition)
		{
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
