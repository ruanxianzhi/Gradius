using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class DeeE : MonoBehaviour {
	public GameObject EWeapon;
	Text score;
	public int points = 100;
	float camH,camW;
	GameObject eshot;
	public float reload=2.8f;
	public enum timedelay {one,two,three,four};
	float currReload;
	// Use this for initialization
	void Start () {
		this.GetComponent<Rigidbody> ().velocity = new Vector3 (-BackgroundScroll.speed, 0f, 0f);
		score = GameObject.Find ("Score").GetComponent<Text>();
		camH = PlayerController.camH;
		camW = PlayerController.camW;
		currReload = Random.value;

	}
	
	// Update is called once per frame
	void Update () {
		currReload -= Time.deltaTime;
	
		if (this.gameObject.transform.position.x - this.gameObject.transform.lossyScale.x / 2 <= -camW / 2) {
			Destroy(this.gameObject);
			return;
		}	
		if(currReload > 0) return;
		currReload = reload;
		StartCoroutine(Example());
		eshot = Instantiate(EWeapon,this.gameObject.transform.position,this.gameObject.transform.rotation) as GameObject;


	}
	public void Scored(){
		score.text = (int.Parse (score.text) + points).ToString();

	}
	void OnTriggerEnter(Collider coll){
		if (coll.gameObject.tag == "Player") {
			Destroy(this.gameObject);
		}
		else if (coll.gameObject.tag == "PlayerWeapon") {
			Scored();
			if (eshot!=null){
				if (eshot.gameObject.transform.position == this.gameObject.transform.position){
					Destroy(eshot);
				}
			}
			//Destroy(coll.gameObject);
			Destroy(this.gameObject);
		}
	}
	void OnDestroy(){
		
	}
	IEnumerator Example() {
		timedelay x = (timedelay)Random.Range (0, 3);
		float t = 0f;
		switch (x) {
		case timedelay.one:
			t = 1f;
			break;
		case timedelay.two:
			t = 2.5f;
			break;
		case timedelay.three:
			t = 3.8f;
			break;
		case timedelay.four:
			t = 4.5f;
			break;
		}
		yield return new WaitForSeconds (t);
	}
}
