using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StageBuilder : MonoBehaviour
{
    private Transform _stageObjectContainer;

    private void Awake()
    {
        _stageObjectContainer = new GameObject("Stage Object Container").transform;
    }
    public void Demolish()
    {
        Destroy(_stageObjectContainer.gameObject);
        _stageObjectContainer = new GameObject("Stage Object Container").transform;
    }

    public void Build(PropData[,] propData)
    {
        Demolish();
        int sizeX = propData.GetLength(1);
        int sizeY = propData.GetLength(0);   
        for (int y = 0; y < sizeY; y++)
        {
            for (int x = 0; x < sizeX; x++)
            {
                PropData currentPropData = propData[y, x];
                if (currentPropData.Id == 0) continue;
                Vector3 itemPosition = GridHelpers.GridToWorldPos(x, y, sizeX , sizeY);
                SpawnProp(currentPropData, itemPosition);
            }
        }
        SpawnBackWall(sizeX, sizeY);
    }


    private void SpawnProp(PropData propData, Vector3 position)
    {
        string propPath = "Props/" + PropMapper.IdToName(propData.Id);
        GameObject propPrefab = Resources.Load<GameObject>(propPath);
        GameObject prop = Instantiate(propPrefab, position, Quaternion.Euler(new Vector3(0f, 0f, -(float)propData.Rotation)));
        prop.transform.parent = _stageObjectContainer;
    }
    private void SpawnBackWall(int sizeX, int sizeY)
    {
        GameObject propPrefab = Resources.Load<GameObject>("Props/BackWall");
        GameObject prop = Instantiate(propPrefab, new Vector3((sizeX / 2f) - 0.5f, (sizeY / 2f) + 0.5f, 1f), Quaternion.identity);
        prop.transform.localScale = new Vector3(sizeX, sizeY, 1f);
        prop.transform.parent = _stageObjectContainer;
    }
}


