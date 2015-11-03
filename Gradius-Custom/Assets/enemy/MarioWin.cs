using UnityEngine;
using System.Collections;

public class MarioWin : MonoBehaviour {
	public GameObject shiprigidbody ;
	int count;

	// Use this for initialization
	void Start () {
		shiprigidbody = GameObject.Find ("Player");
		count = 0;
		PlayerController.invinMode = true;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = shiprigidbody.transform.position;
		pos.y += 0.5f;
		pos.z = -1;
		transform.position = pos;
		count += 1;
		if (count == 600) {
			Application.LoadLevel("the_end");
			StartController.startScene = true;
			StartController.health = 3;
			Spawner.spot = 0;
		}
	}
}
