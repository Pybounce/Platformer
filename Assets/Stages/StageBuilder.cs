using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class StageBuilder : MonoBehaviour
{
    private Transform _stageObjectContainer;

    public void Build(StageData stageData)
    {
        CreateNewStageObjectContainer();
        SpawnProps(stageData);
        SpawnBackWall(stageData.Size.x, stageData.Size.y);
    }
    private void CreateNewStageObjectContainer()
    {
        if (_stageObjectContainer != null)
        {
            Destroy(_stageObjectContainer.gameObject);
        }
        _stageObjectContainer = new GameObject("Stage Object Container").transform;
    }
    private void SpawnProps(StageData stageData)
    {
        if (stageData.PropData == null) { return; }
        foreach (PropData currentPropData in stageData.PropData)
        {
            if (currentPropData.Id == 0) continue;
            Vector3 itemPosition = GridHelpers.GridToWorldPos(currentPropData.GridIndex, stageData.Size);
            SpawnProp(currentPropData, itemPosition);
        }
    }

    private void SpawnProp(PropData propData, Vector3 position)
    {
        string propName = PropMapper.IdToName(propData.Id);
        GameObject propPrefab = GameDb.LoadProp(propName);
        GameObject prop = Instantiate(propPrefab, position, Quaternion.Euler(new Vector3(0f, 0f, -(float)propData.Direction)));
        prop.transform.parent = _stageObjectContainer;

        if (propData.MoverInput.Enabled)
        {
            prop.AddComponent<BasicMover>().Initialise(propData.MoverInput);
        }
        if (propData.RotatorInput.Enabled)
        {
            prop.AddComponent<BasicRotator>().Initialise(propData.RotatorInput);
        }
    }
    private void SpawnBackWall(int sizeX, int sizeY)
    {
        GameObject propPrefab = GameDb.LoadProp("BackWall");
        GameObject prop = Instantiate(propPrefab, new Vector3((sizeX / 2f) - 0.5f, (sizeY / 2f) + 0.5f, 1f), Quaternion.identity);
        prop.transform.localScale = new Vector3(sizeX, sizeY, 1f);
        prop.transform.parent = _stageObjectContainer;
    }
}


