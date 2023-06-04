using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PropMapper
{
    private static Dictionary<int, string> propMap = new Dictionary<int, string>()
    {
        {0, ""},
        {1, "Ground"},
        {2, "Spike"},
        {3, "Diamond"},
        {4, "Spike_Double"},
        {5, "Sawblade"},
        {6, "Sawblade_Block"},

    };
    public static string IdToName(int id) {
        try
        {
            return propMap[id];
        }
        catch { return propMap[0]; }
    }
}
