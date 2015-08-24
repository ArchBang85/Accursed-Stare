using UnityEngine;
using System.Collections;

public class TestMove : MonoBehaviour {
    public float speed = 1.0f;
    private GameObject gameController;

	// Use this for initialization
	void Start () {
        gameController = GameObject.Find("Main Camera");
	}
	
	// Update is called once per frame
	void Update () {

        if (gameController.GetComponent<GameController>().live == true)
        {


            if (gameController.GetComponent<GameController>().mainData.energyAvailable > 0)
            {


                if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.E))
                {
                    transform.GetComponent<GravityWell>().enabled = true;
                    GameObject.Find("GravityWellEffects1").GetComponent<ParticleSystem>().Play();

                    gameController.GetComponent<GameController>().mainData.energyAvailable -= 1;

                }
                else
                {
                    transform.GetComponent<GravityWell>().enabled = false;
                    GameObject.Find("GravityWellEffects1").GetComponent<ParticleSystem>().Pause();
                    GameObject.Find("GravityWellEffects1").GetComponent<ParticleSystem>().Clear();

                }

                bool moving = false;
                if (Input.GetKey(KeyCode.A))
                {
                    this.transform.GetComponent<Rigidbody2D>().AddForce(-Vector2.right * speed);
                    moving = true;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    this.transform.GetComponent<Rigidbody2D>().AddForce(Vector2.right * speed);
                    moving = true;
                }

                if (Input.GetKey(KeyCode.W))
                {
                    this.transform.GetComponent<Rigidbody2D>().AddForce(Vector2.up * speed);
                    moving = true;
                }
                if (Input.GetKey(KeyCode.S))
                {
                    this.transform.GetComponent<Rigidbody2D>().AddForce(-Vector2.up * speed);
                    moving = true;
                }
                if (moving)
                {
                    gameController.GetComponent<GameController>().mainData.energyAvailable -= 1;
                }


            }
        }
	}
}
