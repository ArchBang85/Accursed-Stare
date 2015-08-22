using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StarBehaviour : MonoBehaviour
{
        public float GravityPower = 10;
        public List<Transform> GravityObjects = new List<Transform>();
        //public GroundDetection GroundDetectionScript;
        public float GravityRadius = 2.0f;

        public bool initVelocity = false;
        public float initSpeed = 2.0f;



        void FixedUpdate()
        {

            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, GravityRadius);
            int i = 0;
            while (i < hitColliders.Length)
            {
           
                if (hitColliders[i].tag == "Gravity" && hitColliders[i].gameObject != this.gameObject)
                {
                    // force of gravity is inversely proportional to the square of the distance between them
                    float distance = Vector3.Distance(transform.position, hitColliders[i].transform.position);
                    // compensate for non-working trigger exit


                    float localGravityPower = GravityPower / (distance * distance);

                    //GroundDetectionScript = GravityObjects[i].GetComponent("GroundDetection") as GroundDetection;
                    hitColliders[i].transform.GetComponent<Rigidbody2D>().AddForce((transform.position - hitColliders[i].transform.position) * localGravityPower * Time.deltaTime);

                }
                i++;
            }

        }


    // Use this for initialization
    void Start()
    {
        transform.GetComponent<CircleCollider2D>().radius = GravityRadius;
        transform.GetComponent<Rigidbody2D>().AddForce(Vector2.right * initSpeed);
        transform.GetComponent<Rigidbody2D>().AddForce(-    Vector2.up * initSpeed);
    }

    // Update is called once per frame
    void Update()
    { }



    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        /*Debug.Log("Gravity field entered at " + transform.position);
        if (other.GetComponent<Rigidbody2D>() != null)
        {
            GravityObjects.Add(other.transform);
        }
   
    }

    private void onTriggerExit2D(Collider2D other)
    {
        Debug.Log("Leaving gravity well at " + transform.position);
        GravityObjects.Remove(other.transform);
    }
    
    /*private void onTriggerStay2D(Collider2D other)
    {
        Debug.Log("In gravity well");
    }*/



}
