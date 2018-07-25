using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using JetBrains.Annotations;
using UnityEngine;

public static class FunctionParser
{
    const string SPLIT_RE = @"\(\w+,?\w+\)";

    public static DropStruct[] ParsingDropTable(string text)
    {
        return Parse(text).Select(data => new DropStruct(data)).ToArray();
    }

    public static Dictionary<string, Skill> ParsingSkillTable(string text, DataManager dataManager)
    {
        return Parse(text).ToDictionary(name => name, name => new Skill(name, dataManager));
    }

    static IEnumerable<string> Parse(string text)
    {
        MatchCollection matches = Regex.Matches(text, SPLIT_RE);
        string[] result = new string[matches.Count];
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = matches[i].ToString();
            result[i] = result[i].Replace("(", "").Replace(")", "");
        }
        return result;
    }
    
}