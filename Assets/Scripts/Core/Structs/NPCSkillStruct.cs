using System;

[Serializable]
public struct NPCSkillStruct
{
    public int cid;
    public string name;
    public string nameKor;
    public string styleTube;
    public string enhancerTube;
    public string coolerTube;
    public string relicTube;
    
    public NPCSkillStruct(int cid, string name, string nameKor, string styleTube, string enhancerTube, string coolerTube, string relicTube)
    {
        this.cid = cid;
        this.name = name;
        this.nameKor = nameKor;
        this.styleTube = styleTube;
        this.enhancerTube = enhancerTube;
        this.coolerTube = coolerTube;
        this.relicTube = relicTube;
    }    

}
