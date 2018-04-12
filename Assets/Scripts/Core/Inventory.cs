using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    int capacity = 30;
    [SerializeField] List<Skill> skills;
    public List<Skill> Skills { get { return skills; } }
    public int Capacity { get { return capacity; } }

    public delegate void OnInventoryDelegate(Skill skill, bool success);

    public event OnInventoryDelegate OnGetSkill;
    public event OnInventoryDelegate OnDropSkill;
    public event OnInventoryDelegate OnDeleteSkill;

    public virtual void Awake()
    {
        OnGetSkill = delegate { };
        OnDropSkill = delegate { };
        OnDeleteSkill = delegate { };
    }

    public virtual void GetSkill(Skill skill)
    {
        if (skills.Count > 30)
        {
            //인벤토리가 꽉 참
            if(OnGetSkill != null)
                OnGetSkill(skill, false);
            return;
        }
             
        skills.Add(skill);
        if(OnGetSkill != null)
            OnGetSkill(skill, true);
    }

    public virtual void DropSkill(Skill skill)
    {
        if (!skills.Contains(skill))
        {
            if(OnDropSkill != null)
                OnDropSkill(skill, false);
            return;
        }
        
        skills.Remove(skill);
        if(OnDropSkill != null)
            OnDropSkill(skill, true);
    }

    public virtual void DeleteSkill(Skill skill)
    {
        if (!skills.Contains(skill))
        {
            if(OnDropSkill != null)
                OnDropSkill(skill, false);
            return;
        }
        
        skills.Remove(skill);
        if(OnDeleteSkill != null)
            OnDeleteSkill(skill, true);
    }

}