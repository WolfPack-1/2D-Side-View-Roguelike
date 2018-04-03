using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public static class TypeParser
{

    public static T[] Parsing<T>(string text, Func<string[], T> func)
    {
        return Parse(text).Select(func).ToArray();
    }

    static IEnumerable<string[]> Parse(string text)
    {
        const string SPLIT_RE = @"[\(*\)]";
        text = text.Replace("{", "");
        text = text.Replace("}", "");

        return Regex.Split(text, SPLIT_RE).Where(split => split != "" && split != ", ").Select(split => split.Split(','));
    }
    
}