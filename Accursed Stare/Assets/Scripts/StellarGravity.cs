using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StellarGravity : MonoBehaviour {
    public float GravityPower = 10;
    public List<Transform> AddGravityToObject = new List<Transform>();
    //public GroundDetection GroundDetectionScript;

    // Entering gravitational field 
    void onTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Gravity field entered at " + transform.position);
        if(other.GetComponent<Rigidbody2D>() != null)
        {           
            AddGravityToObject.Add(other.transform);
        }
    }

    void onTriggerStay2D(Collider2D other)
    {
        Debug.Log("In gravity well");
    }
    
    void onTriggerExit2D(Collider2D other)
    {
        AddGravityToObject.Remove(other.transform);
    }

    void FixedUpdate()
    {
        for (int i = 0; i < AddGravityToObject.Count; i++)
        {
            //GroundDetectionScript = AddGravityToObject[i].GetComponent("GroundDetection") as GroundDetection;
            transform.GetComponent<Rigidbody2D>().AddForce((transform.position - AddGravityToObject[i].transform.position) * GravityPower * Time.deltaTime);

        }

    }


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
