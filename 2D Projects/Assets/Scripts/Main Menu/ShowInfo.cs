using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInfo : MonoBehaviour
{
    //Variables
    public GameObject infoUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Check if Hovering Option
    void OnMouseOver()
    {
        Debug.Log("Mouse On");
        infoUI.GetComponent<Renderer>().enabled = true;
    }

    void OnMouseExit()
    {
        Debug.Log("Mouse Off");
        infoUI.GetComponent<Renderer>().enabled = false;
    }
}
