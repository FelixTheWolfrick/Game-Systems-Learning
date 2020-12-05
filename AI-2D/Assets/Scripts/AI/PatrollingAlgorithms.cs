using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Patrol Options: 
1. From a list of nodes/spots, choose them randomly and move there
2. From a list of nodes/spots, go to them in the order they're stored
3. Go to randomly generated locations, not taken from a list of nodes/spots
4. Follow player/some sort of moving character/object
5. Run away from player/some sort of moving character/object
6. 
Notes: 
- When swapping patrols, it'll take off where it left off
- You can use debug option to see where the ai is moving to
- Can turn on and off the option for the ai to shoot the player/object (bullet goes to its last location, doesn't follow it if it moves)*/
public class PatrollingAlgorithms : MonoBehaviour
{
    //Variables

    //Movement Locations
    public Transform[] locations;
    private int randomLocation;
    private int nodeLocation; //Location in locations array
    public GameObject randomGeneratedLocation;
    public GameObject minX, maxX, minY, maxY; //Game Boundries
    public GameObject player;

    //AI
    public GameObject manager;
    public float speed;
    public float startTime;
    private float waitTime;
    public GameObject bullet;
    public Vector3 lastPosition; //Position of Player When Bullet is Shot
    public float startShootTime;
    private float shootTime;

    //Debug
    public GameObject debugLocationMarker;

    // Start is called before the first frame update
    void Start()
    {
        waitTime = startTime;
        randomLocation = Random.Range(0, locations.Length);
        nodeLocation = 0;
        randomGeneratedLocation.transform.position = new Vector2(Random.Range(minX.transform.position.x, maxX.transform.position.x), Random.Range(minY.transform.position.y, maxY.transform.position.y));
        debugLocationMarker.GetComponent<Renderer>().enabled = false;
        shootTime = startShootTime;
    }

    // Update is called once per frame
    void Update()
    {
        //Get Patrolling
        GetPatrollingType();

        //Check To Shoot
        if(manager.GetComponent<PatrollingManager>().shootingMode)
        {
            AIShoot();
        }
    }

    //AI Shoot
    void AIShoot()
    {
        //Stop From Spamming Bullets
        if (shootTime <= 0)
        {
            lastPosition = new Vector2(player.transform.position.x, player.transform.position.y);
            Instantiate(bullet, transform.position, Quaternion.identity);
            shootTime = startShootTime;
        }
        else
        {
            shootTime -= Time.deltaTime;
        }
    }

    //Get Patrolling Type
    void GetPatrollingType()
    {
        //Call Correct Function
        switch (manager.GetComponent<PatrollingManager>().patrollingType)
        {
            case 1:
                RandomlySelectedPatrol();
                break;
            case 2:
                PreSetPatrol();
                break;
            case 3:
                RandomlyGeneratedPatrol();
                break;
            case 4:
                FollowPatrol();
                break;
            case 5:
                RetreatPatrol();
                break;
            default:
                Debug.Log("NO OPTION CHOSEN");
                break;
        }
    }

    //Randomly Selected Patrol
    void RandomlySelectedPatrol()
    {
        //Move AI & Update Debug Location
        transform.position = Vector2.MoveTowards(transform.position, locations[randomLocation].position, speed * Time.deltaTime);
        debugLocationMarker.transform.position = new Vector2(locations[randomLocation].position.x, locations[randomLocation].position.y);

        //Wait To Move Again
        if (Vector2.Distance(transform.position, locations[randomLocation].position) < 0.0002f)
        {
            //Wait To Move To Next Location
            if (waitTime > 0)
            {
                waitTime -= Time.deltaTime;
            }
            else
            {
                waitTime = startTime;
                randomLocation = Random.Range(0, locations.Length); //Get New Location
            }
        }
    }

    //PreSet Patrol
    void PreSetPatrol()
    {
        //Move AI & Update Debug Location
        transform.position = Vector2.MoveTowards(transform.position, locations[nodeLocation].position, speed * Time.deltaTime);
        debugLocationMarker.transform.position = new Vector2(locations[nodeLocation].position.x, locations[nodeLocation].position.y);

        //Wait To Move Again
        if (Vector2.Distance(transform.position, locations[nodeLocation].position) < 0.0002f)
        {
            //Wait To Move To Next Location
            if (waitTime > 0)
            {
                waitTime -= Time.deltaTime;
            }
            else
            {
                //Reset Time
                waitTime = startTime; 

                //Set Next Location
                if (nodeLocation <= (locations.Length - 2))
                {
                    nodeLocation++;
                }
                else
                {
                    nodeLocation = 0;
                }
            }
        }
    }

    //Randomly Generated Patrol
    void RandomlyGeneratedPatrol()
    {
        //Move AI & Update Debug Location
        transform.position = Vector2.MoveTowards(transform.position, randomGeneratedLocation.transform.position, speed * Time.deltaTime);
        debugLocationMarker.transform.position = new Vector2(randomGeneratedLocation.transform.position.x, randomGeneratedLocation.transform.position.y);

        //Wait To Move Again
        if (Vector2.Distance(transform.position, randomGeneratedLocation.transform.position) < 0.0002f)
        {
            //Wait To Move To Next Location
            if (waitTime > 0)
            {
                waitTime -= Time.deltaTime;
            }
            else
            {
                waitTime = startTime;
                randomGeneratedLocation.transform.position = new Vector2(Random.Range(minX.transform.position.x, maxX.transform.position.x), Random.Range(minY.transform.position.y, maxY.transform.position.y)); //Get New Location
            }
        }
    }

    //Follow Patrol
    void FollowPatrol()
    {
        //Check Distance From Player
        if (Vector2.Distance(transform.position, player.transform.position) > 1.2f)
        {
            //Move AI & Update Debug Location
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            debugLocationMarker.transform.position = new Vector2(player.transform.position.x, player.transform.position.y);
        }
    }

    //Retreat Patrol
    void RetreatPatrol()
    {
        //Check Distance From Player
        if (Vector2.Distance(transform.position, player.transform.position) < 2.5f)
        {
            //Move AI & Update Debug Location
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, -speed * Time.deltaTime);
            debugLocationMarker.transform.position = new Vector2(transform.position.x, transform.position.y);
        }
    }
}
