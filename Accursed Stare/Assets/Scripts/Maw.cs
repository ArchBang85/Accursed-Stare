using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Maw : MonoBehaviour {

    public GameObject flareObject;
    private int energyFromEating = 1000;
    public AudioSource sound;
    private int eatCounter = 0;

    public GameObject wText;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKey(KeyCode.P))
        {
            wText.GetComponent<Text>().enabled = true;
        }
	    if(eatCounter > 4)
        {
           wText.GetComponent<Text>().enabled = true;
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.transform.tag == "Core" && collision.transform.name != "SunHeart")
        {
            // try
            //   {
            //Debug.Log("maw colliding");
            GameObject flare = (GameObject)Instantiate(flareObject, transform.position, Quaternion.identity);
            Destroy(flare, 12.0f);

            collision.transform.parent.position = this.transform.position;
            //collision.transform.parent.GetComponent<Rigidbody2D>().drag = 1000;
            //collision.transform.GetComponent<Rigidbody2D>().Sleep();
            collision.transform.parent.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            Debug.Log(energyFromEating);
            // swallow star, increment energy available
            GameObject.Find("Main Camera").GetComponent<GameController>().mainData.energyAvailable += energyFromEating;
            eatCounter += 1;

            Destroy(collision.transform.parent.gameObject, 2.0f);
            Debug.Log(GameObject.Find("Main Camera").GetComponent<GameController>().mainData.energyAvailable);

            sound.GetComponent<AudioSource>().Play();
            //    }
            //    catch { }
            //    }


        }
    }
}
