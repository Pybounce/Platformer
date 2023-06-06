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
        _stageObjectContainer = new GameObject("Stage Object Container").transform;
    }

    public void Build(StageData stageData)
    {
        Demolish();

        Vector2Int dataDimentions = ArrayExtensions.Get2DDimentions(stageData.PropData, stageData.PropDataWidth);
        for (int y = 0; y < dataDimentions.y; y++)
        {
            for (int x = 0; x < dataDimentions.x; x++)
            {
                int flatIndex = ArrayExtensions.FlattenIndex(x, y, stageData.PropDataWidth);
                PropData currentPropData = stageData.PropData[flatIndex];
                if (currentPropData.Id == 0) continue;
                Vector3 itemPosition = GridHelpers.GridToWorldPos(x, y, dataDimentions.x, dataDimentions.y);
                SpawnProp(currentPropData, itemPosition);
            }
        }
        SpawnBackWall(dataDimentions.x, dataDimentions.y);
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


