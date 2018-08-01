using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryUiController : MonoBehaviour
{
	PlayerInventory inventory;
	PlayerSkillSlot skillSlot;

	[SerializeField] Transform skillSlotHolder;
	[SerializeField] Transform skillInventoryHolder;
	[SerializeField] TextMeshProUGUI skillDiscription;
	
	public void Init(Player player)
	{
		inventory = player.Inventory;
		skillSlot = player.SkillSlot;
	}

	void UpdateUI()
	{
		skillDiscription.text = "";
		foreach (Transform skillHolder in skillInventoryHolder)
		{
			skillHolder.GetComponent<SkillUI>().Disable();
		}
		
		for (int i = 0; i < 4; i++)
		{
			if (skillSlot.IsSlotEmpty((PlayerSkillSlot.PlayerSkillKeySlotEnum) i))
			{
				skillSlotHolder.GetChild(i).GetComponent<SkillUI>().Disable();
				continue;
			}
			
			skillSlotHolder.GetChild(i).GetComponent<SkillUI>().SetSkill(skillSlot.GetSkill((PlayerSkillSlot.PlayerSkillKeySlotEnum)i));
		}
		
		for (int i = 0; i < inventory.Skills.Count; i++)
		{
			skillInventoryHolder.GetChild(i).GetComponent<SkillUI>().SetSkill(inventory.Skills[i]);
			skillDiscription.text += inventory.Skills[i].StyleStructs[0].name + "\n";
		}
	}

	public void Open()
	{
		if (gameObject.activeSelf)
		{
			Close();
			return;
		}

		gameObject.SetActive(true);
		UpdateUI();
	}

	public void Close()
	{
		gameObject.SetActive(false);
	}

	public void SlotButton(int slot)
	{
		skillSlot.DeleteSlot((PlayerSkillSlot.PlayerSkillKeySlotEnum)slot);
		UpdateUI();
	}

	public void InventorySkillButton()
	{
		SkillUI skill = EventSystem.current.currentSelectedGameObject.GetComponent<SkillUI>();
		if (skillSlot.IsSlotEmpty(PlayerSkillSlot.PlayerSkillKeySlotEnum.Q))
		{
			skillSlot.SetSlot(PlayerSkillSlot.PlayerSkillKeySlotEnum.Q, inventory.GetSkill(skill.StyleStructs, skill.EnhancerStruct, skill.CoolerStruct, true));
			UpdateUI();
			return;
		}

		if (skillSlot.IsSlotEmpty(PlayerSkillSlot.PlayerSkillKeySlotEnum.W))
		{
			skillSlot.SetSlot(PlayerSkillSlot.PlayerSkillKeySlotEnum.W, inventory.GetSkill(skill.StyleStructs, skill.EnhancerStruct, skill.CoolerStruct, true));
			UpdateUI();
			return;
		}

		if (skillSlot.IsSlotEmpty(PlayerSkillSlot.PlayerSkillKeySlotEnum.E))
		{
			skillSlot.SetSlot(PlayerSkillSlot.PlayerSkillKeySlotEnum.E, inventory.GetSkill(skill.StyleStructs, skill.EnhancerStruct, skill.CoolerStruct, true));
			UpdateUI();
			return;
		}
	}
}
