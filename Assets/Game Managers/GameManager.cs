using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance;

    private StageSceneData _stageSceneData;

    private void Start()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
        DontDestroyOnLoad(gameObject);
    }
    public void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public void LoadStageSelectScene()
    {
        SceneManager.LoadScene("StageSelectScene");
        Cursor.visible = true;
    }

    public void LoadStageScene(int stageIndex)
    {
        _stageSceneData.StageIndex = stageIndex;
        SceneManager.LoadScene("SampleScene");
        
    }
    private void InitialiseStageScene()
    {
        Cursor.visible = false;
        CreateStageManager(_stageSceneData.StageIndex);
    }
    private void CreateStageManager(int stageIndex)
    {
        GameObject stageManagerObj = new GameObject();
        stageManagerObj.AddComponent<StageManager>().Initialise(stageIndex);
    }



    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case SceneNames.StageScene:
                InitialiseStageScene();
                break;

        }
    }

}