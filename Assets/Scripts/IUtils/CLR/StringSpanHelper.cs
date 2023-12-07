using UnityEngine;
using System;
using System.Collections.Generic;

namespace IUtils.CLR
{
    public static class StringSpanHelper
    {
        public static ReadOnlySpan<char> Slice(this ReadOnlySpan<char> str, Vector2Int v2)
        {
            return str.Slice(v2.x, v2.y);
        }

        public static List<Vector2Int> Split(this ReadOnlySpan<char> strSpan, string splitStr)
        {
            var splitSpan = splitStr.AsSpan();

            var list = new List<Vector2Int>();

            var num = 0;

            while (true)
            {
                var n = strSpan.IndexOf(splitSpan);

                if (n > -1)
                {
                    strSpan = strSpan.Slice(n + splitSpan.Length);

                    list.Add(new Vector2Int(num, n));

                    num += n + splitSpan.Length;
                }
                else
                {
                    break;
                }
            }

            list.Add(new Vector2Int(num, strSpan.Length));

            return list;
        }

        public static List<Vector2Int> Split(this ReadOnlySpan<char> strSpan, char splitChar)
        {
            var list = new List<Vector2Int>();

            var num = 0;

            while (true)
            {
                var n = strSpan.IndexOf(splitChar);

                if (n > -1)
                {
                    strSpan = strSpan.Slice(n + 1);

                    list.Add(new Vector2Int(num, n));

                    num += n + 1;
                }
                else
                {
                    break;
                }
            }

            list.Add(new Vector2Int(num, strSpan.Length));

            return list;
        }
    }
}