using UnityEngine;
using System.Collections;

public class CoreForce : MonoBehaviour {
	public GameObject[] shotsup;
	public GameObject[] shotsdown;
	float camH,camW;
	public static Vector3 dir;
	public static Vector3 dirop;
	float respawn=12f;
	// Use this for initialization
	void Start () {
		Camera cam = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		camH = cam.orthographicSize * 2f;
		camW = camH * cam.aspect;
		this.gameObject.transform.position = new Vector3 (0f,1.2f*camH,0f);
		dir = Vector3.up;
		dirop = Vector3.down;
	}
	
	// Update is called once per frame
	void Update () {

		shotsup = GameObject.FindGameObjectsWithTag("Shot1");
		shotsdown = GameObject.FindGameObjectsWithTag("Shot2");

		foreach (GameObject shot in shotsup) {
			var distance = Vector3.Distance(this.transform.position, shot.transform.position);
			//shot.GetComponent<Rigidbody>().AddForce((this.gameObject.transform.position-shot.transform.position)* 250f * Time.smoothDeltaTime);
			shot.GetComponent<Rigidbody>().AddForce(dir* 1580f * Time.smoothDeltaTime);
			
		}
		foreach (GameObject shot in shotsdown) {
			var distance = Vector3.Distance(this.transform.position, shot.transform.position);
			//shot.GetComponent<Rigidbody>().AddForce((this.gameObject.transform.position-shot.transform.position)* 250f * Time.smoothDeltaTime);
			shot.GetComponent<Rigidbody>().AddForce(dirop* 1580f * Time.smoothDeltaTime);
			
		}


		respawn -= Time.deltaTime;
		if (respawn > 0)
			return;
		respawn = 12f;
		if (dir == Vector3.up) {
			dir = Vector3.down;
			dirop = Vector3.up;
		} else {
			dir = Vector3.up;
			dirop = Vector3.down;
		}

	}
}
