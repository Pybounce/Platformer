using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// StageSelectManager will be DDOL, and carry over the info of the selected stage to the StageScene. It will then be picked up from the StageController, and destroyed.
/// </summary>
public class StageSelectManager : MonoBehaviour
{
    public static StageSelectManager instance;

    public int SelectedStage { get; private set; }

    private void Start()
    {
        if (instance == null) { instance = this; }
        else { Destroy(gameObject); }
        DontDestroyOnLoad(gameObject);
    }

    public void LoadStage(int stageIndex)
    {
        SelectedStage = stageIndex;
        FindObjectOfType<GameManager>().LoadStageScene();
    }
}
