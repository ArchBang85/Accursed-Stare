using UnityEngine;
using System.Collections;

public class RandomMove : MonoBehaviour {
    float speed;
    Vector2 dir;
	// Use this for initialization
	void Start () {
        speed = Random.Range(0.01f, 0.03f);
        dir = new Vector2(Random.Range(-1f, 1f), 0);// Random.Range(-1f, 1f));
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time > 60f)
            {
                transform.Translate(dir * speed * Time.deltaTime);
            }
        }
}
