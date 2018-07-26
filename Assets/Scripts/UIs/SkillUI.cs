using UnityEngine;

public class SkillUI : MonoBehaviour
{
    [SerializeField] TubeStyleStruct[] styleStructs;
    [SerializeField] TubeEnhancerStruct enhancerStruct;
    [SerializeField] TubeCoolerStruct coolerStruct;
    [SerializeField] TubeRelicStruct relicStruct;
    
    public TubeStyleStruct[] StyleStructs { get { return styleStructs; } }
    public TubeEnhancerStruct EnhancerStruct { get { return enhancerStruct; } }
    public TubeCoolerStruct CoolerStruct { get { return coolerStruct; } }
    public TubeRelicStruct RelicStruct { get { return relicStruct; } }

    [SerializeField] Tube styleTube;
    [SerializeField] Tube enhancerTube;
    [SerializeField] Tube coolerTube;
    [SerializeField] Tube relicTube;

    bool isInitByTube, isInitBySkill;

    public bool IsInitByTube { get { return isInitByTube; } }
    public bool IsInitBySkill { get { return isInitBySkill; } }

    public int StyleCid
    {
        get
        {
            if (isInitBySkill) return styleStructs[0].cid;
            if (isInitByTube) return styleTube.Cid;
            return -1;
        }
    }

    public int EnhancerCid
    {
        get
        {
            if (isInitBySkill) return enhancerStruct.cid;
            if (isInitByTube) return enhancerTube.Cid;
            return -1;
        }
    }

    public int CoolerCid
    {
        get
        {
            if (isInitBySkill) return coolerStruct.cid;
            if (isInitByTube) return coolerTube.Cid;
            return -1;
        }
    }

    public int RelicCid
    {
        get
        {
            if (isInitBySkill) return relicStruct.cid;
            if (isInitByTube) return relicTube.Cid;
            return -1;
        }
    }


    public void SetSkill(Skill skill)
    {
        if (skill == null)
        {
            Disable();
            return;
        }
        styleStructs = skill.StyleStructs;
        enhancerStruct = skill.EnhancerStruct;
        coolerStruct = skill.CoolerStruct;
        relicStruct = skill.RelicStruct;
        isInitBySkill = true;
        Enable();
    }

    public void SetTube(params Tube[] tubes)
    {
        styleTube = tubes[0];
        enhancerTube = tubes[1];
        coolerTube = tubes[2];
        if (tubes.Length > 4)
            relicTube = tubes[3];
        isInitByTube = true;
        Enable();
    }

    void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

}