using UnityEngine;
using System.Collections;

public class Planet : GravityObject {

    public GameObject deathExplosion;
	// Use this for initialization

	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P))
        {
            //destroyPlanet();
        }
	}

    void destroyPlanet()
    {
        Debug.Log("Destroying Planet!");
        GameObject deathExp = (GameObject)Instantiate(deathExplosion, transform.position, Quaternion.identity);
        Destroy(deathExp, 3.0f);

        // Deactivate and then destroy to leave the trail visible for a while
        this.transform.GetComponent<Renderer>().enabled = false;
        this.transform.GetComponent<Rigidbody2D>().Sleep();
        this.transform.GetComponent<Collider2D>().enabled = false;
        Destroy(this.gameObject, 30.0f); 


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colliding");
        if(collision.transform.tag == "Core")
        {
            destroyPlanet();
        }

    }

}
