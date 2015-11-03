using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PathedEnemyBehaviour : MonoBehaviour {
	public float speed;
	Rigidbody enemyBod;
	enum movementState { forward = 0, back};
	movementState curr = movementState.forward;
	public Vector2 pos1;
	Text score;
	public int points = 100;
	float camH,camW;
	public float dropChance;
	public GameObject PowerUp;

	// Use this for initialization
	void Start () {
		enemyBod = this.GetComponent<Rigidbody> ();
		enemyBod.velocity = new Vector3 (-speed, 0f, 0f);
		score = GameObject.Find ("Score").GetComponent<Text>();
		Camera cam = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		camH = cam.orthographicSize * 2f;
		camW = camH * cam.aspect;
	}
	
	// Update is called once per frame
	void Update () {
		if (this.transform.position.x <= pos1.x && curr == movementState.forward) {
			curr = movementState.back;
			enemyBod.velocity = new Vector3 (speed, 0f , 0f);
		} else if (this.transform.position.x >= camW/2 && curr == movementState.back) {
			Destroy(this.gameObject);
		}
	}

	public void Scored(){
		score.text = (int.Parse (score.text) + points).ToString();
		if (Random.value < dropChance) {
			GameObject power = Instantiate(PowerUp) as GameObject;
			power.GetComponent<Rigidbody>().transform.position = this.gameObject.transform.position;
		}
	}

	void OnDestroy(){

	}
}
