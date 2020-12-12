using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuSelection : MonoBehaviour
{
    //Variables

    //UI
    public Button aiSelectionButton;
    public Button scriptableObjectsSelectionButton;

    //Scenes
    public string aiSelectionScene;
    public string scriptableObjectsSelectionScene;

    // Start is called before the first frame update
    void Start()
    {
        //Instantiate Buttons
        aiSelectionButton.onClick.AddListener(AISelectionButtonTask);
        scriptableObjectsSelectionButton.onClick.AddListener(ScriptableObjectsSelectionButtonTask);
    }

    //Swicth to AI Selection Scene
    void AISelectionButtonTask()
    {
        SceneManager.LoadScene(aiSelectionScene);
    }

    //Switch to Scriptable Objects Scene
    void ScriptableObjectsSelectionButtonTask()
    {
        SceneManager.LoadScene(scriptableObjectsSelectionScene);
    }
}
