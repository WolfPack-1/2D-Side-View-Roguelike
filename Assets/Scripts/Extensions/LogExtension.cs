using UnityEngine;

public static class LogExtension
{
    [System.Diagnostics.Conditional("DEBUG"), System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void Log(this Object context, object message)
    {
        Debug.Log(string.Concat("<color=grey>[Log]</color> ", message), context);
    }

    [System.Diagnostics.Conditional("DEBUG"), System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void Log(object message)
    {
        Debug.Log(string.Concat("<color=grey>[Log]</color> ", message));
    }

    [System.Diagnostics.Conditional("DEBUG"), System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void Warning(this Object context, object message)
    {
        Debug.Log(string.Concat("<color=yellow>[Warning]</color> ", message), context);
    }

    [System.Diagnostics.Conditional("DEBUG"), System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void Warning(object message)
    {
        Debug.Log(string.Concat("<color=yellow>[Warning]</color> ", message));
    }
    
    [System.Diagnostics.Conditional("DEBUG"), System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void Error(this Object context, object message)
    {
        Debug.Log(string.Concat("<color=red>[Error]</color> ", message), context);
    }

    [System.Diagnostics.Conditional("DEBUG"), System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void Error(object message)
    {
        Debug.Log(string.Concat("<color=red>[Error]</color> ", message));
    }
}