using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.SceneManagement;
using UnityEngine;

public class ResourceDb : IReadDb
{

    public T LoadFromJson<T>(string directory)
    {
        TextAsset jsonText = Resources.Load<TextAsset>(directory);
        return JsonUtility.FromJson<T>(jsonText.text);
    }
    public T Load<T>(string directory) where T : UnityEngine.Object
    {
        return Resources.Load<T>(directory);
    }

}
