using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class obstacles_movement : MonoBehaviour {
	Rigidbody enemyBod;
	public int points = 400;
	public float speed;
	public bool dirup = true;
	public float bound ;

	// Use this for initialization
	void Start () {
		enemyBod = this.GetComponent<Rigidbody> ();
		enemyBod.velocity = new Vector3 (-BackgroundScroll.speed, 0f, 0f);
		speed = Random.value*4f;
		if (speed < 1.2f)
			speed = 1.4f;
		bound = 3.5f ;
	}
	
	// Update is called once per frame
	void Update () {
		if (this.gameObject.transform.position.x + this.gameObject.transform.lossyScale.x / 2 <= -PlayerController.camW / 2) {
			Destroy(this.gameObject);
			return;	
		}

		if (dirup)
			enemyBod.velocity = new Vector3 (-BackgroundScroll.speed, speed, 0f);
		else
			enemyBod.velocity = new Vector3 (-BackgroundScroll.speed, -speed, 0f);
			if (transform.position.y >= bound ) {
				dirup = false;
			}
			if (transform.position.y <= 0) {
				dirup = true;
			}
			
	}
	void OnTriggerEnter (Collider coll){
	//	enemyBod.velocity = new Vector3 (-BackgroundScroll.speed, -speed, 0f);
	}
}
