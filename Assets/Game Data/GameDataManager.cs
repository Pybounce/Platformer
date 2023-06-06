using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager
{
    private const string _directory = "GameData";
    private static GameDataManager _instance;
    private static GameData _gameData = null;

    public static GameDataManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameDataManager();
            }
            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }
    public GameData LoadGame()
    {
        if (_gameData != null)
        {
            return _gameData;
        }
        GameData gameData = LocalJSONDBMS.Instance.Load<GameData>(_directory);
        if (gameData == null)
        {
            gameData = NewGame();
        }
        _gameData = gameData;
        return _gameData;    
    }
    public void SaveGame(GameData data)
    {
        LocalJSONDBMS.Instance.Save(data, _directory);
        _gameData = data;
    }
    public GameData NewGame()
    {
        List<PlayerStageData> playerStagesData = new List<PlayerStageData>();
        for (int i = 0; i < ResourceJSONDBMS.GetStageCount(); i++)
        {
            PlayerStageData stageData = new PlayerStageData(i);
            playerStagesData.Add(stageData);
        }
        GameData newGameData = new GameData(playerStagesData);
        SaveGame(newGameData);
        return newGameData;
    }
}
