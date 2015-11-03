using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class TimeScaler {
	public float time;
	public float timeScale;
}

public class CameraMover : MonoBehaviour {
	static public 	CameraMover S;
	public float 	timeStart, timeDuration = 1f;
	public 			List<TimeScaler> timeScalers;
	public bool 	running = false;
	public float 	fullRotation = -360;
	private float 	startRY;
	public GUIText	timer, scale, fdt;
	public float	afterTimeEasing = 0.1f;
	public float	afterTimeScale = 0.1f;
	
	Vector3 		p0, p1;
	Transform 		cam;

	void Awake () {
		S = this;
		cam = transform.Find("Main Camera");
		p0 = cam.localPosition;
		Transform t = transform.Find("CamEnd");
		p1 = t.localPosition;

		Time.timeScale = 0;

		startRY = transform.rotation.eulerAngles.y;
	}
	
	// Update is called once per frame
	void Update () {
		if (!running && Input.GetMouseButtonDown(0)) {
			running = true;
			timeStart = Time.time;
		}

		if (Input.GetKeyDown(KeyCode.R)) {
			Application.LoadLevel("_Scene_0");
		}

		if (!running) return;
		// Determine desired timeScale
		float u = (Time.time-timeStart) / timeDuration;
		float t0, t1, ts0 = 1, ts1 = 1;
		float u2 = 0;
		
		if (u < 1) {
			for (int i=0; i<timeScalers.Count-1; i++) {
				t0 = timeScalers[i].time;
				t1 = timeScalers[i+1].time;
				if (u>t0 && u<t1) {
					u2 = (u-t0)/(t1-t0);
					ts0 = timeScalers[i].timeScale;
					ts1 = timeScalers[i+1].timeScale;
					break;
				}
			}
			float desiredTimeScale = (1-u2)*ts0 + u2*ts1;

			// Update timeScale
			Time.timeScale = desiredTimeScale;
			Time.fixedDeltaTime = 0.02f * desiredTimeScale;

			
			u = Mathf.Min (u,1);

			// Rotate the camera
			float yRot = startRY + (fullRotation * u);
			transform.rotation = Quaternion.Euler(0,yRot,0);

			// Move the camera
			cam.localPosition = (1-u)*p0 + u*p1;

		} else {
			u2 = Time.timeScale;
			if (Input.GetMouseButton(0)) {
				u2 = (1-afterTimeEasing)*u2 + afterTimeEasing*afterTimeScale;
			} else {
				u2 = (1-afterTimeEasing)*u2 + afterTimeEasing*1f;
			}
			Time.timeScale = u2;
			Time.fixedDeltaTime = Time.timeScale * 0.02f;
		}

		
		timer.text = FormatFloat(Time.time - timeStart, 2);
		scale.text = FormatFloat(Time.timeScale, 2);
		fdt.text = FormatFloat(Time.fixedDeltaTime, 4);
	}

	string FormatFloat(float n, int places) {
		float mult = Mathf.Pow(10,places);
		string s = (Mathf.Round (n * mult) / mult).ToString();
		while (s.Length < places+2) {
			s = s + "0";
		}
		return( s );
	}
}
