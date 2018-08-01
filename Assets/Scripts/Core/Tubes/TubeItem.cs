using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeItem : MonoBehaviour
{

    ITube tube;

    public TubeItem Init(ITube tube)
    {
        this.tube = tube;
        return this;
    }

    public TubeItem Drop()
    {
        
        return this;
    }

}
