using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public interface IDataManager
{
    public T Load<T>(string directory);
    public void Save<T>(T data, string directory);
}
