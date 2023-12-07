using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImlReader
{
    public Dictionary<string, string> ReadedDict;

    public ImlReader()
    {
        ReadedDict = new();
    }

    public void Read(string[] raw)
    {
        for (var i = 0; i < raw.Length; i++)
        {
            var str = raw[i];

            var index = str.IndexOf(':');

            if (index == -1)
            {
                index = str.IndexOf('：');
            }

            if (index == -1) continue;

            var key = str.Substring(0, index);

            Debug.Log(key);
            Debug.Log(str.Substring(index + 1));

            ReadedDict[key] = str.Substring(index + 1);
        }
    }
}
