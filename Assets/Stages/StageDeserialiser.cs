using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.SceneManagement;
using UnityEngine;

public static class StageDeserialiser
{
    public static StageData LoadStage(int stageId)
    {
        try
        {
            string stagePath = "Stages/Stage_" + stageId;
            TextAsset jsonText = Resources.Load<TextAsset>(stagePath);
            StageJsonData stageJsonData = JsonUtility.FromJson<StageJsonData>(jsonText.text);
            return new StageData(stageJsonData, stageId);
        }
        catch(Exception ex)
        {
            return DefaultStage;
        }

    }
    public static int GetStageCount()
    {
        try
        {
            string path = "Stages";
            TextAsset[] jsonText = Resources.LoadAll<TextAsset>(path);
            return jsonText.Length;
        }
        catch (Exception ex)
        {
            return 0;
        }
    }
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

    public static StageData DefaultStage = new StageData(-1, new PropData[0, 0], Vector3.zero);
}
