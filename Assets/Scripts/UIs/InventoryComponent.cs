using TMPro;
using UnityEngine;

public class InventoryComponent : MonoBehaviour
{
    [SerializeField] SkillIcon skillIcon;
    [SerializeField] GameObject highlight;
    [SerializeField] TextMeshProUGUI skillName;
    [SerializeField] TextMeshProUGUI skillInfo;
    [SerializeField] TextMeshProUGUI skillTraits;

    Skill skill;
    
    public void Init(Skill skill)
    {
        this.skill = skill;
        skillIcon.Init(skill);
    }

    public void UpdateUI()
    {
        if(skill == null)
            return;
        
        skillIcon.UpdateUI();
        skillName.SetText(skill.Name);
        skillInfo.SetText
        (
            "Damage : " + skill.StyleStructs[skill.CurrentSkillIndex].damage +
            "/ CoolTime : \n" + skill.CoolTime +
            "Company : " + skill.StyleStructs[skill.CurrentSkillIndex].Company
        );
        skillTraits.SetText
        (
            ""
        );
    }

    public void Enable()
    {
        highlight.gameObject.SetActive(true);
    }

    public void Disable()
    {
        highlight.gameObject.SetActive(false);
    }
}
