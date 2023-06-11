using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public abstract class GameDb
{
    private static LocalJsonDb _localJsonDb = new LocalJsonDb();
    private static ResourceDb _resourceDb = new ResourceDb();


    public static StageData LoadStage(int stageId)
    {
        try
        {
            string stagePath = "Stages/Stage_" + stageId;
            return _resourceDb.LoadFromJson<StageData>(stagePath);
        }
        catch (Exception ex)
        {
            return StageData.DefaultStage;
            throw ex;
        }

    }
    public static int GetStageCount()
    {
        try
        {
            string path = "Stages";
            TextAsset[] jsonText = Resources.LoadAll<TextAsset>(path);
            return jsonText.Length;
        }
        catch (Exception ex)
        {
            return 0;
        }
    }

    public static GameData LoadGame()
    {
        string directory = "GameData";

        GameData gameData = _localJsonDb.LoadFromJson<GameData>(directory);
        if (gameData == null)
        {
            gameData = NewGame();
        }
        return gameData;
    }

    public static void SaveGame(GameData data)
    {
        string directory = "GameData";
        _localJsonDb.Save(data, directory);
    }
    public static GameData NewGame()
    {
        List<PlayerStageData> playerStagesData = new List<PlayerStageData>();
        for (int i = 0; i < GetStageCount(); i++)
        {
            PlayerStageData stageData = new PlayerStageData(i);
            playerStagesData.Add(stageData);
        }
        GameData newGameData = new GameData(playerStagesData);
        SaveGame(newGameData);
        return newGameData;
    }
}
