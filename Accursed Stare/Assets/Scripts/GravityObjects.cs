using UnityEngine;
using System.Collections;
using System.Collections.Generic;

abstract public class GravityObject : MonoBehaviour
{
    public float GravityPower = 10;
    public float GravityRadius = 2.0f;

    // gravityobjects can have initial velocity
    public bool initVelocity = false;
    public float initSpeed = 2.0f;

    public float mass;

    void Start()
    {
        if(transform.GetComponent<Rigidbody2D>() != null)
        {
            mass = transform.GetComponent<Rigidbody2D>().mass;
        }

        if(initSpeed!=0.0f)
        {
            transform.GetComponent<Rigidbody2D>().AddForce(Vector2.right * initSpeed);
        }


    }


    void FixedUpdate()
    {       

        if(Time.time > 0.1f)
        {
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(this.transform.position, GravityRadius);
            int i = 0;
            while (i < hitColliders.Length)
            {
                if ((hitColliders[i].tag == "Gravity" || hitColliders[i].tag == "Planet" || hitColliders[i].tag == "Star") && hitColliders[i].gameObject != this.gameObject)
                {
                    // force of gravity is inversely proportional to the square of the distance between them
                    float distance = Vector3.Distance(transform.position, hitColliders[i].transform.position);
                    // compensate for non-working trigger exit

                    // GravityPower should be (mass1 * mass2) / (distance * distance)

                    float localGravityPower = (mass * hitColliders[i].GetComponent<GravityObject>().mass) / (distance * distance);
                    
                    //GroundDetectionScript = GravityObjects[i].GetComponent("GroundDetection") as GroundDetection;
                    if (localGravityPower != 0)
                    {
                        hitColliders[i].transform.GetComponent<Rigidbody2D>().AddForce((transform.position - hitColliders[i].transform.position) * localGravityPower * Time.deltaTime);
                    }
                }
                i++;
            }
        }
    }
}
