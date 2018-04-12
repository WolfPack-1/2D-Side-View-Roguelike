using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sector : MonoBehaviour
{

    [SerializeField] AvailableSectorStruct availableDirection;
    public AvailableSectorStruct AvailableDirection { get { return availableDirection;} }

}