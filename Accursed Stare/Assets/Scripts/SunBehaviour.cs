
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SunBehaviour : MonoBehaviour {
    public GameObject Planet;
    public List<GameObject> Planets = new List<GameObject>();
    public float GravityRadius = 2.0f;
    public float GravityPower = 2.0f;

    public bool gravityFieldDisturbed = false;
	// Use this for initialization
	void Start () {
        createPlanets();
	}

    public void createPlanets()
    {
        int planetCount = 0;
        for (int p = 1; p <= planetCount; p++)
        {
            GameObject planet = (GameObject)Instantiate(Planet, new Vector2(transform.position.x, transform.position.y + p * Random.Range(0.2f, 0.5f)), Quaternion.identity);
            planet.transform.Rotate(new Vector3(90, 0, 0));
            float planetScale = 0f;
            if (p == 1)
            {
                planetScale = 0.02f;
            }
            else if (p == 2)
            {
                planetScale = 0.03f;
            }
            else if (p == 3)
            {
                planetScale = 0.04f;
            }
            else if (p == 4)
            {
                planetScale = 0.04f;
            }
            else if (p == 5)
            {
                planetScale = 0.12f;
            }
            else if (p == 6)
            {
                planetScale = 0.1f;
            }
            else if (p == 7)
            {
                planetScale = 0.09f;
            }
            else if (p == 8)
            {
                planetScale = 0.09f;
            }
            else if (p == 9)
            {
                planetScale = 0.01f;
            }

            planet.transform.localScale = new Vector3(planetScale, 1, planetScale);
            planet.GetComponent<GravityObject>().initSpeed = Random.Range(0.1f, 0.3f);
            planet.GetComponent<GravityObject>().GravityPower = 0.01f;

        }
    }

    public void activatePlanetaryGravity()
    {
        if(!gravityFieldDisturbed)
        {
            gravityFieldDisturbed = true;
            // activate gravity for all planets
            foreach(GameObject p in Planets)
            {
                try
                {


                    p.GetComponent<Planet>().enabled = true;
                    p.GetComponent<Collider2D>().enabled = true;
                    p.GetComponent<Planet>().initSpeed = 0f;
                }
                catch
                {

                }
                }


        }
    }
    public void SunReverse()
    {
        // reverse orbits and sun spoke movement
        foreach(GameObject p in Planets)
        {
            try
            {
                p.transform.parent.GetComponent<Orbit>().directionToggle();
                p.transform.GetComponent<TrailRenderer>().enabled = true;
                p.transform.GetComponent<TrailRenderer>().time = 36.0f;
                GameObject.Find("SunSpokes").GetComponent<Orbit>().directionToggle();
                GameObject.Find("SunSpokesCurved").GetComponent<Orbit>().directionToggle();
            }
            catch
            {
                Debug.Log("Orbit Reversal Error");
            }
        }

    }
    void FixedUpdate()
    {

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, GravityRadius);
        int i = 0;
        while (i < hitColliders.Length)
        {

            if (hitColliders[i].tag == "Gravity" || hitColliders[i].tag == "Star" || hitColliders[i].tag == "Core" && hitColliders[i].gameObject != this.gameObject)
            {


                if (!gravityFieldDisturbed) { if (hitColliders[i].tag == "Core") { activatePlanetaryGravity(); } }
                // force of gravity is inversely proportional to the square of the distance between them
                float distance = Vector3.Distance(transform.position, hitColliders[i].transform.position);
                // compensate for non-working trigger exit


                float localGravityPower = GravityPower / (distance * distance);

                //GroundDetectionScript = GravityObjects[i].GetComponent("GroundDetection") as GroundDetection;
                if (localGravityPower != 0)
                {
                    try
                    {
                        hitColliders[i].transform.GetComponent<Rigidbody2D>().AddForce((transform.position - hitColliders[i].transform.position) * localGravityPower * Time.deltaTime);
                    }
                    catch
                    {
                        Debug.Log("Gravity attraction error for the Sun");
                    }
                    }
            }
            i++;
        }

    }
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.P))
        {
            //activatePlanetaryGravity();
        }
	}

    private void OnTriggerEnter2D (Collider2D other)
    {
        if(other.tag == "Core")
        {
            // Absorb the incoming object
            Destroy(other.transform.parent.gameObject, 0.0f);

            Debug.Log("Increasing sun size");
            // Make the sun a bit bigger
           // this.transform.localScale *= 1.1f;            
            if (other.name == "StarHeart")
            {
               // --this.transform.localScale *= 1.3f;

                GameObject.Find("Main Camera").GetComponent<GameController>().mainData.starsEaten += 1;
                GameObject.Find("Main Camera").GetComponent<GameController>().updateStarText();
            }
        }

    }
}
