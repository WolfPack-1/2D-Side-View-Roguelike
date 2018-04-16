
using System.Linq;
using UnityEngine;

public static class InputExtensions
{
    public static bool GetKey(params KeyCode[] keyCodes)
    {
        return keyCodes.Any(Input.GetKey);
    }
}