using UnityEngine;
using System.Collections;

public class die : MonoBehaviour {
	public float t;
	// Use this for initialization
	void Start () {
		t = 1f;
	}
	
	// Update is called once per frame
	void Update () {
		t -= Time.deltaTime;
		if (t < 0) {
			Destroy(this.gameObject);
		}
	}
}
