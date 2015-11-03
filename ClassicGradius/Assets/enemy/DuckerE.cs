
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DuckerE : MonoBehaviour {
	float camH,camW;
	public float speed = 2f;
	public GameObject EWeapon;
	public int points = 100;
	Text score;
	public float reload=1f;
	//float currReload;
	public enum movestage{first,second};
	public movestage cur = movestage.first;
	public GameObject shiprigidbody ;
	public Vector3 middle = new Vector3(0f,0f,0f);
	//GameObject eshot;
	public GameObject PowerUp;
	public bool isRed;
	public Vector3 groundedHitPoint;
	// Use this for initialization
	void Start () {
		//currReload = reload;
		shiprigidbody = GameObject.Find ("Player");
		Camera cam = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		camH = cam.orthographicSize * 2f;
		camW =camH * cam.aspect;
		score = GameObject.Find ("Score").GetComponent<Text>();
		middle.y = this.gameObject.transform.position.y;
		
		var heading = middle - this.gameObject.transform.position;

		var distance = heading.magnitude;
		var direction = heading / distance;
		this.GetComponent<Rigidbody> ().velocity = speed * direction;
	}
	
	// Update is called once per frame
	void Update () {
		
		//Debug.Log (this.gameObject.transform.position);

		if (cur == movestage.second) {
			this.GetComponent<Rigidbody> ().velocity = new Vector3 (-2*speed, 0f, 0f);
			if (this.gameObject.transform.position.x - this.gameObject.transform.lossyScale.x / 2 >= camW / 2) {
				Destroy(this.gameObject);
				return;
			}	
			else if (this.gameObject.transform.position.x - this.gameObject.transform.lossyScale.x / 2 <= -camW / 2){
				Destroy(this.gameObject);
				return;				
			}

		}
		else {
			float tmp = this.gameObject.transform.position.x-shiprigidbody.gameObject.transform.position.x;
			
			if (tmp>=camW/3 ) {
				if (tmp  >= camW/2){
					this.GetComponent<Rigidbody> ().velocity = new Vector3 (2*speed, 0f, 0f);
				}
				else{

				this.GetComponent<Rigidbody> ().velocity = new Vector3 (0f, 0f, 0f);
				//currReload -= Time.deltaTime;
				//if (currReload > 0){
				//	return;
				//}
				//else{
					StartCoroutine (delay());
					Instantiate (EWeapon, this.gameObject.transform.position, this.gameObject.transform.rotation);
					cur = movestage.second;
				this.GetComponent<Rigidbody> ().velocity = new Vector3 (-2*speed, 0f, 0f);
					//currReload = reload;
				//}
				}
				
			} 
			//else if (tmp>0) {
			//	this.GetComponent<Rigidbody> ().velocity = new Vector3 (-speed, 0f, 0f);
				//currReload = reload;
				
			//}
			else{
				this.GetComponent<Rigidbody> ().velocity = new Vector3 (speed, 0f, 0f);
				//currReload = reload;
			}
		}
	}
	
	
	
	public void Scored(){
		score.text = (int.Parse (score.text) + points).ToString();
		/*if (Random.value < dropChance) {
			GameObject power = Instantiate(PowerUp) as GameObject;
			power.GetComponent<Rigidbody>().transform.position = this.gameObject.transform.position;
		}
		*/
	}
	void OnTriggerEnter(Collider coll){
		if (coll.gameObject.tag == "Player") {
			Destroy (this.gameObject);
		} else if (coll.gameObject.tag == "PlayerWeapon") {
			if (EWeapon != null) {
				if (EWeapon.transform.position == this.gameObject.transform.position) {
					Destroy (EWeapon);
				}
			}
			if (isRed == true) {
				GameObject power = Instantiate (PowerUp) as GameObject;
				power.GetComponent<Rigidbody> ().transform.position = this.gameObject.transform.position;
			}
			Scored ();
			//Destroy(coll.gameObject);
			Destroy (this.gameObject);
		} /*else if (coll.gameObject.tag == "Hill") {
			Debug.Log (this.gameObject.transform.position);
			float distance = 2f; // I use this value to make sure the Debug ray is identical (Of the same length) to the Ray I cast.
			//Debug.DrawRay(placeSpot.transform.position, Vector3.down * distance, Color.green);

				RaycastHit hit;
				if (Physics.Raycast(this.gameObject.transform.position, Vector3.up, out hit))
				{
					float distanceToGround = hit.distance;
				Debug.Log(distanceToGround);
					Debug.Log("In coll");
					Debug.Log(hit.point);
					//this.gameObject.transform.parent = coll.gameObject.transform;
					groundedHitPoint = hit.point;
				this.gameObject.transform.position = hit.point;
				//this.gameObject.transform.Translate(new Vector3(this.gameObject.transform.position.x,this.gameObject.transform.position.y + distanceToGround, this.gameObject.transform.position.z));
					
				}
			Debug.Log (this.gameObject.transform.position);

		}*/
	}
	void OnDestroy(){
		
	}
	IEnumerator delay() {
		yield return new WaitForSeconds(1.2f);
	}
	
}
