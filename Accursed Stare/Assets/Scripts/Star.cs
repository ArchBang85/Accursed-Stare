using UnityEngine;
using System.Collections;
using System.Collections.Generic;


    public class Star : GravityObject
    {
        public GameObject Planet;
        public List<Transform> GravityObjects = new List<Transform>();
        public List<GameObject> Planets = new List<GameObject>();

       
        public void createPlanets()
        {   
            int planetCount = Random.Range(1, 6);
            for (int p = 1; p <= planetCount; p++)
            {
                GameObject planet = (GameObject)Instantiate(Planet, new Vector3(transform.position.x, transform.position.y + p * Random.Range(0.4f, 0.7f), 6.2f), Quaternion.identity);
                planet.transform.Rotate(new Vector3(90, 0, 0));
                float planetScale = Random.Range(0.01f, 0.1f);
                planet.transform.localScale = new Vector3(planetScale, planetScale, planetScale);
                float planetMass = planetScale;
                planet.GetComponent<Rigidbody2D>().mass = planetMass;
                
                // orbital velocity
                float dist = Vector3.Distance(transform.position, planet.transform.position);
                if (dist == 0) dist = 0.01f;
                float orbitalVelocity =  (planetMass * mass) / dist;

            //    Debug.Log(orbitalVelocity);
                planet.GetComponent<GravityObject>().initSpeed = orbitalVelocity;
              //  planet.transform.SetParent(this.transform);

            }
        }
    
        // Use this for initialization
        void Start()
        {
            transform.GetComponent<CircleCollider2D>().radius = GravityRadius;
            transform.GetComponent<Rigidbody2D>().AddForce(Vector2.right * initSpeed);
            // transform.GetComponent<Rigidbody2D>().AddForce(-    Vector2.up * initSpeed);
            if (Planet != null)
                createPlanets();

        }


    }


