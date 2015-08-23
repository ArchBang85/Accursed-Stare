using UnityEngine;
using System.Collections;

public class Orbit : MonoBehaviour {

    private float masterSpeed = 1.0f;
    public float speed = 1.0f;
    public bool clockwise = true;
    public int dir = 1;

	// Use this for initialization
	void Start () {
        speed = Random.Range(0.5f, 3.0f);
	}
	
	// Update is called once per frame
	void Update () {
        if (clockwise)
        {
            if (dir == 1)
            {

                this.transform.Rotate(Vector3.up * Time.deltaTime * speed * masterSpeed);

            }
            else if (dir == 2)
            {
                this.transform.Rotate(-Vector3.forward * Time.deltaTime * speed * masterSpeed);
            }

        }

        else
        {   
            if (dir == 1)
            {

                this.transform.Rotate(-Vector3.up * Time.deltaTime * speed * masterSpeed);

            }
            else if (dir == 2)
            {
                this.transform.Rotate(Vector3.forward * Time.deltaTime * speed * masterSpeed);
            }
        }

	}
}
