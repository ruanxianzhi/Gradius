using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class RoadBaby : MonoBehaviour {
	int side = 1;
	Text score;
	public int points = 100;

	// Use this for initialization
	void Start () {
		score = GameObject.Find ("Score").GetComponent<Text>();
		if (transform.position.y > 0)
			side = -1;
	}
	
	// Update is called once per frame
	void Update () {
		this.GetComponent<Rigidbody> ().velocity = new Vector3 (-BackgroundScroll.speed, 4f*side, 0);
		if (transform.position.y <= -PlayerController.camW/ 2) {
			Destroy(this.gameObject);
		}
	}

	public void Scored(){
		score.text = (int.Parse (score.text) + points).ToString();
	}

	void OnTriggerEnter(Collider coll){
		if (coll.gameObject.tag == "Player") {
			Destroy (this.gameObject);
		} else if (coll.gameObject.tag == "Shield"||coll.gameObject.tag == "Shot1"||coll.gameObject.tag == "Shot2") {
			Scored();
			Destroy(this.gameObject);
		}
	}
}
