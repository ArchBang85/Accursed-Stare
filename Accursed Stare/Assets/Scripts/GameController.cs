using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    private GameObject Background;
    public GameObject gravityWell;
    public GameObject repulsion;
    private gravityWells gW;
    public List<Transform> gWells = new List<Transform>();

    public void pause() 
    {
        Time.timeScale = 0.0f;
    
    }

    public void halfSpeed()
    {
        Time.timeScale = 0.5f;
    }
    public void fullSpeed()
    {
        Time.timeScale = 1.0f;
    }

    public void doubleSpeed()
    {
        Time.timeScale = 2.0f;
    }


	// Use this for initialization
	void Start () {
        gW = new gravityWells();
        Background = GameObject.Find("Background");
        
    }
	
	// Update is called once per frame
	void Update () {

    // General controls
        if (Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel(Application.loadedLevel);
        }


	// check for clicks on the background cube
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0))
        {
            int mouseButton = 0;
             if (Input.GetMouseButtonDown(1))
             {
                 mouseButton = 1;
             }
            
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Background.GetComponent<Collider>().Raycast (ray, out hit, Mathf.Infinity))
            {
                // Click on background. Do action
                doAction(mouseButton, hit.point);
            }

        }




	}

    public void doAction(int actionType, Vector3 pos)
    {
        if(actionType == 0)
        {
            // See if there already is a 


            // Create new gravity well if possible
            if (gW.canInstantiate())
            {
                Instantiate(gravityWell, new Vector3(pos.x, pos.y, 0), Quaternion.identity);
                gW.addWell();
            }

        }
    
        else if (actionType == 1)
        {
            // Create new repulsion field if possible
            if (gW.canInstantiate())
            {
                Instantiate(repulsion, new Vector3(pos.x, pos.y, 0), Quaternion.identity);
            }
        }

    }
}

public class gravityWells : GameController
{
    int allowedGravityWells = 3;

    public void removeWell()
    {
        // Instantiate gravity well on the map if there's space 

        allowedGravityWells++;
        // update GUI
    }
    public void addWell()
    {
        allowedGravityWells--;
    }
    public int value()
    {
        return allowedGravityWells;
    }

    public bool canInstantiate()
    {
        if (allowedGravityWells > 0)
            return true;
        return false;
    }

}