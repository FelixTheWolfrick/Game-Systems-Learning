using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Patrol Options: 
1. From a list of nodes/spots, choose them randomly and move there
2. From a list of nodes/spots, go to them in the order they're stored
3. Go to randomly generated locations, not taken from a list of nodes/spots
4. Follow player/some sort of moving character/object
5. Run away from player/some sort of moving character/object

Patrol Add-Ons (Extra pieces that can be added onto the prior patrol aglorithms)
6. Spin in place while looking for a character to walk into vision, can't see past walls
7. When ai reaches certain points it'll jump onto or off of the platform
 
Extra Addition: State Machine Scenario, taking different patrols and merging them into an example of a state machine 

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
    public Transform[] jumpLocations;
    private int jumpNodeLocation; //Location in jumpLocations array

    //AI
    public GameObject manager;
    public GameObject stateMachine;

    public float speed;
    public float jumpHeight;

    public float startTime;
    private float waitTime;

    public GameObject bullet;
    public Vector3 lastPosition; //Position of Player When Bullet is Shot
    public float startShootTime;
    private float shootTime;

    public Vector2 stealthLocation; //Position of Enemy While in Stealth Patrol
    private bool centerAI;
    private bool rotateAI;
    public float rotationSpeed;
    public float distance;
    public GameObject lineOfSightObject; 
    public LineRenderer lineOfSight; //View of AI in Stealth Mode
    public Gradient redLine; //Color of lineOfSight When Hitting Something
    public Gradient greenLine; //Color of lineOfSight When Not Hitting Anything

    //Debug
    public GameObject debugLocationMarker;

    // Start is called before the first frame update
    void Start()
    {
        //Set Wait Time Between Moving to Next Location
        waitTime = startTime;

        //Set Random Location
        randomLocation = Random.Range(0, locations.Length);
        randomGeneratedLocation.transform.position = new Vector2(Random.Range(minX.transform.position.x, maxX.transform.position.x), Random.Range(minY.transform.position.y, maxY.transform.position.y));
        
        //Start at Beginning of locations & jumpLocations Array
        nodeLocation = 0;
        jumpNodeLocation = 0;

        //Set Wait Time Between Shooting Bullets
        shootTime = startShootTime;

        //Stealth Example Variables
        centerAI = true; //Move AI to Center of Screen
        rotateAI = true; //Rotate in Place

        //Ignore Collider on AI Itself
        Physics2D.queriesStartInColliders = false;

        //Hide Line of Sight on Start Up
        lineOfSightObject.SetActive(false);

        //Automatically Turn Off Debug Mode at Start
        debugLocationMarker.GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Get Patrolling Type
        GetPatrollingType();

        //Get Example Type
        GetExampleType();

        //Check to Shoot
        if(manager.GetComponent<PatrollingManager>().shootingMode && manager.GetComponent<PatrollingManager>().patrollingType != 6)
        {
            AIShoot();
        }

        //Check to Turn On and Off lineOfSight
        LineOfSightManager();
    }

    //AI Shoot
    void AIShoot()
    {
        //Check if Stealth Mode is On
        if (manager.GetComponent<PatrollingManager>().patrollingType == 6)
        {
            lastPosition = new Vector2(player.transform.position.x, player.transform.position.y);
            Instantiate(bullet, transform.position, Quaternion.identity);
        }
        else
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
    }

    //Turn On and Off lineOfSight
    void LineOfSightManager()
    {
        if(manager.GetComponent<PatrollingManager>().patrollingType == 6)
        {
            lineOfSightObject.SetActive(true);
        }
        else
        {
            lineOfSightObject.SetActive(false);
        }
    }

    //Get Patrolling Type
    void GetPatrollingType()
    {
        //Call Correct Patrol
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
            case 6:
                StealthPatrolAddOn();
                break;
            case 7:
                JumpingPatrolAddOn();
                break;
            default:
                Debug.Log("NO OPTION CHOSEN");
                break;
        }
    }

    //Get Example Type
    void GetExampleType()
    {
        //Play Example Scenario
        switch(stateMachine.GetComponent<PatrollingStateMachine>().stateCounter)
        {
            case 1:
                stateMachine.GetComponent<PatrollingStateMachine>().ExampleOne();
                break;
            default:
                break;
        }
    }

    //Randomly Selected Patrol
    public void RandomlySelectedPatrol()
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
    public void PreSetPatrol()
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
    public void RandomlyGeneratedPatrol()
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
    public void FollowPatrol()
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
    public void RetreatPatrol()
    {
        //Check Distance From Player
        if (Vector2.Distance(transform.position, player.transform.position) < 2.5f)
        {
            //Move AI & Update Debug Location
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, -speed * Time.deltaTime);
            debugLocationMarker.transform.position = new Vector2(transform.position.x, transform.position.y);
        }
    }

    //Stealth Patron Add-On
    public void StealthPatrolAddOn()
    {
        //Center Character (If Wanted)
        if(centerAI)
        {
            transform.position = new Vector2(0, 0);
        }

        //Spin Character Around (If Wanted)
        if(rotateAI)
        {
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }

        //Variables
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance);

        //Check If Hits Something
        if(hitInfo.collider != null)
        {
            Debug.DrawLine(transform.position, hitInfo.point, Color.red);
            lineOfSight.SetPosition(1, hitInfo.point);
            lineOfSight.colorGradient = redLine;

            //Shoot If Detecting Player
            if(hitInfo.collider.CompareTag("DestroyableObject"))
            {
                AIShoot();
            }
        }
        else
        {
            Debug.DrawLine(transform.position, transform.position + transform.up * distance, Color.green);
            lineOfSight.SetPosition(1, transform.position + transform.up * distance);
            lineOfSight.colorGradient = greenLine;
        }

        lineOfSight.SetPosition(0, transform.position);
    }

    //Jumping Patrol Add-On
    public void JumpingPatrolAddOn()
    {
        //Rotate Sprite to Face Direction it's Moving (Right)
        transform.eulerAngles = new Vector3(0, 0, -90);

        //Update Debug Location Marker
        debugLocationMarker.transform.position = new Vector2(jumpLocations[jumpNodeLocation].position.x, jumpLocations[jumpNodeLocation].position.y);

        //Stop Once Reach End Screen
        if (transform.position.x <= maxX.transform.position.x)
        {
            //Start Moving
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        //Jump If Node Is Close Enough
        if (Vector2.Distance(transform.position, jumpLocations[jumpNodeLocation].transform.position) < 0.5f)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
            
            //Update Node Array Location
            if(jumpNodeLocation != jumpLocations.Length - 1)
            {
                jumpNodeLocation++;
            }
        }
    }
}
