using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public interface IDBMS : IReadIDBMS, IWriteIDBMS {}

public interface IReadIDBMS
{
    public T Load<T>(string directory);
}
public interface IWriteIDBMS
{
    public void Save<T>(T data, string directory);
}