using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour
{
    //Variables
    public float speed;
    public GameObject minX, maxX, minY, maxY;
    public GameObject aiObject;

    // Start is called before the first frame update
    void Start()
    {
        //Set Boundries
        minX = GameObject.Find("MinX");
        maxX = GameObject.Find("MaxX");
        minY = GameObject.Find("MinY");
        maxY = GameObject.Find("MaxY");

        //Get AI
        aiObject = GameObject.Find("AI");
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        OutOfBoundsCheck();
    }

    //Move
    void Move()
    {
        transform.position += aiObject.GetComponent<PatrollingAlgorithms>().lastPosition * (speed * Time.deltaTime);
    }

    //Check Colliding With Object
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("DestroyableObject") || other.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Bullet Hit");
            Destroy(gameObject);
        }
    }

    //Check If Out-Of-Bounds
    void OutOfBoundsCheck()
    {
        if(transform.position.x > maxX.transform.position.x)
        {
            Destroy(gameObject);
        }
        else if(transform.position.x < minX.transform.position.x)
        {
            Destroy(gameObject);
        }
        else if (transform.position.y > maxY.transform.position.y)
        {
            Destroy(gameObject);
        }
        else if (transform.position.y < minY.transform.position.y)
        {
            Destroy(gameObject);
        }
    }

}
