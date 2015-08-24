using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

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
    */    private bool gameEnd = false;
    private GameObject Background;
    public GameObject gravityWellObject;
    public GameObject repulsion;

    private bool threeGotten = false;

    public GameObject introText;
    private gravityWells gW;

    private int zoomLevel = 1;

    public mainVariables mainData;
    public List<GameObject> gWells = new List<GameObject>();
    public bool live = false;

    public AudioSource music;

    public GameObject EnergyText;
    public GameObject StarText;

	// check for clicks on the background cube
     bool one_click = false;
     bool timer_running;
     float timer_for_double_click = 0.0f;
    public void restartLevel()
     {
         Application.LoadLevel(Application.loadedLevel);
     }

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
        float zoomSpeed = duration;
        if (diff < 0)
        {
            for (float f = 0f; f < Mathf.Abs(diff); f += Time.deltaTime * zoomSpeed)
            {

                transform.GetComponent<Camera>().orthographicSize = originalSize - f;

                yield return null;
            }
        }
        else
        {
            for (float f = 0f; f < Mathf.Abs(diff); f += Time.deltaTime * zoomSpeed)
            {

                transform.GetComponent<Camera>().orthographicSize = originalSize + f;

                yield return null;
            }
        }

    }


    public void pause() 
    {
        Time.timeScale = 0.0f;
    
    }

    public void halfSpeed()
    {
        Time.timeScale = 0.5f;
        music.pitch = 0.5f;
    }
    public void fullSpeed()
    {
        Time.timeScale = 1.0f;
        music.pitch = 1.0f;
    }

    public void fastSpeed()
    {
        Time.timeScale = 4.0f;
        music.pitch = 1.4f;
    }

    // at the start get the game to run at 10x speed for 5 secs
    public IEnumerator FastForward(float duration)
    {
     
        yield return new WaitForSeconds(0.7f);
       
        Time.timeScale = 25.0f;
       // Debug.Log("time at 10");
        yield return new WaitForSeconds(50.0f);

        Time.timeScale = 1.0f;
      //  Debug.Log("time at 1");
        GameObject.Find("SunHeart").GetComponent<SunBehaviour>().SunReverse();
        introText.SetActive(true);
        live = false;


    }

    public void zoomToggle()
    {
        float dur = 2.6f;
        zoomLevel++;
        if(zoomLevel > 4)
        {
            zoomLevel = 0;
            transform.parent = null;
            transform.position = new Vector2(0, 0);
        }
        if(zoomLevel == 0)
        {
            StartCoroutine(CameraTransition(0.8f, dur));
        }
        if(zoomLevel == 1)
        {
            StartCoroutine(CameraTransition(5.0f, dur));

        }
        if (zoomLevel == 2)
        {
            StartCoroutine(CameraTransition(6.3f, dur));

        }
        if (zoomLevel == 3)
        {
            StartCoroutine(CameraTransition(10.0f, dur));

        } 
        if (zoomLevel == 4)
        {
            StartCoroutine(CameraTransition(5.0f, dur));
            transform.parent = GameObject.Find("GravityWell").transform;
            transform.position = new Vector2(GameObject.Find("GravityWell").transform.position.x, GameObject.Find("GravityWell").transform.position.y);
        }

    }


	// Use this for initialization
	void Start () {
        gW = new gravityWells();
        mainData = new mainVariables();
        Background = GameObject.Find("Background");

        StartCoroutine(StartTransition());

        InvokeRepeating("updateEnergy", 30.0f, 0.3f);
        
    }
    IEnumerator StartTransition()
    {
        yield return new WaitForSeconds(0.4f);
        StartCoroutine(CameraTransition(0.7f, 0.4f));
        yield return new WaitForSeconds(0.7f);
        StartCoroutine(FastForward(5.0f));
   
    }
    public void updateStarText()
    {
        StarText.GetComponent<Text>().text = "Stars the sun has been given: " + mainData.starsEaten +  "/6";
    }
	
	// Update is called once per frame
	void Update () {

        if(!live && Time.time > 30f)
        {
            if(Input.anyKeyDown)
            {
                live = true;
                zoomToggle();
                introText.SetActive(false);
            }
        }

        if (live)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                zoomToggle();
                //StartCoroutine(CameraTransition(6, 5.0f));
            }
            // General controls
            if (Input.GetKeyDown(KeyCode.R))
            {
                Application.LoadLevel(Application.loadedLevel);
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                mainData.starsEaten++;
                //  updateEnergyText("30");
            }

            if (live)
            {/*
                if (Input.GetMouseButtonDown(1))
                {
                    Debug.Log("Clicking");
                        RaycastHit hit;
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                        {
                            if (hit.transform.tag == "Background")
                            {
                                // Click on background. Do action
                                doAction(1, hit.point);
                            }
                        }
               


                }
                /*
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
                */

            }
        }
	}
    

    


    public void doAction(int actionType, Vector3 pos)
    {
        if(actionType == 0)
        {
            // See if there already is a 


            // Create new gravity well if possible
           // if (gW.canInstantiate())
            //{
            GameObject newWell = Instantiate(gravityWellObject, new Vector3(pos.x, pos.y, 0), Quaternion.identity) as GameObject;
                gWells.Add(newWell);
                Debug.Log("Added " + newWell);
                // deduct instantiation cost from energy reserves
                mainData.energyAvailable -= newWell.transform.GetComponent<GravityWell>().getStartingCost();    

               // gW.addWell();
           // }

        }
    
        else if (actionType == 1)
        {
            // Create new repulsion field if possible
           // if (gW.canInstantiate())
            //{
                Instantiate(repulsion, new Vector3(pos.x, pos.y, 0), Quaternion.identity);
           // }
        }

    }

    public void checkEnd()
    {
        // check end
        if (!gameEnd)
        {
            if (mainData.starsEaten >= mainData.starLimit)
            {
                gameEnd = true;

                introText.SetActive(true);
                GameObject.Find("IntroText").SetActive(false);
                GameObject.Find("EndText").GetComponent<Text>().enabled = true;
            }
        }
    }

    void updateEnergy()
    {
       // Debug.Log("u");
        // For every active repulsor and gravitywell we must reduce energy
        if(!threeGotten)
        {
            if(mainData.starsEaten>=3)
            {
                threeGotten = true;
                zoomToggle();
            }
              
        }
        checkEnd();



    

        foreach(GameObject g in gWells)
        {

                int energyCost = 0;
                //try
                //{
                energyCost = g.transform.GetComponent<GravityWell>().getEnergyCost();
                Debug.Log(energyCost);
                //}
                //catch
                //{

                //}
                mainData.energyAvailable -= energyCost;
            
           
        }
        updateEnergyText(mainData.energyAvailable.ToString());
    }

    public void updateEnergyText(string s)
    {
        EnergyText.GetComponent<Text>().text = "Energy available: " + s + " Exajoules";

    }



}

public class mainVariables
{
    // Energy at your disposal
    // in about a million trillion watts
    // Let's say Exawatts
    //10,000,000,000,000,000,000
    public int energyAvailable = 2000;

    public int starsEaten = 0;
    public int starLimit = 6;



    

}

public class gravityWells 
{
    int allowedGravityWells = 3;

    public void removeWell()
    {
        // Instantiate gravity well on the map if there's space 

        //allowedGravityWells++;
        // update GUI
    }
    public void addWell()
    {
       // allowedGravityWells--;
    }
    public int value()
    {
        return allowedGravityWells;
    }

    public bool canInstantiate()
    {
        //if (allowedGravityWells > 0)
            return true;
        //return false;
    }

}