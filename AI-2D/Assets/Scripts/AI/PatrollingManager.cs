using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatrollingManager : MonoBehaviour
{
    //Variables
    public int patrollingType;
    public bool debugMode;
    public bool shootingMode;
    public GameObject aiObject;

    //UI Variables
    public Button randomPatrolButton;
    public Button preSetPatrolButton;
    public Button randomGeneratedPatrolButton;
    public Button followPatrolButton;
    public Button retreatPatrolButton;

    public Button debugModeButton;
    public Button shootingModeButton;

    // Start is called before the first frame update
    void Start()
    {
        //Instantiate Buttons
        randomPatrolButton.onClick.AddListener(RandomPatrolButtonTask);
        preSetPatrolButton.onClick.AddListener(PreSetPatrolButtonTask);
        randomGeneratedPatrolButton.onClick.AddListener(RandomGeneratedPatrolButtonTask);
        followPatrolButton.onClick.AddListener(FollowPatrolButtonTask);
        retreatPatrolButton.onClick.AddListener(RetreatPatrolButtonTask);

        debugModeButton.onClick.AddListener(DebugModeButtonTask);
        shootingModeButton.onClick.AddListener(ShootingModeButtonTask);
    }

    //Selected Random Patrol
    void RandomPatrolButtonTask()
    {
        patrollingType = 1;
    }

    //Selected Predetermined Patrol
    void PreSetPatrolButtonTask()
    {
        patrollingType = 2;
    }

    //Randomly Generated Patrol
    void RandomGeneratedPatrolButtonTask()
    {
        patrollingType = 3;
    }

    //Follow Patrol
    void FollowPatrolButtonTask()
    {
        patrollingType = 4;
    }

    //Retreat Patrol
    void RetreatPatrolButtonTask()
    {
        patrollingType = 5;
    }

    //Turn On Debug Mode
    void DebugModeButtonTask()
    {
        if(debugMode)
        {
            debugMode = false;
            aiObject.GetComponent<PatrollingAlgorithms>().debugLocationMarker.GetComponent<Renderer>().enabled = false;
        }
        else
        {
            debugMode = true;
            aiObject.GetComponent<PatrollingAlgorithms>().debugLocationMarker.GetComponent<Renderer>().enabled = true;
        }
    }

    //Turn On Shooting Mode
    void ShootingModeButtonTask()
    {
        if(shootingMode)
        {
            shootingMode = false;
        }
        else
        {
            shootingMode = true;
        }
    }
}
