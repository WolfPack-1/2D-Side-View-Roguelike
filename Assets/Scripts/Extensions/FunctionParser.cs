﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public static class FunctionParser
{
    const string SPLIT_RE = @"[\(*\)]";

    public static T ParsingAttackType<T>(string text, Func<string[], T> func)
    {
        foreach (string[] data in Parse(text))
        {
            return func(data);
        }
        Debug.Assert(false);
        return default(T);
    }

    public static string ParsingAttackType(string text)
    {
        foreach (string[] data in Parse(text))
        {
            return data[0];
        }
        return "";
    }

    public static DropStruct[] ParsingDropTable(string text)
    {
        return Parse(text).Select(data => new DropStruct(data)).ToArray();
    }

    static IEnumerable<string[]> Parse(string text)
    {
        text = text.Replace("{", "");
        text = text.Replace("}", "");

        return Regex.Split(text, SPLIT_RE).Where(split => split != "" && split != ", ").Select(split => split.Split(','));
    }
    
}