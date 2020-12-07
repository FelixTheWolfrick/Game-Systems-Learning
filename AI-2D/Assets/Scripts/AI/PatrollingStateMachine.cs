using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatrollingStateMachine : MonoBehaviour
{
    //Variables
    public int stateCounter; //Counter is used to changed the example shown to another example
    public GameObject aiObject;

    //UI
    public Button exampleOneButton;

    // Start is called before the first frame update
    void Start()
    {
        //Instantiate Buttons
        exampleOneButton.onClick.AddListener(ExampleOneButtonTask);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Example One - AI wanders to random points and follows player when within range
    public void ExampleOne()
    {
        aiObject.GetComponent<PatrollingAlgorithms>().RandomlyGeneratedPatrol();
        aiObject.GetComponent<PatrollingAlgorithms>().FollowPatrol();
    }

    //Example One Task
    void ExampleOneButtonTask()
    {
        stateCounter = 1;
    }
}
