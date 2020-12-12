using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Variables
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    //Move Player
    void MovePlayer()
    {
        if(Input.GetKey(KeyCode.D)) //Right
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, 0, -90); //Update Sprite
        }
        else if(Input.GetKey(KeyCode.A)) //Left
        {
            transform.position += Vector3.right * -speed * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, 0, 90); //Udate Sprite
        }
        else if (Input.GetKey(KeyCode.W)) //Up
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, 0, 0); //Udate Sprite
        }
        else if (Input.GetKey(KeyCode.S)) //Down
        {
            transform.position += Vector3.up * -speed * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, 0, 180); //Udate Sprite
        }
    }
}
