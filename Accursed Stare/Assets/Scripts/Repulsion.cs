using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Repulsion : MonoBehaviour {

    public float RepulsionLifetime = 3.0f;
	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, RepulsionLifetime);
	}


    public float RepulsionPower = 10;
    public List<Transform> RepulsionObjects = new List<Transform>();
    //public GroundDetection GroundDetectionScript;
    public float RepulsionRadius = 2.0f;

    void FixedUpdate()
    {

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, RepulsionRadius);

        int i = 0;
        while (i < hitColliders.Length)
        {

            if (hitColliders[i].tag == "Gravity")
            {
                // force of gravity is inversely proportional to the square of the distance between them
                float distance = Vector3.Distance(transform.position, hitColliders[i].transform.position);
                // compensate for non-working trigger exit

                float localRepulsionPower = RepulsionPower / (distance * distance);

                //GroundDetectionScript = GravityObjects[i].GetComponent("GroundDetection") as GroundDetection;
                hitColliders[i].transform.GetComponent<Rigidbody2D>().AddForce((-transform.position + hitColliders[i].transform.position) * localRepulsionPower * Time.deltaTime);

            }
            i++;
        }

    }


	// Update is called once per frame
	void Update () {
	    



	}
}
