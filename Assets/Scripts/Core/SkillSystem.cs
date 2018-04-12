using System.Collections;
using UnityEngine;

public class SkillSystem : MonoBehaviour
{
    Skill currentSkill;
    
    public virtual void SetSkill(Skill skill)
    {
        currentSkill = skill;
    }
    
    public virtual void UseSkill()
    {
        
    }

    public virtual void StopSkill(bool force = false)
    {
        
    }

    IEnumerator SkillUpdator()
    {
        while (true)
        {
            
            yield return null;
        }
    }
    
}