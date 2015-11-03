using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartController : MonoBehaviour {
	static public bool startScene = true;
	static public int health = 3;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (startScene == true) {
			if (Input.GetKey (KeyCode.Return)) {
				Application.LoadLevel ("Instruction");
				startScene = false;
				health = 3;
			}
		} else {
			if (Input.GetKey (KeyCode.Return)) {
			Application.LoadLevel ("MainScene2");
			health = 3;
			}
		}
	
	}
}
