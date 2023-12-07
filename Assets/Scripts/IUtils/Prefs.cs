using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class Prefs
{
    public readonly Dictionary<string, int> IntDict = new();
    public readonly Dictionary<string, bool> BoolDict = new();
    public readonly Dictionary<string, float> FloatDict = new();
    public readonly Dictionary<string, string> StringDict = new();

    public int GetInt(string key, int defaultValue)
    {
        return IntDict.ContainsKey(key) ? IntDict[key] : defaultValue;
    }

    public void SetInt(string key, int value)
    {
        IntDict[key] = value;
    }

    public bool GetBool(string key, bool defaultValue)
    {
        return BoolDict.ContainsKey(key) ? BoolDict[key] : defaultValue;
    }

    public void SetBool(string key, bool value)
    {
        BoolDict[key] = value;
    }

    public float GetFloat(string key, float defaultValue)
    {
        return FloatDict.ContainsKey(key) ? FloatDict[key] : defaultValue;
    }

    public void SetFloat(string key, float value)
    {
        FloatDict[key] = value;
    }

    public string GetString(string key, string defaultValue)
    {
        return StringDict.ContainsKey(key) ? StringDict[key] : defaultValue;
    }

    public void SetString(string key, string value)
    {
        StringDict[key] = value;
    }
}
