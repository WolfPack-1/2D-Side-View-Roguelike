using UnityEngine;
using UnityEngine.UI;

public class SkillIcon : MonoBehaviour
{
    [SerializeField] Image iconImage;
    Skill skill;

    public void Init(Skill skill)
    {
        this.skill = skill;
    }

    public void UpdateUI()
    {
        // TODO : 현재 스킬에 따라 iconImage 변경
    }

}
