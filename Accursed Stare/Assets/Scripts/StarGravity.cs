using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/*
public class StarGravity : MonoBehaviour
{
        public float GravityPower = 10;
        public GameObject Planet;
        public List<Transform> GravityObjects = new List<Transform>();
        public List<GameObject> Planets = new List<GameObject>();
    //public GroundDetection GroundDetectionScript;
        public float GravityRadius = 2.0f;

        public bool initVelocity = false;
        public float initSpeed = 2.0f;



        public void createPlanets()
        {
            int planetCount = Random.Range(0, 6);
            for (int p = 1; p <= planetCount; p++)
            {
                GameObject planet = (GameObject) Instantiate(Planet, new Vector3(transform.position.x, transform.position.y + p * Random.Range(0.2f, 0.5f), 6.2f), Quaternion.identity);
                planet.transform.Rotate(new Vector3(90, 0, 0));
                float planetScale = Random.Range(0.01f, 0.1f);
                planet.transform.localScale = new Vector3(planetScale, planetScale * 4, planetScale);
                planet.GetComponent<StarGravity>().initSpeed = Random.Range(0.5f, 3.0f);

            }

        }

        void FixedUpdate()
        {

            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, GravityRadius);
            int i = 0;
            while (i < hitColliders.Length)
            {
           
                if ((hitColliders[i].tag == "Gravity" || hitColliders[i].tag == "Planet") && hitColliders[i].gameObject != this.gameObject)
                {
                    // force of gravity is inversely proportional to the square of the distance between them
                    float distance = Vector3.Distance(transform.position, hitColliders[i].transform.position);
                    // compensate for non-working trigger exit

                    float localGravityPower = GravityPower / (distance * distance);         
                    //GroundDetectionScript = GravityObjects[i].GetComponent("GroundDetection") as GroundDetection;
                    if (localGravityPower != 0)
                    {

                        hitColliders[i].transform.GetComponent<Rigidbody2D>().AddForce((transform.position - hitColliders[i].transform.position) * localGravityPower * Time.deltaTime);
                    }
                }
                i++;
            }

        }


    // Use this for initialization
    void Start()
    {
        transform.GetComponent<CircleCollider2D>().radius = GravityRadius;
        transform.GetComponent<Rigidbody2D>().AddForce(Vector2.right * initSpeed);
       // transform.GetComponent<Rigidbody2D>().AddForce(-    Vector2.up * initSpeed);
        if(Planet!= null)
            createPlanets();



    }


    // Update is called once per frame
    void Update()
    { }






}
*/