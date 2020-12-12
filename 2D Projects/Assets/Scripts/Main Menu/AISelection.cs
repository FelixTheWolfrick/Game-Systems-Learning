using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AISelection : MonoBehaviour
{
    //Variables

    //UI 
    public Button patrollingButton;

    //Scenes
    public string patrollingScene;

    // Start is called before the first frame update
    void Start()
    {
        //Instantiate Buttons
        patrollingButton.onClick.AddListener(PatrollingButtonTask);
    }

    //Swicth to Patrolling AI
    void PatrollingButtonTask()
    {
        SceneManager.LoadScene(patrollingScene);
    }
}
