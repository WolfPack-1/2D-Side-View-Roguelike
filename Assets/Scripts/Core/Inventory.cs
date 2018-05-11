using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    const int capacity = 30;
    [SerializeField] List<SkillStruct> skillStructs;
    public List<SkillStruct> SkillStructs { get { return skillStructs; } }
    public int Capacity { get { return capacity; } }
    public bool IsFull { get { return skillStructs.Count <= capacity; } }

    public delegate void OnInventoryDelegate(SkillStruct skillStruct, bool success);

    public event OnInventoryDelegate OnGetSkill;
    public event OnInventoryDelegate OnDropSkill;
    public event OnInventoryDelegate OnDeleteSkill;

    public virtual void Awake()
    {
        OnGetSkill = delegate { };
        OnDropSkill = delegate { };
        OnDeleteSkill = delegate { };
    }

    public virtual bool GetSkill(SkillStruct skillStruct)
    {
        if (skillStructs.Count > 30)
        {
            //인벤토리가 꽉 참
            if(OnGetSkill != null)
                OnGetSkill(skillStruct, false);
            return false;
        }
             
        skillStructs.Add(skillStruct);
        if(OnGetSkill != null)
            OnGetSkill(skillStruct, true);
        return true;
    }

    public virtual bool DropSkill(SkillStruct skillStruct)
    {
        if (!skillStructs.Contains(skillStruct))
        {
            if(OnDropSkill != null)
                OnDropSkill(skillStruct, false);
            return false;
        }
        
        //Todo : 스킬 큐브로 드랍
        Debug.Log("Drop : " + skillStruct.nameKor);
        
        skillStructs.Remove(skillStruct);
        if(OnDropSkill != null)
            OnDropSkill(skillStruct, true);
        return true;
    }

    public virtual bool DeleteSkill(SkillStruct skillStruct)
    {
        if (!skillStructs.Contains(skillStruct))
        {
            if(OnDropSkill != null)
                OnDropSkill(skillStruct, false);
            return false;
        }
        
        skillStructs.Remove(skillStruct);
        if(OnDeleteSkill != null)
            OnDeleteSkill(skillStruct, true);
        return true;
    }

}