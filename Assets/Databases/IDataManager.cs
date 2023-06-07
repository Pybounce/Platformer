using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public interface IDatabase : IReadDb, IWriteDb {}

public interface IReadDb
{
    public T Load<T>(string directory);
}
public interface IWriteDb
{
    public void Save<T>(T data, string directory);
}