using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StarGravity : MonoBehaviour
{
    public float GravityPower = 10;
    public List<Transform> GravityObjects = new List<Transform>();
    //public GroundDetection GroundDetectionScript;

  
    void FixedUpdate()
    {
        for (int i = 0; i < GravityObjects.Count; i++)
        {
            //GroundDetectionScript = GravityObjects[i].GetComponent("GroundDetection") as GroundDetection;
            GravityObjects[i].transform.GetComponent<Rigidbody2D>().AddForce((transform.position - GravityObjects[i].transform.position) * GravityPower * Time.deltaTime);

        }

    }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    { }

    // Entering gravitational field 


    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Gravity field entered at " + transform.position);
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
