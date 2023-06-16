using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
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
        _stageObjectContainer = new GameObject("Stage Object Container").transform; //TODO -> Move this to it's own method, it's called in awake too
    }

    public void Build(StageData stageData)
    {
        Demolish();

        for (int y = 0; y < stageData.Size.y; y++)
        {
            for (int x = 0; x < stageData.Size.x; x++)
            {
                int flatIndex = ArrayExtensions.FlattenIndex(x, y, stageData.Size.x);
                PropData currentPropData = stageData.PropData[flatIndex];
                if (currentPropData.Id == 0) continue;
                Vector3 itemPosition = GridHelpers.GridToWorldPos(x, y, stageData.Size.x, stageData.Size.y);
                SpawnProp(currentPropData, itemPosition);
            }
        }

        SpawnBackWall(stageData.Size.x, stageData.Size.y);
    }


    private void SpawnProp(PropData propData, Vector3 position)
    {
        string propName = PropMapper.IdToName(propData.Id);
        GameObject propPrefab = GameDb.LoadProp(propName);
        GameObject prop = Instantiate(propPrefab, position, Quaternion.Euler(new Vector3(0f, 0f, -(float)propData.Direction)));
        prop.transform.parent = _stageObjectContainer;
    }
    private void SpawnBackWall(int sizeX, int sizeY)
    {
        GameObject propPrefab = GameDb.LoadProp("BackWall");
        GameObject prop = Instantiate(propPrefab, new Vector3((sizeX / 2f) - 0.5f, (sizeY / 2f) + 0.5f, 1f), Quaternion.identity);
        prop.transform.localScale = new Vector3(sizeX, sizeY, 1f);
        prop.transform.parent = _stageObjectContainer;
    }
}


