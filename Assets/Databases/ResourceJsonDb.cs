using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.SceneManagement;
using UnityEngine;

public class ResourceJsonDb : IReadDb
{

    public T Load<T>(string directory)
    {
        TextAsset jsonText = Resources.Load<TextAsset>(directory);
        return JsonUtility.FromJson<T>(jsonText.text);
    }

}
