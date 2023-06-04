using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelector : MonoBehaviour
{
    public void SelectStage(int stageIndex)
    {
        FindObjectOfType<StageSelectManager>().LoadStage(stageIndex);
    }
}
