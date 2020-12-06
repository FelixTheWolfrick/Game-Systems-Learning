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

    //Scenes
    public string aiSelectionScene;

    // Start is called before the first frame update
    void Start()
    {
        //Instantiate Buttons
        aiSelectionButton.onClick.AddListener(AISelectionButtonTask);
    }

    //Swicth to AI Selection Scene
    void AISelectionButtonTask()
    {
        SceneManager.LoadScene(aiSelectionScene);
    }
}
