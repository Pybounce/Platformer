using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.SceneManagement;
using UnityEngine;

public class ResourceJSONDBMS : IReadIDBMS
{

    public static StageData LoadCustomStage(int stageId)
    {
        try
        {
            string stagePath = Application.persistentDataPath + "Stages/Stage_" + stageId;
            string fileContents = File.ReadAllText(stagePath);
            return JsonUtility.FromJson<StageData>(fileContents);
        }
        catch
        {
            return DefaultStage;
        }
    }

    public T Load<T>(string directory)
    {
        TextAsset jsonText = Resources.Load<TextAsset>(directory);
        return JsonUtility.FromJson<T>(jsonText.text);
    }

    public static StageData DefaultStage = new StageData(-1, new PropData[1], 1, Vector3.zero);
}
