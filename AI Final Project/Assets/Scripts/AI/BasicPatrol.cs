using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 2 Basic Patrol Options: 
1. From a list of nodes/spots, choose them randomly and move there
2. From a list of nodes/spots, go to them in the order they're stored */
public class BasicPatrol : MonoBehaviour
{
    //Variables

    //Movement Locations
    public GameObject[] locations;

    //AI Variables
    public float speed;
    public int patrollingType;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetPatrollingType();
    }

    //Get Patrolling Type
    void GetPatrollingType()
    {
        //Get Patrolling Type
        patrollingType = 0; //Temp Variable

        //Call Correct Function
        switch (patrollingType)
        {
            case 0:
                Debug.Log("Random Selection AI Patrol Selected");
                RandomlySelectedPatrol();
                break;
            case 1:
                Debug.Log("PreDetermined AI Patrol Selected");
                break;
            default:
                Debug.Log("ERROR: NO OPTION CHOSEN");
                break;
        }
    }

    //Randomly Selected Patrol
    void RandomlySelectedPatrol()
    {
        //Get Random Number
        int randomLocation = Random.Range(0, locations.Length);
        Debug.Log(randomLocation);

        //Move AI
        transform.position = Vector3.MoveTowards(transform.position, locations[randomLocation].transform.position, speed * Time.deltaTime);
    }
}
