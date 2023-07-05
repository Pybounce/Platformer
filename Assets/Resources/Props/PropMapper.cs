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
        {3, "CompletionItem"},
        {4, "Spike_Double"},
        {5, "Sawblade"},
        {6, "Sawblade_Block"},
        {7, "Sawblade_02"},

    };
    public static string IdToName(int id) {
        try
        {
            return propMap[id];
        }
        catch { return propMap[0]; }
    }
}
