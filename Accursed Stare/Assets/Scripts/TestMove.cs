using UnityEngine;
using System.Collections;

public class TestMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
            if(Input.GetKey(KeyCode.A))
            {
                this.transform.GetComponent<Rigidbody2D>().AddForce(-Vector2.right);
            }
            if (Input.GetKey(KeyCode.D))
            {
                this.transform.GetComponent<Rigidbody2D>().AddForce(Vector2.right);
            }

            if (Input.GetKey(KeyCode.W))
            {
                this.transform.GetComponent<Rigidbody2D>().AddForce(Vector2.up);
            }
            if (Input.GetKey(KeyCode.S))
            {
                this.transform.GetComponent<Rigidbody2D>().AddForce(-Vector2.up);
            }
	}
}
