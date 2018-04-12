using System.Collections.Generic;
using UnityEngine;

public static class ActExtension
{
    public static void Generate(this Act act)
    {
        Debug.Assert(act.ActList != null);
        
    }
}
