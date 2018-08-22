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
	int index;

	[SerializeField] Transform SkillHolder;
	[SerializeField] InventoryComponent referenceComponent;
	List<InventoryComponent> inventoryComponents;

	public bool IsOpen { get { return isOpen; } }
	public bool CanTransition { get { return canTransition; } }

	public InventoryComponent CurrentSkill
	{
		get
		{
			if (inventoryComponents != null && inventoryComponents.Count > index && !craftringController.IsOpen)
				return inventoryComponents[index];
			return null;
		}
	}

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
		index = 0;
		UpdateUI();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			index--;
			UpdateUI();
		}

		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			index++;
			UpdateUI();
		}

		if (Input.GetKeyDown(KeyCode.Q))
		{
			SetSkillSlot(PlayerSkillSlot.PlayerSkillKeySlotEnum.Q);
		}
		
		if (Input.GetKeyDown(KeyCode.W))
		{
			SetSkillSlot(PlayerSkillSlot.PlayerSkillKeySlotEnum.W);
		}
		
		if (Input.GetKeyDown(KeyCode.E))
		{
			SetSkillSlot(PlayerSkillSlot.PlayerSkillKeySlotEnum.E);
		}
		
		if (Input.GetKeyDown(KeyCode.R))
		{
			SetSkillSlot(PlayerSkillSlot.PlayerSkillKeySlotEnum.R);
		}
	}

	public void UpdateUI()
	{
		foreach (Transform holder in SkillHolder)
		{
			holder.gameObject.SetActive(false);
		}

		List<Skill> skills = inventory.Skills;
		inventoryComponents = new List<InventoryComponent>();
		for (int i = 0; i < skills.Count; i++)
		{
			InventoryComponent inventoryComponent;
			if (SkillHolder.childCount > i)
			{
				inventoryComponent = SkillHolder.GetChild(i).GetComponent<InventoryComponent>();
			}
			else
			{
				inventoryComponent = Instantiate(referenceComponent, SkillHolder);
			}

			inventoryComponent.gameObject.SetActive(true);
			inventoryComponent.Init(skills[i]);
			inventoryComponent.UpdateUI();
			inventoryComponent.Disable();
			inventoryComponents.Add(inventoryComponent);
		}

		if (!craftringController.IsOpen && CurrentSkill != null)
		{
			index = Mathf.Clamp(index, 0, inventoryComponents.Count - 1);
			CurrentSkill.Enable();
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
				index = 0;
				UpdateUI();
				canTransition = false;
				animator.SetTrigger("DoOpen");
			}
		}
	}

	public void Close()
	{
		if (canTransition && isOpen && !craftringController.IsOpen && craftringController.CanTransition)
		{
			canTransition = false;
			animator.SetTrigger("DoClose");
		}
	}

	public void EnableCurrentSkill()
	{
		UpdateUI();
		if (inventoryComponents != null && inventoryComponents.Count > index)
			inventoryComponents[index].Enable();
	}

	public void DisalbeCurrentSkill()
	{
		UpdateUI();
		if (CurrentSkill != null)
			CurrentSkill.Disable();
	}

	void SetSkillSlot(PlayerSkillSlot.PlayerSkillKeySlotEnum playerSkillKeySlotEnum)
	{
		if (CurrentSkill != null && !craftringController.IsOpen && isOpen)
		{
			skillSlot.SetSlot(playerSkillKeySlotEnum, CurrentSkill.Skill);
			inventory.DeleteSkill(CurrentSkill.Skill);
			UpdateUI();
		}
	}

	public void FinishAnimation()
	{
		isOpen = !isOpen;
		canTransition = true;
	}
}