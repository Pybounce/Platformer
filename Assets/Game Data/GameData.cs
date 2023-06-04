using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public List<PlayerStageData> StagesData;
    public GameData(List<PlayerStageData> stagesData)
    { this.StagesData = stagesData; }
}
[System.Serializable]
public struct PlayerStageData
{
    public int StageId;
    public bool StageComplete;
    public bool DevTimeComplete;
    public bool MirrorModeComplete;
    public PlayerStageData(int stageId)
    {
        this.StageId = stageId;
        this.StageComplete = false;
        this.DevTimeComplete = false;
        this.MirrorModeComplete = false;
    }
}