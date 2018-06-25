using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    const int tubeCapacity = 100;
    const int skillCapacity = 30;
    [SerializeField] List<Tube> tubes;
    [SerializeField] List<Skill> skills;
    public List<Tube> Tubes { get { return tubes; } }
    public int TubeCapacity { get { return tubeCapacity; } }
    public bool IsFull { get { return tubes.Count <= tubeCapacity; } }

    public delegate void OnInventoryTubeDelegate(Tube tube, bool success);
    public delegate void OnInventorySkillDelegate(Skill skill, bool success);
    
    public event OnInventoryTubeDelegate OnGetTube;
    public event OnInventoryTubeDelegate OnDropTube;
    public event OnInventoryTubeDelegate OnDeleteTube;

    public event OnInventorySkillDelegate OnGetSkill;
    public event OnInventorySkillDelegate OnDeleteSkill;
    public event OnInventorySkillDelegate OnCreateSkill;

    public virtual void Awake()
    {
        OnGetTube = delegate { };
        OnDropTube = delegate { };
        OnDeleteTube = delegate { };
        OnGetSkill = delegate { };
        OnDeleteSkill = delegate { };
        OnCreateSkill = delegate { };

        tubes = new List<Tube>();
        skills = new List<Skill>();
    }

    public virtual bool GetSkill(Skill skill)
    {
        if (skills.Count > skillCapacity)
        {
            // 인벤토리가 꽉참
            if (OnGetSkill != null)
                OnGetSkill(null, false);
            return false;
        }

        if (skill == null)
        {
            // 스킬이 예기치 못한 에러로 null
            if (OnGetSkill != null)
                OnGetSkill(null, false);
            return false;
        }
        
        skills.Add(skill);
        if (OnGetSkill != null)
            OnGetSkill(skill, true);
        return true;
    }

    public virtual bool DeleteSkill(Skill skill)
    {
        if (skill == null)
        {
            // 스킬이 예기치 못한 에러로 null
            if (OnDeleteSkill != null)
                OnDeleteSkill(null, false);
            return false;
        }
        
        skills.Remove(skill);
        if (OnDeleteSkill != null)
            OnDeleteSkill(skill, true);
        return true;
    }

    public virtual Tube FindTube(int cid)
    {
        return tubes.Find(t => t.Cid == cid);
    }
    
    public virtual bool GetTube(Tube tube)
    {
        if (tubes.Count > tubeCapacity)
        {
            // 인벤토리가 꽉참
            if (OnGetTube != null)
                OnGetTube(null, false);
            return false;
        }

        if (tube == null)
        {
            if (OnGetTube != null)
                OnGetTube(null, false);
            return false;
        }

        tubes.Add(tube);
        if (OnGetTube != null)
            OnGetTube(tube, true);
        return true;
    }

    public virtual bool DropTube(int cid)
    {
        Tube tube = tubes.Find(t => t.Cid == cid);
        if (!tubes.Contains(tube))
        {
            // cid가 인벤토리에 없음
            if (OnDropTube != null)
                OnDropTube(null, false);
            return false;
        }
        
        Debug.Log("Drop : " + tube.NameKor);
        // Todo : 스킬 튜브로 드랍

        tubes.Remove(tube);
        if (OnDropTube != null)
            OnDropTube(tube, true);
        return true;
    }

    public virtual bool DeleteTube(Tube tube)
    {
        if (tube == null)
            return false;
        return DeleteTube(tube.Cid);
    }

    public virtual bool DeleteTube(int cid)
    {
        Tube tube = tubes.Find(t => t.Cid == cid);
        if (!tubes.Contains(tube))
        {
            // cid가 인벤토리에 없음
            if (OnDeleteTube != null)
                OnDeleteTube(null, false);
            return false;
        }
        
        Debug.Log("Delete : " + tube.NameKor);
        tubes.Remove(tube);
        if (OnDeleteTube != null)
            OnDeleteTube(tube, true);
        return true;
    }

    public virtual bool CreateSkill(int styleCid, int enhancerCid, int coolerCid, int relicCid = -1)
    {
        Tube styleTube = FindTube(styleCid);
        Tube enhancerTube = FindTube(enhancerCid);
        Tube coolerTube = FindTube(coolerCid);
        Tube relicTube = FindTube(relicCid);

        if (styleTube == null || enhancerTube == null || coolerTube == null)
        {
            if (OnCreateSkill != null)
                OnCreateSkill(null, false);
            return false;
        }

        TubeStyleStruct styleStruct = (TubeStyleStruct) styleTube.TubeData;
        TubeEnhancerStruct enhancerStruct = (TubeEnhancerStruct) enhancerTube.TubeData;
        TubeCoolerStruct coolerStruct = (TubeCoolerStruct) coolerTube.TubeData;
        TubeRelicStruct relicStruct = default(TubeRelicStruct);
        if(relicTube != null)
            relicStruct = (TubeRelicStruct) relicTube.TubeData;

        Skill skill = relicTube == null ? new Skill(styleStruct, enhancerStruct, coolerStruct) : new Skill(styleStruct, enhancerStruct, coolerStruct, relicStruct);
        GetSkill(skill);
        DeleteTube(styleTube);
        DeleteTube(enhancerTube);
        DeleteTube(coolerTube);
        DeleteTube(relicTube);
        if (OnCreateSkill != null)
            OnCreateSkill(skill, true);
        Debug.Log("Create : " + skill.Name);
        return true;
    }
}