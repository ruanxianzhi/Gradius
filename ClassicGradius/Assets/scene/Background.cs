using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.GetComponent<Rigidbody> ().velocity = new Vector3 (-BackgroundScroll.speed, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		this.GetComponent<Rigidbody> ().velocity = new Vector3 (-BackgroundScroll.speed, 0, 0);
	
	}
}
