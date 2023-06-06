using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.SceneManagement;
using UnityEngine;

public class ResourceJSONDBMS : IReadIDBMS
{
    #region Singleton
    private static ResourceJSONDBMS _instance;
    public static ResourceJSONDBMS Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ResourceJSONDBMS();
            }
            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }
    #endregion

    public static StageData LoadStage(int stageId)
    {
        try
        {
            string stagePath = "Stages/Stage_" + stageId;
            TextAsset jsonText = Resources.Load<TextAsset>(stagePath);
            return JsonUtility.FromJson<StageData>(jsonText.text);
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

    public T Load<T>(string directory)
    {
        TextAsset jsonText = Resources.Load<TextAsset>(directory);
        return JsonUtility.FromJson<T>(jsonText.text);
    }

    public static StageData DefaultStage = new StageData(-1, new PropData[1], 1, Vector3.zero);
}
