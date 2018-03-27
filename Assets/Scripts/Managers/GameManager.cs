using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    DataManager dataManager;
    
    void Awake()
    {
        dataManager = FindObjectOfType<DataManager>();
        
        dataManager.LoadData();
    }

}
