using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {


    // TODO:
    /*
     * Solar system planet destabilisation 
     * Power counter and link to player capacities
     * Make sure creation doesn't interfere with UI
     * End condition
     * Start phasing
     * Sounds
     * 
    */
    private GameObject Background;
    public GameObject gravityWellObject;
    public GameObject repulsion;
    private gravityWells gW;
   
    private mainVariables mainData;
    public List<Transform> gWells = new List<Transform>();
    private bool live = true;

    public AudioSource music;

	// check for clicks on the background cube
     bool one_click = false;
     bool timer_running;
     float timer_for_double_click = 0.0f;
 

    public void soundToggle()
     {
        if(music.isPlaying)
        {
            music.Pause();
        }
        else
        {
            music.Play();
        }

     }

    public IEnumerator CameraTransition(float toSize, float duration)
    {
        float diff = toSize - transform.GetComponent<Camera>().orthographicSize;
        if (diff == 0) diff = 0.01f;
        float originalSize = transform.GetComponent<Camera>().orthographicSize;

        float increment = diff / duration;

        for (float f = 0f; f < Mathf.Abs(diff); f += Time.deltaTime)
        {
            transform.GetComponent<Camera>().orthographicSize = originalSize + f;
            
            yield return null;
        }


    }


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

    public void fastSpeed()
    {
        Time.timeScale = 4.0f;
    }

    // at the start get the game to run at 10x speed for 5 secs
    public IEnumerator FastForward(float duration)
    {

        yield return new WaitForSeconds(0.1f);
       
        Time.timeScale = 25.0f;
       // Debug.Log("time at 10");
        yield return new WaitForSeconds(45.0f);

        Time.timeScale = 1.0f;
      //  Debug.Log("time at 1");

    }


	// Use this for initialization
	void Start () {
        gW = new gravityWells();
        mainData = new mainVariables();
        Background = GameObject.Find("Background");
        StartCoroutine(FastForward(5.0f));
    }


	
	// Update is called once per frame
	void Update () {

        if (Time.time % 1 == 0)
        {
            // update every second
            updateEnergy();
        }

        if(Input.GetKeyDown(KeyCode.Z))
        {
            StartCoroutine(CameraTransition(6, 5.0f));
        }
    // General controls
        if (Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel(Application.loadedLevel);
        }

        if (live)
        {

           // Debug.Log(one_click);
            //this is how long in seconds to allow for a double click
            float delay = 0.4f;
            if (Input.GetMouseButtonDown(0))
            {
                if (!one_click) // first click no previous clicks
                {
                    one_click = true;
                    timer_for_double_click = Time.time; // save the current time
                }
                else
                {
                    // do two click things;
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                    {
                        if (hit.transform.tag == "Background")
                        {
                            // Click on background. Do action
                            doAction(0, hit.point);
                        }
                    }
                    one_click = false; // found a double click, now reset
                }
            }
            if (one_click)
            {
                // if the time now is delay seconds more than when the first click started. 
                if ((Time.time - timer_for_double_click) > delay)
                {

                    //basically if thats true its been too long and we want to reset so the next click is simply a single click and not a double click.

                    one_click = false;
                }
 
            }

            if (Input.GetMouseButtonDown(1))
            {

                if (Input.GetMouseButtonDown(1))
                { // do one click things;
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                    {
                        if (hit.transform.tag == "Background")
                        {
                            // Click on background. Do action
                            doAction(0, hit.point);
                        }
                    }
                }


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
                Transform newWell = (Transform)Instantiate(gravityWellObject, new Vector3(pos.x, pos.y, 0), Quaternion.identity);
                gWells.Add(newWell);
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


    void updateEnergy()
    {
        // For every active repulsor and gravitywell we must reduce energy
        foreach (Transform gWell in gWells)
        {
            try
            {
                gWell.GetComponent<GravityWell>().getEnergyCost();
            }
            catch
            {

            }
        }




    }
}

public class mainVariables
{
    // Energy at your disposal
    // in about a million trillion watts
    // Let's say Exawatts
    //10,000,000,000,000,000,000
    int energyAvailable = 2000;


}


public class gravityWells 
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