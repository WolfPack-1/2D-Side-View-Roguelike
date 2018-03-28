using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{

    [SerializeField] NPCStruct npcStruct;
    
    public void Init(NPCStruct npcStruct)
    {
        this.npcStruct = npcStruct;

        transform.name = npcStruct.nameKor;
    }    

}
