using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    DataManager dataManager;
    
    void Awake()
    {
        dataManager = FindObjectOfType<DataManager>();
        foreach (AttackTypeMelee attackTypeMelee in TypeParser.Parsing("{(melee, 2, 1, 0.5), (melee, 3, 2, 0.1)}", (x) => new AttackTypeMelee(x)))
        {
            Debug.Log(attackTypeMelee.Damage);
        }
    }

}