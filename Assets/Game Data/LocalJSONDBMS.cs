using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LocalJSONDBMS : IDBMS
{
    #region Singleton
    private static LocalJSONDBMS _instance;
    public static LocalJSONDBMS Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new LocalJSONDBMS();
            }
            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }
    #endregion

    public T Load<T>(string directory)
    {
        string fullPath = Path.GetFullPath(Path.Combine(Application.persistentDataPath, directory));
        CreateDirectory(fullPath);
        if (File.Exists(fullPath) == false)
        {
            return default;
        }
        string jsonData = File.ReadAllText(fullPath);
        return JsonUtility.FromJson<T>(jsonData);
    }

    public void Save<T>(T data, string directory)
    {
        string fullPath = Path.GetFullPath(Path.Combine(Application.persistentDataPath, directory));
        CreateDirectory(fullPath);
        string jsonData = JsonUtility.ToJson(data, true);
        System.IO.File.WriteAllText(fullPath, jsonData);
    }

    private void CreateDirectory(string fullPath)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
    }
}
