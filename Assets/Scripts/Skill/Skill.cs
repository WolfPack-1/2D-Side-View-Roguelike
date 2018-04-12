using UnityEngine;

public class Skill : MonoBehaviour
{
    
    [SerializeField] SkillStruct skillStruct;

    public virtual void Init(SkillStruct data)
    {
        skillStruct = data;
    }
    
    public virtual void Use()
    {
    
    }

    public virtual void Stop()
    {
        
    }
    
    
}