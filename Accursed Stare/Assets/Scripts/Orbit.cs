using UnityEngine;
using System.Collections;

public class Orbit : MonoBehaviour {

    private float masterSpeed = 1.0f;
    public float speed = 1.0f;
    public bool clockwise = true;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    if(clockwise)
        {
            this.transform.Rotate(Vector3.up * Time.deltaTime * speed * masterSpeed);
            
        }
        else
        {
            this.transform.Rotate(-Vector3.up * Time.deltaTime * speed * masterSpeed);
            
        }
	}
}
