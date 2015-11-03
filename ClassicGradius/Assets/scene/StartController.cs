using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartController : MonoBehaviour {
	static public bool startScene;
	static public int health = 3;

	// Use this for initialization
	void Start () {
		startScene = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (startScene == true) {
			if (Input.GetKey (KeyCode.Return)) {
				Application.LoadLevel("MainScene");
				startScene = false;
				health = 3;
			}
		}
	
	}
}
