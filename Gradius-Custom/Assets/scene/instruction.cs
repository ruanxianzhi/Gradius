using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class instruction : MonoBehaviour {
	static public bool startScene;

	// Use this for initialization
	void Start () {
		startScene = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (startScene == true) {
			if (Input.GetKey (KeyCode.Return)) {
				Application.LoadLevel("MainScene2");
				startScene = false;
				StartController.health = 3;
			}
		}
	
	}
}
