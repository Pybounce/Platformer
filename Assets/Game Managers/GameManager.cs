using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public static GameManager instance;

    private void Start()
    {
        if (instance == null) { instance = this; }
        else { Destroy(gameObject); }
        DontDestroyOnLoad(gameObject);
    }

    public void LoadStageSelectScene()
    {
        SceneManager.LoadScene("StageSelectScene");
        Cursor.visible = true;
    }

    public void LoadStageScene()
    {
        SceneManager.LoadScene("SampleScene");
        Cursor.visible = false;
    }
}



//make me a singleton