﻿using UnityEngine;
using System.Text.RegularExpressions;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Reflection;
using System.ComponentModel;
 
public static class CSVParser
{
    const string LINE_SPLIT_RE = @"(((?<x>(?=[,\r\n]+))|""(?<x>([^""]|"""")+)""|(?<x>[^,\r\n]+)),?)";

    public static List<T> LoadObjects<T>(string fileName, bool strict = false) where T : new()
    {
        using (FileStream stream = File.Open("Assets/Resources/Data/CSV/"+fileName, FileMode.Open))
        {
            using (TextReader reader = new StreamReader(stream))
            {
                return LoadObjects<T>(reader, strict);
            }
        }
    }

    public static List<T> LoadObjects<T>(TextReader reader, bool strict = false) where T : new()
    {
        List<T> list = new List<T>();
        string header = reader.ReadLine();
        Dictionary<string, int> fieldDictionary = ParseHeader(header);
        FieldInfo[] fieldInfos = typeof(T).GetFields();
        bool isValueType = typeof(T).IsValueType;
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            T obj = new T();
            object boxed = obj;
            if (ParseLineToObject(line, fieldDictionary, fieldInfos, boxed, strict))
            {
                if (isValueType)
                {
                    obj = (T) boxed;
                }
                list.Add(obj);
            }
        }
        return list;
    }

    public static void LoadObject<T>(string fileName, ref T targetObject)
    {
        using (FileStream stream = File.Open(fileName, FileMode.Open))
        {
            using (TextReader reader = new StreamReader(stream))
            {
                LoadObject(reader, ref targetObject);
            }
        }
    }

    public static void LoadObject<T>(TextReader reader, ref T targetObject)
    {
        FieldInfo[] fieldInfos = typeof(T).GetFields();
        object nonValueObject = targetObject;
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            if(line.StartsWith("#"))
                continue;

            string[] values = EnumerateCSVLine(line).ToArray();
            if (values.Length >= 2)
            {
                SetField(values[0].Trim(), values[1], fieldInfos, nonValueObject);
            }
            else
            {
                Debug.LogWarning(string.Format("CSVParser : {0}번째 줄을 무시합니다. Fields가 충분하지 않습니다.", line));
            }
        }

        if (typeof(T).IsValueType)
        {
            targetObject = (T) nonValueObject;
        }
    }

    static bool ParseLineToObject(string line, Dictionary<string, int> fieldDictionary, FieldInfo[] fieldInfos,
        object targetObject, bool strict)
    {
        string[] values = EnumerateCSVLine(line).ToArray();
        bool setAny = false;
        foreach (string field in fieldDictionary.Keys)
        {
            int index = fieldDictionary[field];
            if (index < values.Length)
            {
                string value = values[index];
                setAny = SetField(field, value, fieldInfos, targetObject) || setAny;
            }
            else if (strict)
            {
                Debug.LogWarning(string.Format("CSVParser : {0}번째 줄을 파싱하는데 Fields가 충분하지 않습니다.", line));
            }
        }
        return setAny;
    }

    static bool SetField(string fieldName, string value, FieldInfo[] fieldInfos, object targetObject)
    {
        foreach (FieldInfo field in fieldInfos)
        {
            if (string.Compare(fieldName, field.Name, true) != 0) continue;
            object typedVal = field.FieldType == typeof(string) ? value : ParseString(value, field.FieldType);
            field.SetValue(targetObject, typedVal);
            return true;
        }
        return false;
    }

    static object ParseString(string stringValue, Type t)
    {
        if (t == typeof(bool))
        {
            if (stringValue == "0")
                stringValue = "False";
            else if (stringValue == "1")
                stringValue = "True";
        }
        TypeConverter typeConverter = TypeDescriptor.GetConverter(t);
        return string.IsNullOrEmpty(stringValue) ? null : typeConverter.ConvertFromInvariantString(stringValue);
    }

    static Dictionary<string, int> ParseHeader(string header)
    {
        Dictionary<string, int> headers = new Dictionary<string, int>();
        int n = 0;
        foreach (string field in EnumerateCSVLine(header))
        {
            string trimmed = field.Trim();
            if (!trimmed.StartsWith("#"))
            {
                headers[trimmed] = n;
            }
            n++;
        }
        return headers;
    }

    static IEnumerable<string> EnumerateCSVLine(string line)
    {
        foreach (Match m in Regex.Matches(line, LINE_SPLIT_RE, RegexOptions.ExplicitCapture))
        {
            yield return m.Groups[1].Value;
        }
    }
}