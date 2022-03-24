using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringFunctions : MonoBehaviour
{
    public static string RepeatString(string s, int n)
    {
        string repeatString = "";
        
        for(int i = 0; i < n; i++)
        {
            repeatString += s;
        }

        return repeatString;
    }

    public static List<string> SplitStringByChar(string s, char c)
    {
        List<string> returnList = new List<string>();
        int lastCharFound = -1;
        for(int i = 0; i < s.Length; i++)
        {
            if(s[i] == c)
            {
                returnList.Add(s.Substring(lastCharFound + 1, i - lastCharFound - 1));
                lastCharFound = i;
            }
        }
        returnList.Add(s.Substring(lastCharFound + 1));
        return returnList;
    }

    public static float GetLengthOfString(string s)
    {
        float length = 0;
        for(int i = 0; i < s.Length; i++)
        {
            if(s[i] > 64 && s[i] < 91)
            {
                length += 1.5f;
            }
            else
            {
                length += 1.0f;
            }
        }
        return length;
    }
}
