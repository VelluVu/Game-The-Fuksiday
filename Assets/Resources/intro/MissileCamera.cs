using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileCamera : MonoBehaviour {

	bool started;
	float X = -45;

	public void enable() {
		started = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (started) {
			X -= Time.deltaTime*2.0f;
			this.transform.position = new Vector3 (X - Random.Range (-0.1f, 0.1f), Random.Range (-0.1f, 0.1f), -10.0f);
		}
	}
}
