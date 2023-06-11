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
    public static StageSelectManager Instance;

    private void Start()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }

    public void LoadStage(int stageIndex)
    {
        GameManager.Instance.LoadStageScene(stageIndex);
    }
}
